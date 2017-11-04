using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace BitWhiskey
{

    public class BChartStyle
    {
        public Pen drawPen;
        public Color backColor;
        public Pen barUp = new Pen(Color.FromArgb(63, 170, 57));// new Pen(Color.FromArgb(73,192,67));
        public Pen barDown = new Pen(Color.FromArgb(200, 11,11));
        public Pen SelLine = Pens.Pink;
        
        public Brush barUpBrush;
        public Brush barDownBrush;
        public Brush strLabelBrush;
        public Brush strLabelSelBrush;
        
        public Font  strLabelFont=new Font(FontFamily.GenericSansSerif,8);
        public Font strLabelSelFont = new Font(FontFamily.GenericSansSerif, 8);


        public BChartStyle()
        {
            drawPen = new Pen(Color.Red);
            backColor = Color.Black;
            barUpBrush = new SolidBrush(barUp.Color);
            barDownBrush = new SolidBrush(barDown.Color);
            strLabelBrush = new SolidBrush(Color.LightGray);
            strLabelSelBrush = new SolidBrush(Color.Green);
        }
    }
    public enum ConvertDataType
    {
        BAR_5,
        BAR_15,
        BAR_HOUR,
        BAR_DAY,
        BAR_WEEK,
        BAR_MONTH
    }
    public class ConvertData
    {
        ConvertDataType datatype;
        public bool created = false;
//        public int hour = -1;
        public int lastperiod = -1;
        public DateTime lastdate = DateTime.Now;
        public PriceCandle lastprice = new PriceCandle();
        public double prevclose = 0;
        PriceCandle lastprice15 = new PriceCandle();
        DateTime dt;

        public ConvertData(ConvertDataType datatype_)
        {
            datatype = datatype_;
        }
        public int HourPeriodNumber(DateTime date, int period)
        {
            int minute = date.Minute;
            int periodnum = (minute /period); 
            return periodnum;
        }

        public void ProcessBar(Dictionary<int, PriceCandle> pricedata)
        {
            int period = 0;

            switch (datatype)
            {
                case ConvertDataType.BAR_15:
                    period = HourPeriodNumber(dt,15);
                    break;
                case ConvertDataType.BAR_HOUR:
                    period = dt.Hour;
                    break;
                case ConvertDataType.BAR_DAY:
                    period = dt.Day;
                    break;
                case ConvertDataType.BAR_WEEK:
                    period = Helper.WeekNumber(dt);
                    break;
                case ConvertDataType.BAR_MONTH:
                    period = dt.Month;
                    break;
            }
            if (period != lastperiod)
            {
                AddPrice(pricedata);
                CreatePrice();
            }
            else
                AddBar();
        }
        public void SetCurBar(DateTime dt_, PriceCandle lastprice15_)
        {
            lastprice15 = lastprice15_;
            dt = dt_;
        }
        public void CreatePrice()
        {
            created = true;

            switch (datatype)
            {
                case ConvertDataType.BAR_15:
                    lastperiod = HourPeriodNumber(dt, 15);
                    lastdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, lastperiod*15, 0);
                    break;
                case ConvertDataType.BAR_HOUR:
                    lastperiod = dt.Hour;
                    lastdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
                    break;
                case ConvertDataType.BAR_DAY:
                    lastperiod = dt.Day;
                    lastdate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                    break;
                case ConvertDataType.BAR_WEEK:
                    lastperiod = Helper.WeekNumber(dt);
                    lastdate = Helper.WeekDate(dt.Year, lastperiod).AddDays(-1);
                    /*
                    DateTime d1 = new DateTime(2017, 1, 1).AddDays(34 * 7);
                    DateTime d2 = new DateTime(2017, 1, 1).AddDays(52 * 7);

                    lastperiod = Helper.WeekNumber(new DateTime(2017, 1, 15));
                    DateTime d3 = new DateTime(2017, 1, 1).AddDays(lastperiod * 7-1);
*/
                    break;
                case ConvertDataType.BAR_MONTH:
                    lastperiod = dt.Month;
                    lastdate = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
                    break;
            }


            lastprice = new PriceCandle();
            lastprice.open = lastprice15.open;
            lastprice.high = lastprice15.high;
            lastprice.low = lastprice15.low;
            lastprice.volume = lastprice15.volume;
            lastprice.date = lastprice15.date;
        }
        public void AddPrice(Dictionary<int, PriceCandle> pricedata)
        {
            if (created)
            {
                lastprice.close = prevclose;
                pricedata.Add(Helper.ToUnixTimeStamp(lastdate), lastprice);
            }
        }
        public void AddBar()
        {
            lastprice.high = Math.Max(lastprice.high, lastprice15.high);
            lastprice.low = Math.Min(lastprice.low, lastprice15.low);
            lastprice.volume += lastprice15.volume;
        }
    }
    public class PriceData
    {
        List<int> keys;
        Dictionary<int, PriceCandle> pricedata;
        Dictionary<int, PriceCandle> normpricedata;

        Dictionary<int, PriceCandle> origpricedata;
        Dictionary<int, PriceCandle> min15pricedata;
        Dictionary<int, PriceCandle> hourpricedata;
        Dictionary<int, PriceCandle> daypricedata;
        Dictionary<int, PriceCandle> weekpricedata;
        Dictionary<int, PriceCandle> monthpricedata;

        public PriceData()
        {
        }
        public void SetData(Dictionary<int, PriceCandle> pricedata_, ConvertDataType datatype)
        {
            origpricedata = pricedata_;
            Convert(false);
            SelectPeriod(datatype);
        }
        public void SelectPeriod(ConvertDataType datatype)
        {
            switch (datatype)
            {
                case ConvertDataType.BAR_5:
                      pricedata = origpricedata;
                    break;
                case ConvertDataType.BAR_15:
                    if (min15pricedata != null)
                        pricedata = min15pricedata;
                    break;
                case ConvertDataType.BAR_HOUR:
                    if (hourpricedata != null)
                        pricedata = hourpricedata;
                    break;
                case ConvertDataType.BAR_DAY:
                    if (daypricedata != null)
                        pricedata = daypricedata;
                    break;
                case ConvertDataType.BAR_WEEK:
                    if (weekpricedata != null)
                        pricedata = weekpricedata;
                    break;
                case ConvertDataType.BAR_MONTH:
                    if (monthpricedata != null)
                        pricedata = monthpricedata;
                    break;
            }
            keys = new List<int>(pricedata.Keys);
        }
        public void Convert(bool fromday)
        {
            List<int> keys15 ;
            keys15 = new List<int>(origpricedata.Keys);

            min15pricedata = new Dictionary<int, PriceCandle>();
            hourpricedata = new Dictionary<int, PriceCandle>();
            if (!fromday)
                daypricedata = new Dictionary<int, PriceCandle>();
            weekpricedata = new Dictionary<int, PriceCandle>();
            monthpricedata = new Dictionary<int, PriceCandle>();

            DateTime dt;
            PriceCandle lastprice15 = new PriceCandle();
            ConvertData min15data = new ConvertData(ConvertDataType.BAR_15);
            ConvertData hourdata = new ConvertData(ConvertDataType.BAR_HOUR);
            ConvertData daydata = new ConvertData(ConvertDataType.BAR_DAY);
            ConvertData weekdata = new ConvertData(ConvertDataType.BAR_WEEK);
            ConvertData monthdata = new ConvertData(ConvertDataType.BAR_MONTH);

            for (int n = 0; n < keys15.Count; n++)
            {
                if(n==486)
                { }
                dt = Helper.UnixToDateTime(keys15[n].ToString());
//                if (dt.Month == 9 && dt.Day == 15)
                min15data.prevclose = lastprice15.close;
                hourdata.prevclose = lastprice15.close;
                daydata.prevclose = lastprice15.close;
                weekdata.prevclose = lastprice15.close;
                monthdata.prevclose = lastprice15.close;
                lastprice15 = origpricedata[keys15[n]];

                if (!fromday)
                {
                    min15data.SetCurBar(dt, lastprice15);
                    min15data.ProcessBar(min15pricedata);

                    hourdata.SetCurBar(dt, lastprice15);
                    hourdata.ProcessBar(hourpricedata);

                    daydata.SetCurBar(dt, lastprice15);
                    daydata.ProcessBar(daypricedata);
                }

                weekdata.SetCurBar(dt, lastprice15);
                weekdata.ProcessBar(weekpricedata);

                monthdata.SetCurBar(dt, lastprice15);
                monthdata.ProcessBar(monthpricedata);

            }
            if (!fromday)
            {
                min15data.prevclose = lastprice15.close;
                hourdata.prevclose = lastprice15.close;
                daydata.prevclose = lastprice15.close;

                min15data.AddPrice(min15pricedata);
                hourdata.AddPrice(hourpricedata);
                daydata.AddPrice(daypricedata);
            }
            weekdata.prevclose = lastprice15.close;
            monthdata.prevclose = lastprice15.close;

            weekdata.AddPrice(weekpricedata);
            monthdata.AddPrice(monthpricedata);
        }
        public List<int> GetKeys()
        {
            return keys;
        }
        public Dictionary<int, PriceCandle> GetData()
        {
            return pricedata;
        }
        public Dictionary<int, PriceCandle> GetNormalizeData()
        {
            return normpricedata;
        }
        public int Count()
        {
            return keys.Count;
        }
        public void NormalizeData(double minprice, double yzoomcoeff)
        {
            normpricedata = new Dictionary<int, PriceCandle>();

            foreach (int key in keys)
            {
                PriceCandle p = pricedata[key];
                PriceCandle pnew = p.Copy();
                pnew.open = (pnew.open - minprice) / yzoomcoeff;
                pnew.high = (pnew.high - minprice) / yzoomcoeff;
                pnew.low = (pnew.low - minprice) / yzoomcoeff;
                pnew.close = (pnew.close - minprice) / yzoomcoeff;
//                pnew.date = key;
                normpricedata.Add(key, pnew);
            }
        }

    }
    public class ChartButton
    {
        public PointF pos ;
        public PointF size;
        public string text = "";
        public bool clicked = false;

        public ChartButton(PointF pos_, PointF size_, string text_)
        {
           pos = pos_;
            size = size_;
            text = text_;
        }
        public void Click()
        {
            clicked = true;
        }
        public void Reset()
        {
            clicked = false;
        }
    }
    public class PriceChart
    {
        string chartCaption;
        Graphics gform;
        Graphics g = null;
        Bitmap bmap ;
        BChartStyle style = new BChartStyle();
        PriceData data;

        float xFormPos = 0;
        float yFormPos = 0;
        float areaxmax = 0;
        float areaymax = 0;
        // draw rect
        float xstart = 0;
        float ystart = 0;
        float xend = 0;
        float yend = 0;
        float draww = 0;
        float drawh = 0;

        //markers labels
        float LeftOffset = 0;
        float RightOffset = 0;
        float TopOffset = 0;
        float DownOffset = 0;

        float yzoomcoeff = 1;

        // max min data
        double maxprice = 0;
        double minprice = double.MaxValue;


        float barsizex = 5;
        int lasttick =0;
        int starttick =0;

        bool scrollActive = false;
        PointF scrollStart;
        PointF scrollEnd;
        PointF scrollChange;

        //mouse selection
        int mousex=200;
        int mousey=200;
        float mousedrawx =0;
        float mousedrawy = 0;
        float mouseareax = 0;
        float mouseareay = 0;
        int buttonstate = 0;
        int selectedPriceKey = -1;

        //buttons
        ChartButton buttonMonth;
        ChartButton buttonWeek;
        ChartButton buttonDay;
        ChartButton buttonHour;
        ChartButton buttonMin15;
        ChartButton buttonMin5;

        ChartButton buttonZoomPlus;
        ChartButton buttonZoomMinus;


        public PriceChart(string chartCaption_,Graphics gform_, float xFormPos_, float yFormPos_, float xmax_, float ymax_)
        {
            chartCaption = chartCaption_;
            Init(gform_,xFormPos_, yFormPos_, xmax_, ymax_);
        }
        public void Init(Graphics gform_,float xFormPos_, float yFormPos_, float xmax_, float ymax_)
        {
            gform = gform_;
            xFormPos = xFormPos_;
            yFormPos = yFormPos_;
            areaxmax = xmax_;
            areaymax = ymax_;

            LeftOffset = 5;
            RightOffset = 85;
            TopOffset = 22;
            DownOffset = 23;

            xstart = LeftOffset;
            ystart = DownOffset;
            xend = areaxmax - RightOffset;
            yend = areaymax - TopOffset;
            draww = xend - xstart;
            drawh = yend - ystart;

            ChartButton selButton = GetSelectedButton();

            float bx = 4;
            float bxadd = 3;
            float sx = 26;
            float bsizeh = 18;
            buttonMonth = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh-2), "M");
            bx += sx + bxadd;
            buttonWeek = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "W");
            bx += sx + bxadd;
            buttonDay = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "D");
            bx += sx + bxadd;
            buttonHour = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "H");
            bx += sx + bxadd;
            buttonMin15 = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "M15");
            bx += sx + bxadd;
            buttonMin5 = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "M5");

            bx += sx + 12;
//            sx = 30;
            buttonZoomPlus = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "+");
            bx += sx + bxadd;
            buttonZoomMinus = new ChartButton(new PointF(bx, areaymax - bsizeh), new PointF(sx, bsizeh - 2), "-");

            bmap = new Bitmap((int)areaxmax, (int)areaymax);
            g = Graphics.FromImage(bmap);

            if(selButton!=null)
              SelectButton(selButton.text);
        }
        public void SetData(Dictionary<int, PriceCandle> pricedata_, ConvertDataType convertFromType, ConvertDataType viewType)
        {
            data = new PriceData();
            data.SetData(pricedata_, convertFromType);

            switch (viewType)
            {
                case ConvertDataType.BAR_5:
                    SelectButton(buttonMin5.text);
                    break;
                case ConvertDataType.BAR_15:
                    SelectButton(buttonMin15.text);
                    break;
                case ConvertDataType.BAR_HOUR:
                    SelectButton(buttonHour.text);
                    break;
                case ConvertDataType.BAR_DAY:
                    SelectButton(buttonDay.text);
                    break;
                case ConvertDataType.BAR_WEEK:
                    SelectButton(buttonWeek.text);
                    break;
                case ConvertDataType.BAR_MONTH:
                    SelectButton(buttonMonth.text);
                    break;
            }

            data.SelectPeriod(viewType);
            ScrollToEnd();
            ReCalc();

        }
        public void SaveImage(string imagepathfile)
        {
            ReDrawFull();
            bmap.Save(imagepathfile, ImageFormat.Png);
        }
        public PriceData GetPriceData()
        {
            return data;
       
        }
        public void Zoom(bool zoomin)
        {
            if (zoomin)
            {
                barsizex+=1;
            }
            else
            {
                  barsizex-=1;
                  if (barsizex < 1)
                      barsizex = 1;
            }
        }
        public void ScrollToEnd()
        {
            lasttick = data.Count() - 1;
        } 
        public void CalcScroll()
        {
            if(scrollActive)
            {
                lasttick = lasttick - (int)(scrollChange.X);
                scrollChange = new PointF(0, 0);
            }
            if (lasttick >= data.Count())
                lasttick = data.Count() - 1;
            starttick = lasttick - (int)(draww / barsizex);
            if (starttick < 0)
                starttick = 0;

        }
        public void MouseMove(int x, int y)
        {
            mousex = x;
            mousey = y;

            CalcMouseSelection();

            if (scrollActive)
            {
                scrollEnd = new PointF(x, y);
                float needBarSize=2;
                float barSizeFactor=barsizex/needBarSize;
                if(barSizeFactor<1)
                    barSizeFactor=1;
                float scrollInPixels = scrollStart.X - scrollEnd.X;
//                float scrollUnitInPixels = barsizex;
                float scrollInBars=scrollInPixels/barSizeFactor;
                scrollChange = new PointF(scrollInBars, scrollEnd.Y - scrollStart.Y);
                scrollStart = new PointF(x, y);

                ReCalc();
            }

            ReDraw();
        }
        public void MouseClick(int x, int y,int buttonstate_)
        {

            buttonstate = buttonstate_;
            bool redraw = false;

            scrollActive = false;
            if (buttonstate != 1)
                return;

            scrollActive = true;
            scrollStart = new PointF(x, y);
            scrollEnd = new PointF(x, y);
            scrollChange = new PointF(0,0);


            if( MouseOnButton(buttonMonth))
            {
                SelectButton(buttonMonth.text);
                data.SelectPeriod(ConvertDataType.BAR_MONTH);
                redraw = true;
            }
            else if (MouseOnButton(buttonWeek))
            {
                SelectButton(buttonWeek.text);
                data.SelectPeriod(ConvertDataType.BAR_WEEK);
               redraw = true;
            }
            else if (MouseOnButton(buttonDay))
            {
                SelectButton(buttonDay.text);
                data.SelectPeriod(ConvertDataType.BAR_DAY);
                redraw = true;
            }
            else if (MouseOnButton(buttonHour))
            {
                SelectButton(buttonHour.text);
                data.SelectPeriod(ConvertDataType.BAR_HOUR);
                redraw = true;
            }
            else if (MouseOnButton(buttonMin15))
            {
                SelectButton(buttonMin15.text);
                data.SelectPeriod(ConvertDataType.BAR_15);
                redraw = true;
            }
            else if (MouseOnButton(buttonMin5))
            {
                SelectButton(buttonMin5.text);
                data.SelectPeriod(ConvertDataType.BAR_5);
                redraw = true;
            }

            if (MouseOnButton(buttonZoomPlus))
            {
                Zoom(true);
                ReDrawFull();
            }
            if (MouseOnButton(buttonZoomMinus))
            {
                Zoom(false);
                ReDrawFull();
            }

            if (redraw)
            {
                ScrollToEnd();
                ReDrawFull();
            }

        }
        void SelectButton(string btext)
        {
            if(btext==null)
                return ;
            buttonMonth.Reset();
            buttonWeek.Reset();
            buttonDay.Reset();
            buttonHour.Reset();
            buttonMin15.Reset();
            buttonMin5.Reset();

            if (btext== buttonMonth.text) buttonMonth.Click();
            if (btext == buttonWeek.text) buttonWeek.Click();
            if (btext == buttonDay.text) buttonDay.Click();
            if (btext == buttonHour.text) buttonHour.Click();
            if (btext == buttonMin15.text) buttonMin15.Click();
            if (btext == buttonMin5.text) buttonMin5.Click();
        }
        ChartButton GetSelectedButton()
        {
            if(buttonMonth==null)
                return null;

            if (buttonMonth.clicked) return buttonMonth;
            if (buttonWeek.clicked) return buttonWeek;
            if (buttonDay.clicked) return buttonDay;
            if (buttonHour.clicked) return buttonHour;
            if (buttonMin15.clicked) return buttonMin15;
            if (buttonMin5.clicked) return buttonMin5;
            return null; 
        }
        void CalcMouseSelection()
        {
            mousedrawx = -1;
            mousedrawy = -1;

            mouseareax = -1;
            mouseareay = -1;

            if (mousex >= xFormPos + LeftOffset && mousex <= xFormPos + LeftOffset + draww)
                if (mousey >= yFormPos + TopOffset && mousey <= yFormPos + TopOffset + drawh)
                {
                    mousedrawx =(int)( mousex - (xFormPos + LeftOffset));
                    mousedrawy = (int)((yFormPos + TopOffset+drawh) - mousey);
                }

            if (mousex >= xFormPos  && mousex <= xFormPos + areaxmax)
                if (mousey >= yFormPos && mousey <= yFormPos + areaymax)
                {
                    mouseareax = (int)(mousex - xFormPos);
                    mouseareay = (int)((yFormPos + areaymax) - mousey);
                }

        }
        public void ReDrawFull()
        {
            ReCalc();
            ReDraw();
        }
        public void ReCalc()
        {
            CalcScroll();
            PrepareChart();
        }
    
        public void Resize(Graphics gform_, float xFormPos_, float yFormPos_, float xmax_, float ymax_)
    {
        Init(gform_,xFormPos_, yFormPos_, xmax_, ymax_);
    }
    public void ReDraw()
        {

            g.Clear(style.backColor);

            DrawMainChart();
            DrawPrice();
            
            DrawUpdate();

        }
        void DrawUpdate()
        {
            gform.DrawImage(bmap, (int)xFormPos, (int)yFormPos);
        }
        void PrepareChart()
        {
            maxprice = 0;
            minprice = double.MaxValue;

            Dictionary<int, PriceCandle> pricedata = data.GetData();
            List<int> keys = data.GetKeys(); 
            for (int n = starttick; n <= lasttick; n++)
            {
                int key1 = keys[n];
//                int date1 = key1;
                PriceCandle price1 = pricedata[key1];
                if (price1.open > maxprice)
                    maxprice = price1.open;
                if (price1.high > maxprice)
                                        maxprice = price1.high;
                    if (price1.low > maxprice)
                                          maxprice = price1.low;
                        if (price1.close > maxprice)
                                            maxprice = price1.close;

                if (price1.open < minprice)
                    minprice = price1.open;
                if (price1.high < minprice)
                                        minprice = price1.high;
                    if (price1.low < minprice)
                                            minprice = price1.low;
                        if (price1.close < minprice)
                                              minprice = price1.close;

            }
            yzoomcoeff = (float)(maxprice-minprice) / drawh;
            data.NormalizeData(minprice, yzoomcoeff);

        }
        double NormalizePrice(double pricedata)
        {
            return (pricedata - minprice) / yzoomcoeff;
        }
        double DeNormalizePrice(double pricedata)
        {
            return (pricedata * yzoomcoeff) + minprice;
        }
        void DrawPrice()
        {
            Dictionary<int, PriceCandle> pricedata = data.GetNormalizeData();
            List<int> keys = data.GetKeys();
            
            float pos = draww;
            selectedPriceKey=-1;

            for (int n = lasttick; n > starttick; n--)
            {
                Pen pen = style.barUp;
                Brush brush = style.barUpBrush;

                int key1 = keys[n - 1];
                int date1 = key1;
                PriceCandle price1 = pricedata[key1];
                int key2 = keys[n];
                int date2 = key2;
                PriceCandle price2 = pricedata[key2];
                bool fill = false;
                if (price2.close < price2.open)
                {
                    pen = style.barDown;
                    brush = style.barDownBrush;
                    fill = true;
                }
                float barspace = 0;

                if (mousedrawx != -1)
                {
                    if (mousedrawx >= pos - barsizex && mousedrawx <pos)
                    {
                        pen = new Pen(Color.Yellow);
                        brush = new SolidBrush(Color.Yellow);
                        selectedPriceKey = key2;
                    }

                }

                if (barsizex > 2)
                {
                    barspace = 2;
                    float realbarsize = barsizex - barspace;
                    PointF p = new PointF(pos - barsizex, (float)Math.Min(price2.close, price2.open));
                    PointF size = new PointF(realbarsize, (float)Math.Abs(price2.close - price2.open));
                    AreaDrawRectangle(pen, brush, p, size, fill);

                    PointF p1 = new PointF(pos - barsizex / 2 - barspace / 2, (float)price2.high);
                    PointF p2 = new PointF(pos - barsizex / 2 - barspace / 2, (float)Math.Max(price2.close, price2.open));
                    AreaDrawLine(pen, p1, p2);

                    p1 = new PointF(pos - barsizex / 2 - barspace / 2, (float)price2.low);
                    p2 = new PointF(pos - barsizex / 2 - barspace / 2, (float)Math.Min(price2.close, price2.open));
                    AreaDrawLine(pen, p1, p2);
                }
                else
                {
                    PointF p1 = new PointF(pos - barsizex / 2, (float)price2.low);
                    PointF p2 = new PointF(pos - barsizex / 2, (float)price2.high);
                    if (p1.X == p2.X && p1.Y == p2.Y)
                        p2.Y += 1;
                    AreaDrawLine(pen, p1, p2);
                }
                pos -= barsizex;
            }
        }
        void DrawMainChart()
        {
            // price rect
            int dx = 3;
            PointF p = new PointF(-dx,- dx);
            PointF size = new PointF(draww + dx * 2, drawh + dx * 2);
            AreaDrawRectangle(Pens.RosyBrown, Brushes.Red, p, size, false);

            // right price levels
            int plevelcount = 7;
            double startplevel = minprice;
            double endplevel = maxprice;
            double priceadd = (endplevel - startplevel) / plevelcount;

            double curplevel = startplevel;
            for (int n = 0; n <= plevelcount; n++)
            {
                p = new PointF(draww + 10, (float)NormalizePrice(curplevel) + style.strLabelFont.Size/2);
                string pricestr = Helper.PriceToString(curplevel);
                AreaDrawString(style.strLabelBrush, style.strLabelFont, p, pricestr);
                 curplevel += priceadd;
            }

            // down data labels
            int datesize = 90;
            float cursize = 0;
            float pos = draww - barsizex;
            Dictionary<int, PriceCandle> pricedata = data.GetNormalizeData();
            List<int> keys = data.GetKeys();
            for (int n = lasttick; n > starttick; n--)
            {
                cursize += barsizex;
                if (cursize >= datesize)
                {
                    int date1 =keys[n];
                    p.X = pos;
                    p.Y = -style.strLabelFont.Size ;
                    DateTime unixtime = Helper.UnixToDateTime(date1.ToString());
                    AreaDrawString(style.strLabelBrush, style.strLabelFont, p, unixtime.ToString("dd.MM.yyyy"));
                    cursize=0;
                }
                pos -= barsizex;
            }

            // mouse selection

            if (mousedrawx != -1)
            {
                PointF p1 = new PointF(0, mousedrawy);
                PointF p2 = new PointF(draww, mousedrawy);
                float[] dashValues = { 5, 8 };
                Pen dashPen = new Pen(style.SelLine.Color);
                dashPen.DashPattern = dashValues;
                AreaDrawLine(dashPen, p1, p2);

                float sy = mousedrawy - style.strLabelSelFont.Size;
                p = new PointF(draww+10,sy);
                size = new PointF(RightOffset, style.strLabelSelFont.Size * 2f);
                AreaDrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, true);
                AreaDrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, false);

                p = new PointF(draww + 10, sy + style.strLabelSelFont.Size * 2);
                string pricestr = Helper.PriceToString(DeNormalizePrice(mousedrawy));
                AreaDrawString(style.strLabelSelBrush, style.strLabelSelFont, p, pricestr);

                // vertical
                p1 = new PointF(mousedrawx,0);
                p2 = new PointF(mousedrawx, drawh);
                float[] dashValuesV = { 4, 11 };
                dashPen = new Pen(style.SelLine.Color);
                dashPen.DashPattern = dashValuesV;
                AreaDrawLine(dashPen, p1, p2);

                if (selectedPriceKey != -1)
                {
                    PriceCandle price = pricedata[selectedPriceKey];
                    DateTime unixtime = Helper.UnixToDateTime(price.date.ToString());
                    string strtime = unixtime.ToString("dd.MM.yyyy  HH:mm");
                    float strwidth=Helper.StringSize(g, style.strLabelSelFont, strtime).Width;
                    p = new PointF(mousedrawx - strwidth/2-3,0);
                    size = new PointF(strwidth+3*2, style.strLabelSelFont.Size * 2f);
                    DrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, true);
                    DrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, false);

                    p.X = mousedrawx- strwidth / 2;
                    p.Y = -style.strLabelSelFont.Size;
                    AreaDrawString(style.strLabelSelBrush, style.strLabelSelFont, p, strtime);
                }

            }

            // buttons
            DrawButton(buttonMonth);
            DrawButton(buttonWeek);
            DrawButton(buttonDay);
            DrawButton(buttonHour);
            DrawButton(buttonMin15);
            DrawButton(buttonMin5);
            DrawButton(buttonZoomPlus);
            DrawButton(buttonZoomMinus);


            DrawString(style.strLabelBrush, style.strLabelFont, new PointF(draww+10 , areaymax - style.strLabelSelFont.Size/2), chartCaption);

        }

        private bool MouseOnButton(ChartButton button)
        {
            if (mouseareax >= button.pos.X && mouseareax <= button.pos.X + button.size.X)
                if (mouseareay >= button.pos.Y && mouseareay <= button.pos.Y + button.size.Y)
                {
                    return true;
                }

            return false;
        }
        private void DrawButton(ChartButton button)
        {
            if(button.clicked)
              DrawRectangle(Pens.Red, Brushes.LightCoral, button.pos, button.size, true);
            else
                DrawRectangle(Pens.Red, Brushes.DarkGray, button.pos, button.size, true);
            PointF pos = new PointF(button.pos.X, button.pos.Y + button.size.Y);
            SizeF strsize=Helper.StringSize(g, style.strLabelSelFont, button.text);
            pos.X += button.size.X / 2 - strsize.Width / 2;
            DrawString(Brushes.Black, style.strLabelSelFont,pos,button.text);
        }
        private void DrawRectangle(Pen pen, Brush brush, PointF pos, PointF size, bool fill)
        {
            pos.Y = pos.Y + size.Y;
            pos = ToArea(pos);
            if (fill)
                g.FillRectangle(brush, pos.X, pos.Y, size.X, size.Y);
            else
                g.DrawRectangle(pen, pos.X, pos.Y, size.X, size.Y);
        }
        private void DrawString(Brush brush, Font font, PointF pos, string s)
        {
            pos = ToArea(pos);
            g.DrawString(s, font, brush, pos);
        }
        private PointF ToArea(PointF p)
        {
            return new PointF(p.X + xFormPos, areaymax - p.Y);
        }

        private void AreaDrawString(Brush brush, Font font, PointF pos, string s)
        {
            pos = ToDrawArea(pos);
            g.DrawString(s,font,brush, pos);
        }
        private void AreaDrawRectangle(Pen pen, Brush brush, PointF pos, PointF size, bool fill)
        {
            pos.Y = pos.Y + size.Y;
            pos = ToDrawArea(pos);
            if (fill)
                g.FillRectangle(brush, pos.X, pos.Y, size.X, size.Y);
            else
                g.DrawRectangle(pen, pos.X, pos.Y, size.X, size.Y);
        }
        private void AreaDrawLine(Pen pen, PointF p1, PointF p2)
        {
            p1 = ToDrawArea(p1);
            p2 = ToDrawArea(p2);
            g.DrawLine(pen, p1, p2);
        }
        private PointF ToDrawArea(PointF p)
        {
            return new PointF(p.X + xstart, areaymax-DownOffset - p.Y);
        }
    }
}
