using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NLog;


namespace BitWhiskey
{
    public class ApplicationPath
    {
        static public string directory;
        static public string directoryAppBin;
        static ApplicationPath()
        {
            directory = Environment.CurrentDirectory.Replace(@"\bin\Debug","");
            directoryAppBin = Path.Combine(directory, @"AppBin");
        }
    }

    public static class Helper
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Init()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            Global.settingsInit = SettingsInit.Load(Path.Combine(ApplicationPath.directoryAppBin, "init.json"));
            AppSettingsManager settingsManager = new AppSettingsManager();
            string settingsPath = settingsManager.GetSettingsFilePath(Global.settingsInit.currentprofile, "settings.json");
            Global.settingsMain = MySettings.Load(settingsPath);
        }
        public static void LogAndDisplay(Exception ex, string msg = null)
        {
            string dispmsg = msg;
            if (dispmsg == null)
                dispmsg = ex.Message;
            logger.Error(ex,msg);
            MessageBox.Show(dispmsg);
        }

        public static double GetFractionOnly(double num)
        {
            double x = num - Math.Truncate(num);
            return x;
        }
        public static string GetFractionOnlyString8(double num)
        {
            double x = num - Math.Truncate(num);
            return x.ToString("F8").Replace("0.","");
        }
        public static string TradeAmountToString(double num)
        {
            string frac = Helper.GetFractionOnlyString8(num);
            return Math.Truncate(num).ToString() + " ." + frac;
        }

        public static DateTime StringToDateTimeExact(string strdate,string format= "yyyy-MM-dd HH:mm:ss")
        {
            DateTime dateTime = DateTime.ParseExact(strdate, format, CultureInfo.InvariantCulture);
            return dateTime;
        }
        public static string PriceToStringBtc(double price)
        {
            string formatted = String.Format("{0:F8}", price);
            return formatted;
        }

        public static string MakeRelative(string filePath, string referencePath)
        {
            var fileUri = new Uri(filePath);
            var referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString();
        }
        public static string PriceToString(double price)
        {
            string pricestr = "";
            if (price > 1)
                pricestr = price.ToString("0.00");
            else if (price > 0.1)
                pricestr = price.ToString("0.0000");
            else
                pricestr = price.ToString("0.00000000");
            return pricestr;
        }

        public static DirectoryInfo CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                return Directory.CreateDirectory(path);
            return null;
        }
        public static int WeekNumber(DateTime d)
        {
            int w = d.DayOfYear / 7;
            if (d.DayOfYear % 7 == 0)
                return w;
            return w + 1;
        }
        public static DateTime WeekDate(int year, int week)
        {
            DateTime d = new DateTime(year, 1, 1);
            d = d.AddDays(week * 7);
            return d;
        }
        public static DateTime UnixToDateTime(this string jsonDate)
        {
            Double seconds = Convert.ToDouble(jsonDate);

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dateTime = epoch.AddSeconds(seconds);
            return dateTime;
        }
        public static DateTime UnixToDateTime(this int uxDate)
        {
            //            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime dateTime = epoch.AddSeconds(uxDate);
            return dateTime;
        }

        public static int ToUnixTimeStamp(this DateTime date)
        {
            int unixTimestamp = (int)((date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            return unixTimestamp;
        }
        public static SizeF StringSize(Graphics g, Font font, string s)
        {
            /*      using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                return graphics.MeasureString("Hello there", new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point));
            }*/
            return g.MeasureString(s, font);
        }
        public static double ToDouble(string strnumber)
        {
            return double.Parse(strnumber.Replace(",", "."));
        }
        public static bool IsDouble(string strnumber)
        {
            double value;
            return double.TryParse(strnumber.Replace(",", "."), out value);
        }
        public static double CalcSpread(double sellPrice, double buyPrice)
        {
            if (Math.Max(buyPrice, sellPrice) <= 0.00000000001)
                return double.MaxValue;

            double spread = Math.Abs(buyPrice - sellPrice);
            spread = (spread / Math.Max(buyPrice, sellPrice)) * 100;
            return spread;
        }



    }
}
