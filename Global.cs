using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Configuration;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Threading;
using System.Media;

namespace BitWhiskey
{

  public static class Global
  {
        public static Dictionary<int, Alert> alerts=new Dictionary<int, Alert>();
        public static MySettings settingsMain;
        public static SettingsInit settingsInit;
        public static TaskScheduler uiScheduler;
        public static ExchangeManager markets = new ExchangeManager();
        public static MarketsState marketsState = new MarketsState();
        public static SoundPlayer player=null;

        public static double GetCurrentPrice(string market, string ticker)
        {
            List<MarketCurrentView> prices = Global.marketsState.curMarkets[market];
            string finder = ticker;
            MarketCurrentView price = prices.Single(s => s.ticker == finder);
            return price.origPrice;
        }

    }


}
