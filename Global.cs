using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Configuration;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Threading;

namespace BitWhiskey
{

  public static class Global
  {
        public static MySettings settingsMain;
        public static SettingsInit settingsInit;
        public static TaskScheduler uiScheduler;
        public static ExchangeManager markets = new ExchangeManager();
    }


}
