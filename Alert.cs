using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Media;
using System.IO;


namespace BitWhiskey
{
    public class Alert
    {
        //private static int IdCounter=0;
        public int id;
        public string market;
        public string caption;
        public string tickerPair;
        public DateTime createDate;
        public double createPrice;

        public double priceAlert;

        public bool showInChart;
        public bool playSound;

        public bool alertExecute;
        public bool enabled;

        public Alert(string caption_)
        {
            //IdCounter++;
           
            id = GetNewId();

            caption = caption_;
            createDate = DateTime.Now;
            showInChart = true;
            alertExecute = false;
            enabled = true;
        }
        public static int GetNewId()
        {
            int newid = 0;
            if (AlertManager.alerts.Count() == 0)
                newid = 1;
            else
                newid = AlertManager.alerts.Max(x => x.Key) + 1;

            return newid;
        }

    }


    public static class AlertManager
    {
        public static SettingsAlert settingsAlert;
        public static Dictionary<int, Alert> alerts = new Dictionary<int, Alert>();
        public static SoundPlayer player = null;


        public static void CheckAlerts()
        {
            foreach (Alert alert in alerts.Values)
            {
                if (alert.alertExecute)
                    continue;
                if (!alert.enabled)
                    continue;
                double price = Global.GetCurrentPrice(alert.market, alert.tickerPair);
                if (alert.priceAlert > alert.createPrice)
                {
                    if (alert.priceAlert <= price)
                        alert.alertExecute = true;
                }
                else
                {
                    if (alert.priceAlert >= price)
                        alert.alertExecute = true;
                }
            }
/*
            foreach (Alert alert in alerts.Values)
            {
                if (alert.alertExecute)
                {
                    string soundfile = Path.Combine(Path.Combine(ApplicationPath.directory, @"AppBin"), "alert.wav");
                    player = new SoundPlayer(soundfile);
                    player.PlayLooping();
                    break;
                }
            }
*/
            int count = alerts.Values.Count(x => x.alertExecute == true && x.enabled == true && x.playSound == true);
            if (count > 0)
            {
                if (player == null)
                {
                    string soundfile = Path.Combine(Path.Combine(ApplicationPath.directory, @"AppBin"), "alert.wav");
                    player = new SoundPlayer(soundfile);
                    player.Tag = false;
                }
                if ((bool)player.Tag == false)
                {
                    player.PlayLooping();
                    player.Tag = true;
                }
            }
            else
            {
                PlayerStop();
            }

            //            SystemSounds.Asterisk. Playloo();

        }

        public static void SaveAlerts()
        {
            settingsAlert.alerts = alerts;
            settingsAlert.Save(AppSettingsManager.pathAlert);
        }
        public static void Remove(int alertid)
        {
            alerts.Remove(alertid);
            SaveAlerts();
        }
        public static void PlayerStop()
        {
            if (player != null)
            {
                player.Stop();
                player.Tag = false;
            }
        }
        public static void RemoveAll()
        {
            alerts.Clear();
            SaveAlerts();
            PlayerStop();
        }
        public static void ToggleAll()
        {
            int count = alerts.Values.Count(x => x.enabled == false);
            bool enableAlert = false;
            if (count == alerts.Count)
                enableAlert = true;

            foreach (Alert alert in alerts.Values)
            {
                alert.enabled = enableAlert;
                if(!alert.enabled)
                  alert.alertExecute = false;
            }
            SaveAlerts();
            if (count == alerts.Count)
                PlayerStop();
        }

    }


}
