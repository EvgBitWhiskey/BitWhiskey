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
        public Pen volbarUp = new Pen(Helper.ColorAlpha(Color.MediumSeaGreen,100));
        public Pen volbarDown = new Pen(Helper.ColorAlpha(Color.LightPink,130));
        public Pen SelLine = Pens.Pink;
        
        public Brush barUpBrush;
        public Brush barDownBrush;

        public Brush volbarUpBrush ;
        public Brush volbarDownBrush;

        public Brush strLabelBrush;
        public Font strLabelFont = new Font(FontFamily.GenericSansSerif, 8);

        public Brush strLabelSelBrush;
        public Font strLabelSelFont = new Font(FontFamily.GenericSansSerif, 8);

        public Brush strLastPriceBrush = new SolidBrush(Color.DarkTurquoise);
        public Font strLastPriceFont = new Font(new FontFamily("Arial"), 11);


        public BChartStyle()
        {
            drawPen = new Pen(Color.Red);
            backColor = Color.Black;
            barUpBrush = new SolidBrush(barUp.Color);
            barDownBrush = new SolidBrush(barDown.Color);
            volbarUpBrush = new SolidBrush(volbarUp.Color);
            volbarDownBrush = new SolidBrush(volbarDown.Color);
            strLabelBrush = new SolidBrush(Color.LightGray);
            strLabelSelBrush = new SolidBrush(Color.Green);
        }
    }
    public class DrawContext
    {
        Graphics gform;
        Graphics g = null;
        Bitmap bmap;

        public float xFormPos = 0;
        public float yFormPos = 0;
        public float areaxmax = 0;
        public float areaymax = 0;

        // draw rect
        public float xstart = 0;
        public float ystart = 0;
        public float xend = 0;
        public float yend = 0;
        public float draww = 0;
        public float drawh = 0;

        //markers labels
        public float LeftOffset = 0;
        public float RightOffset = 0;
        public float TopOffset = 0;
        public float DownOffset = 0;

        public DrawContext(Graphics gform_, float xmax_, float ymax_, float xFormPos_, float yFormPos_,
            float LeftOffset_, float RightOffset_, float TopOffset_, float DownOffset_)
        {
            gform = gform_;
            bmap = new Bitmap((int)xmax_, (int)ymax_);
            g = Graphics.FromImage(bmap);
            areaxmax = xmax_;
            areaymax = ymax_;
            xFormPos = xFormPos_;
            yFormPos = yFormPos_;

            LeftOffset = LeftOffset_;
            RightOffset = RightOffset_;
            TopOffset = TopOffset_;
            DownOffset = DownOffset_;

            xstart = LeftOffset;
            ystart = DownOffset;
            xend = areaxmax - RightOffset;
            yend = areaymax - TopOffset;
            draww = xend - xstart;
            drawh = yend - ystart;

        }
        
        public void Dispose()
        {
            gform.Dispose();
        }
        
        public void SaveImage(string imagepathfile, ImageFormat format)
        {
            bmap.Save(imagepathfile, format);
        }
        public void Clear(Color color)
        {
            g.Clear(color);
        }
        public void DrawToScreen()
        {
            gform.DrawImage(bmap, (int)xFormPos, (int)yFormPos);
        }
        public void DrawRectangle(Pen pen, Brush brush, PointF pos, PointF size, bool fill)
        {
            pos.Y = pos.Y + size.Y;
            pos = ToArea(pos);
            if (fill)
                g.FillRectangle(brush, pos.X, pos.Y, size.X, size.Y);
            else
                g.DrawRectangle(pen, pos.X, pos.Y, size.X, size.Y);
        }
        public void DrawString(Brush brush, Font font, PointF pos, string s)
        {
            pos = ToArea(pos);
            g.DrawString(s, font, brush, pos);
        }
        public PointF ToArea(PointF p)
        {
            return new PointF(p.X + xFormPos, areaymax - p.Y);
        }

        //AreaDraw... - draw inside area, without offsets
        public void AreaDrawString(Brush brush, Font font, PointF pos, string s)
        {
            pos = ToDrawArea(pos);
            g.DrawString(s, font, brush, pos);
        }
        public void AreaDrawRectangle(Pen pen, Brush brush, PointF pos, PointF size, bool fill)
        {
            pos.Y = pos.Y + size.Y;
            pos = ToDrawArea(pos);
            if (fill)
                g.FillRectangle(brush, pos.X, pos.Y, size.X, size.Y);
            else
                g.DrawRectangle(pen, pos.X, pos.Y, size.X, size.Y);
        }
        public void AreaDrawLine(Pen pen, PointF p1, PointF p2)
        {
            p1 = ToDrawArea(p1);
            p2 = ToDrawArea(p2);
            g.DrawLine(pen, p1, p2);
        }
        public PointF ToDrawArea(PointF p)
        {
            return new PointF(p.X + xstart, areaymax - DownOffset - p.Y);
        }


    }

    public class PriceDrawPlugin
    {
        DrawContext draw;
        public PriceDrawPlugin(DrawContext draw_)
        {
            draw = draw_;
        }
        public void BeforeDraw()
        {
        }
        public void AfterDraw()
        {
           // draw.AreaDrawLine(Pens.Blue,new PointF(0, 0), new PointF(draw.draww, draw.drawh));
        }

    }

    public class PriceChart
    {
        string chartCaption;
        BChartStyle style = new BChartStyle();
        PriceData data;
        PriceDrawPlugin plugin;

        float yzoomcoeff = 1;
        // max min data
        double maxprice = 0;
        double minprice = double.MaxValue;

        float volzoomcoeff = 1;
        double maxvol = 0;
        double minvol = double.MaxValue;

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
        int selectedPriceKey = -1;

        bool drawVolume = true;

        DrawContext draw;

        public PriceChart(string chartCaption_,Graphics gform_, float xFormPos_, float yFormPos_, float xmax_, float ymax_)
        {
            chartCaption = chartCaption_;
            Init(gform_,xFormPos_, yFormPos_, xmax_, ymax_);
        }
        public void Init(Graphics gform_,float xFormPos_, float yFormPos_, float xmax_, float ymax_)
        {
            draw = new DrawContext(gform_, xmax_, ymax_, xFormPos_, yFormPos_,5,85,30,23);
            plugin = new PriceDrawPlugin(draw);
        }
        public void SetData(Dictionary<int, PriceCandle> pricedata_, ConvertDataType convertFromType, ConvertDataType viewType)
        {
            data = new PriceData();
            data.SetData(pricedata_, convertFromType, viewType);

            ScrollToEnd();
            ReCalc();

        }
        public void ShowVolume(bool showVolume)
        {
            drawVolume = showVolume;
        }
        public float GetVolumeDrawH()
        {
            return draw.drawh/4.0f;
        }

        public void Dispose()
        {
            draw.Dispose();
        }
        
        public void SaveImage(string imagepathfile)
        {
            ReDrawFull();
            draw.SaveImage(imagepathfile, ImageFormat.Png);
        }
        public PriceData GetPriceData()
        {
            return data;
       
        }
        public void UpdatePrice(double lastPrice)
        {
            bool isChartEnd = (lasttick == data.Count() - 1);
            bool barAdded=data.UpdatePrice(lastPrice);
            if(barAdded && isChartEnd)
              ScrollToEnd();
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
            starttick = lasttick - (int)(draw.draww / barsizex);
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
            scrollActive = false;
            if (buttonstate_ != 1)
                return;

            scrollActive = true;
            scrollStart = new PointF(x, y);
            scrollEnd = new PointF(x, y);
            scrollChange = new PointF(0,0);
        }
        public void ClearMouseSelection()
        {
            mousedrawx = -1;
            mousedrawy = -1;
            mouseareax = -1;
            mouseareay = -1;
        }
        void CalcMouseSelection()
        {
            mousedrawx = -1;
            mousedrawy = -1;

            mouseareax = -1;
            mouseareay = -1;

            if (mousex >= draw.xFormPos + draw.LeftOffset && mousex <= draw.xFormPos + draw.LeftOffset + draw.draww)
                if (mousey >= draw.yFormPos + draw.TopOffset && mousey <= draw.yFormPos + draw.TopOffset + draw.drawh)
                {
                    mousedrawx =(int)( mousex - (draw.xFormPos + draw.LeftOffset));
                    mousedrawy = (int)((draw.yFormPos + draw.TopOffset + draw.drawh) - mousey);
                }

            if (mousex >= draw.xFormPos  && mousex <= draw.xFormPos + draw.areaxmax)
                if (mousey >= draw.yFormPos && mousey <= draw.yFormPos + draw.areaymax)
                {
                    mouseareax = (int)(mousex - draw.xFormPos);
                    mouseareay = (int)((draw.yFormPos + draw.areaymax) - mousey);
                }

        }
        public void Resize(Graphics gform_, float xFormPos_, float yFormPos_, float xmax_, float ymax_)
        {
            Init(gform_, xFormPos_, yFormPos_, xmax_, ymax_);
        }
        public void ReCalc()
        {
            CalcScroll();
            PrepareChart();
        }
        public void ReDrawFull()
        {
            ReCalc();
            ReDraw();
        }
    
    public void ReDraw()
        {

            draw.Clear(style.backColor);

            plugin.BeforeDraw();

            DrawMainChart();
            DrawPrice();

            plugin.AfterDraw();

            draw.DrawToScreen();

        }
        void PrepareChart()
        {
            maxprice = 0;
            minprice = double.MaxValue;

            maxvol = 0;
            minvol = double.MaxValue;

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
                if (price1.volume > maxvol)
                    maxvol = price1.volume;

                if (price1.open < minprice)
                    minprice = price1.open;
                if (price1.high < minprice)
                    minprice = price1.high;
                if (price1.low < minprice)
                    minprice = price1.low;
                if (price1.close < minprice)
                    minprice = price1.close;
                if (price1.volume < minvol)
                    minvol = price1.volume;

            }
            yzoomcoeff = (float)(maxprice-minprice) / draw.drawh;
            float voldrawh=GetVolumeDrawH();
            volzoomcoeff = (float)(maxvol - minvol) / voldrawh;
            data.NormalizeData(minprice, yzoomcoeff, minvol, volzoomcoeff);

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
            
            float pos = draw.draww;
            selectedPriceKey=-1;

            Dictionary<int, PriceCandle> realData = data.GetData();
            PriceCandle lastPrice = realData.Last().Value; //[keys[lasttick]];
            string strLastPrice = Helper.PriceToString(lastPrice.close);
            draw.DrawString(style.strLastPriceBrush, style.strLastPriceFont, new PointF(draw.draww - 80, draw.areaymax ), strLastPrice);

            for (int n = lasttick; n > starttick; n--)
            {
                Pen pen = style.barUp;
                Brush brush = style.barUpBrush;

                Pen volpen = style.volbarUp;
                Brush volbrush = style.volbarUpBrush;

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
                    volpen = style.volbarDown;
                    volbrush = style.volbarDownBrush;
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

                    PointF p;
                    PointF size;
                    // volume
                    if (drawVolume)
                    {
                        p = new PointF(pos - barsizex - 1, (float)0);
                        size = new PointF(realbarsize + 1, (float)price2.volume);
                        draw.AreaDrawRectangle(volpen, volbrush, p, size, true);
                    }
                    // price
                    p = new PointF(pos - barsizex, (float)Math.Min(price2.close, price2.open));
                    size = new PointF(realbarsize, (float)Math.Abs(price2.close - price2.open));
                    draw.AreaDrawRectangle(pen, brush, p, size, fill);

                    PointF p1 = new PointF(pos - barsizex / 2 - barspace / 2, (float)price2.high);
                    PointF p2 = new PointF(pos - barsizex / 2 - barspace / 2, (float)Math.Max(price2.close, price2.open));
                    draw.AreaDrawLine(pen, p1, p2);

                    p1 = new PointF(pos - barsizex / 2 - barspace / 2, (float)price2.low);
                    p2 = new PointF(pos - barsizex / 2 - barspace / 2, (float)Math.Min(price2.close, price2.open));
                    if (p1.X == p2.X && p1.Y == p2.Y)
                        p2.X += 1;
                    draw.AreaDrawLine(pen, p1, p2);
                }
                else
                {
                    PointF p1;
                    PointF p2;

                    // volume
                    if (drawVolume)
                    {
                        p1 = new PointF(pos - barsizex / 2, (float)0);
                        p2 = new PointF(pos - barsizex / 2, (float)price2.volume);
                        if (p1.X == p2.X && p1.Y == p2.Y)
                            p2.Y += 1;
                        draw.AreaDrawLine(volpen, p1, p2);
                    }

                    // price
                    p1 = new PointF(pos - barsizex / 2, (float)price2.low);
                    p2 = new PointF(pos - barsizex / 2, (float)price2.high);
                    if (p1.X == p2.X && p1.Y == p2.Y)
                        p2.Y += 1;
                    draw.AreaDrawLine(pen, p1, p2);

                }

                pos -= barsizex;
            }
        }
        void DrawMainChart()
        {
            // price rect
            int dx = 3;
            PointF p = new PointF(-dx,- dx);
            PointF size = new PointF(draw.draww + dx * 2, draw.drawh + dx * 2);
            draw.AreaDrawRectangle(Pens.RosyBrown, Brushes.Red, p, size, false);

            // draw right price levels
            int plevelcount = 7;
            double startplevel = minprice;
            double endplevel = maxprice;
            double priceadd = (endplevel - startplevel) / plevelcount;

            double curplevel = startplevel;
            for (int n = 0; n <= plevelcount; n++)
            {
                p = new PointF(draw.draww + 10, (float)NormalizePrice(curplevel) + style.strLabelFont.Size/2);
                string pricestr = Helper.PriceToString(curplevel);
                draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, pricestr);
                 curplevel += priceadd;
            }

            // draw left volume levels
            if (drawVolume)
            {
                float voldrawh = GetVolumeDrawH();
                p = new PointF(draw.LeftOffset, (float)voldrawh + style.strLabelFont.Size / 2);
                string volstr = Helper.PriceToStringFinance(maxvol);
                draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, volstr);

                p = new PointF(draw.LeftOffset, (float)voldrawh * (2f / 3f) + style.strLabelFont.Size / 2);
                volstr = Helper.PriceToStringFinance(maxvol * (2f / 3f));
                draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, volstr);

                p = new PointF(draw.LeftOffset, (float)voldrawh * (1f / 3f) + style.strLabelFont.Size / 2);
                volstr = Helper.PriceToStringFinance(maxvol * (1f / 3f));
                draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, volstr);

                p = new PointF(draw.LeftOffset, (float)voldrawh * (1f / 6f) + style.strLabelFont.Size / 2);
                volstr = Helper.PriceToStringFinance(maxvol * (1f / 6f));
                draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, volstr);
            }


            // down data labels
            int datesize = 90;
            float cursize = 0;
            float pos = draw.draww - barsizex;
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
                    DateTime unixtime = Helper.UnixToDateTime(date1.ToString()).ToLocalTime();
                    draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, unixtime.ToString("dd.MM.yyyy"));
                    cursize=0;
                }
                pos -= barsizex;
            }

            Dictionary<int, PriceCandle> curdata = data.GetData();
            PriceCandle lastpriceReal = curdata.Last().Value;// [curdata.Count - 1];
            PriceCandle lastprice = pricedata.Last().Value;
            // rectangle with last price
            float ry = (float)lastprice.close - style.strLastPriceFont.Size;
            p = new PointF(draw.draww + 10, ry);
            size = new PointF(draw.RightOffset, style.strLastPriceFont.Size * 2f);
            draw.AreaDrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, true);
            draw.AreaDrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, false);
            p = new PointF(draw.draww + 10, ry + style.strLastPriceFont.Size * 2);
            string pricestrreal = Helper.PriceToString(lastpriceReal.close);
            draw.AreaDrawString(style.strLastPriceBrush, style.strLastPriceFont, p, pricestrreal);

            // mouse selection
            if (mousedrawx != -1)
            {

                PointF p1 = new PointF(0, mousedrawy);
                PointF p2 = new PointF(draw.draww, mousedrawy);
                float[] dashValues = { 5, 8 };
                Pen dashPen = new Pen(style.SelLine.Color);
                dashPen.DashPattern = dashValues;
                draw.AreaDrawLine(dashPen, p1, p2);

                // rectangle with selected price
                float sy = mousedrawy - style.strLabelSelFont.Size;
                p = new PointF(draw.draww +10,sy);
                size = new PointF(draw.RightOffset, style.strLabelSelFont.Size * 2f);
                draw.AreaDrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, true);
                draw.AreaDrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, false);

                p = new PointF(draw.draww + 10, sy + style.strLabelSelFont.Size * 2);
                string pricestr = Helper.PriceToString(DeNormalizePrice(mousedrawy));
                draw.AreaDrawString(style.strLabelSelBrush, style.strLabelSelFont, p, pricestr);

                // vertical
                p1 = new PointF(mousedrawx,0);
                p2 = new PointF(mousedrawx, draw.drawh);
                float[] dashValuesV = { 4, 11 };
                dashPen = new Pen(style.SelLine.Color);
                dashPen.DashPattern = dashValuesV;
                draw.AreaDrawLine(dashPen, p1, p2);

                if (selectedPriceKey != -1)
                {
                    PriceCandle priceReal = curdata[selectedPriceKey];
                    PriceCandle price = pricedata[selectedPriceKey];
                    DateTime unixtime = Helper.UnixToDateTime(price.date.ToString());
                    string strtime = unixtime.ToLocalTime().ToString("dd.MM.yyyy  HH:mm");
                    float strwidth=Helper.StringSize(style.strLabelSelFont, strtime).Width;
                    p = new PointF(mousedrawx - strwidth/2-3,0);
                    size = new PointF(strwidth+13*2, style.strLabelSelFont.Size * 2f);
                    draw.DrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, true);
                    draw.DrawRectangle(Pens.RosyBrown, Brushes.Black, p, size, false);

                    p.X = mousedrawx- strwidth / 2;
                    p.Y = -style.strLabelSelFont.Size;
                    draw.AreaDrawString(style.strLabelSelBrush, style.strLabelSelFont, p, strtime);

                    //Date+V+OHLC 
                    p.X = draw.draww /12;
                    p.Y = draw.drawh - style.strLabelFont.Size/5;
                    // string 
                    string strDescr = string.Format("{0}      VOL: {1}", strtime, ((int)priceReal.volume).ToString());
                    draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, strDescr);
                    p.X = draw.draww / 12;
                    p.Y = draw.drawh - style.strLabelFont.Size  - 8;
                    strDescr = string.Format("O: {0}    H: {1}    L: {2}    C: {3}", Helper.PriceToString(priceReal.open), Helper.PriceToString(priceReal.high), Helper.PriceToString(priceReal.low), Helper.PriceToString(priceReal.close));
                    draw.AreaDrawString(style.strLabelBrush, style.strLabelFont, p, strDescr);

                }

            }

            draw.DrawString(style.strLabelBrush, style.strLabelFont, new PointF(draw.draww +10 , draw.areaymax - style.strLabelSelFont.Size/5), chartCaption);

        }
    }
}
