using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using Newtonsoft.Json;


namespace BitWhiskey
{
    public class CustomDateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    { public CustomDateTimeConverter() { base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss"; } }


    public class AppSettings<T> where T : new()
    {
        public void Save(string filePath)
        {
            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(this, new CustomDateTimeConverter()));
        }
        public static T Load(string filePath)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath), new CustomDateTimeConverter());
       }
    }
    public class AppSettingsManager 
    {
        public string pathProfiles;
        public string pathStandart ;
        public static string fileAlert;
        public static string pathAlert ;

        public AppSettingsManager()
        {
            pathProfiles = Path.Combine(ApplicationPath.directory, @"AppBin\Profiles");
            fileAlert = "alert.json";
            pathAlert = Path.Combine(ApplicationPath.directoryAppBin, fileAlert);
        }
        public static void LoadSettings()
        {
            Global.settingsInit = SettingsInit.Load(Path.Combine(ApplicationPath.directoryAppBin, "init.json"));
            AppSettingsManager settingsManager = new AppSettingsManager();
            string settingsPath = settingsManager.GetSettingsFilePath(Global.settingsInit.currentprofile, "settings.json");
            Global.settingsMain = MySettings.Load(settingsPath);

            if(File.Exists(pathAlert))
              AlertManager.settingsAlert = SettingsAlert.Load(pathAlert);
            else
                AlertManager.settingsAlert = new SettingsAlert();
            AlertManager.alerts= AlertManager.settingsAlert.alerts;
        }

        public string  GetProfileDir(string profileName)
        {
            return Path.Combine(pathProfiles, profileName);
        }
        public void CreateProfileDir(string profileName)
        {
            string newProfilePath=Path.Combine(pathProfiles, profileName);
            Directory.CreateDirectory(newProfilePath);
        }
        public string GetSettingsFilePath(string profileName, string fileName)
        {
            return Path.Combine(GetProfileDir(profileName), fileName);
        }
        public string GetStandartProfileDir()
        {
            return Path.Combine(pathProfiles,"standart");
        }
        public List<string> GetProfiles()
        {
           return Directory.EnumerateDirectories(pathProfiles).Select(d => new DirectoryInfo(d).Name).ToList(); 
        }
    }

    public class MySettings : AppSettings<MySettings>
    {
        public string poloniexkey = "";
        public string poloniexsecret = "";
        public bool   poloniexdisabled = false;
        public string bittrexkey = "";
        public string bittrexsecret = "";
        public bool   bittrexdisabled = false;
        public string yobitkey = "";
        public string yobitsecret = "";
        public bool yobitdisabled = false;
        public bool defaultlimitorders = true;
    }
    public class SettingsInit : AppSettings<SettingsInit>
    {
        public string currentprofile = "Standart";
//        public string alertSettingsFile = "Standart";
    }
    public class SettingsAlert : AppSettings<SettingsAlert>
    {
        public Dictionary<int, Alert> alerts = new Dictionary<int, Alert>();
    }
}
