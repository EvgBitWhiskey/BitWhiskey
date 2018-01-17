using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BitWhiskey
{
    public class Alert
    {
        private static int IdCounter=0;
        public int id;
        public string caption;
        public string tickerPair;
        public DateTime createDate;
        public double createPrice;

        public double priceAlert;

        public bool showInChart;
        public bool playSound;

        public bool alertExecute;

        public Alert(string caption_)
        {
            IdCounter++;
            id = IdCounter;

            caption = caption_;
            createDate = DateTime.Now;
            showInChart = true;
            alertExecute = false;
        }
        public static int GetNewId()
        {
            return IdCounter + 1;
        }

    }


}
