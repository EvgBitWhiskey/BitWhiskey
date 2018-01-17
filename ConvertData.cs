using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWhiskey
{
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
            int periodnum = (minute / period);
            return periodnum;
        }

        public void ProcessBar(Dictionary<int, PriceCandle> pricedata)
        {
            int period = 0;

            switch (datatype)
            {
                case ConvertDataType.BAR_15:
                    period = HourPeriodNumber(dt, 15);
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
                    lastdate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, lastperiod * 15, 0);
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
        ConvertDataType curViewType;
        public PriceData()
        {
        }
        public void SetData(Dictionary<int, PriceCandle> pricedata_, ConvertDataType datatype, ConvertDataType viewType)
        {
            origpricedata = pricedata_;
            curViewType = viewType;
            if(datatype==viewType && viewType==ConvertDataType.BAR_5)
            {
                pricedata = origpricedata;
            }
            else
              Convert(viewType);

            keys = new List<int>(pricedata.Keys);

        }
        public bool UpdatePrice(double lastPrice)
        {
            bool barAdded = false;
            PriceCandle last = pricedata.Last().Value;
            int lastPriceDateUnix = pricedata.Keys.Max();
            DateTime lastPriceDate = Helper.UnixToDateTime(lastPriceDateUnix);

            int unixPeriod = 0;
            switch (curViewType)
            {
                case ConvertDataType.BAR_5:
                    unixPeriod = 5 * 60;
                    break;
                case ConvertDataType.BAR_15:
                    unixPeriod = 15 * 60;
                    break;
                case ConvertDataType.BAR_HOUR:
                    unixPeriod = 60 * 60;
                    break;
                case ConvertDataType.BAR_DAY:
                    unixPeriod = 24 * 60 * 60;
                    break;
                case ConvertDataType.BAR_WEEK:
                    unixPeriod = 7 * 24 * 60 * 60;
                    break;
                case ConvertDataType.BAR_MONTH:
                    unixPeriod = DateTime.DaysInMonth(lastPriceDate.Year, lastPriceDate.Month) * 24 * 60 * 60;
                    break;
            }


            int curDateUnix = Helper.ToUnixTimeStamp(DateTime.UtcNow);
            if (curDateUnix >= lastPriceDateUnix + unixPeriod)
            { //create new bar
                PriceCandle newbar = new PriceCandle();
                newbar.close = lastPrice;
                newbar.open = last.close;
                newbar.high = Math.Max(last.close, lastPrice);
                newbar.low = Math.Min(last.close, lastPrice);
                newbar.volume = 666;
                newbar.date = lastPriceDateUnix + unixPeriod;
                pricedata.Add(newbar.date, newbar);
                barAdded = true;

            }
            // else
            { //update last bar
                double newClose = lastPrice;// + 100;
                last.close = newClose;
                if (last.high < newClose)
                    last.high = newClose;
                if (last.low > newClose)
                    last.low = newClose;
            }

            keys = new List<int>(pricedata.Keys);
            return barAdded;
        }

        public void Convert(ConvertDataType viewType)
        {
            List<int> keys15;
            keys15 = new List<int>(origpricedata.Keys);

            pricedata = new Dictionary<int, PriceCandle>();
            DateTime dt;
            PriceCandle lastprice15 = new PriceCandle();
            ConvertData cdata = new ConvertData(viewType);

            for (int n = 0; n < keys15.Count; n++)
            {
                if (n == 486)
                { }
                dt = Helper.UnixToDateTime(keys15[n].ToString());
                cdata.prevclose = lastprice15.close;
                lastprice15 = origpricedata[keys15[n]];

                cdata.SetCurBar(dt, lastprice15);
                cdata.ProcessBar(pricedata);
            }
            cdata.prevclose = lastprice15.close;
            cdata.AddPrice(pricedata);
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
        public void NormalizeData(double minprice, double yzoomcoeff, double minvol, double volzoomcoeff)
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
                pnew.volume = (pnew.volume - minvol) / volzoomcoeff;
                //                pnew.date = key;
                normpricedata.Add(key, pnew);
            }
        }

    }



}
