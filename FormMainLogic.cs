using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BitWhiskey
{
    /*
        public class FormMainLogic
        {
        }
    */

    public class MarketCurrentView
    {
        public string ticker { get; set; }
        public double lastPrice { get; set; }
        public double lastPriceUSD { get; set; }
        public double percentChange { get; set; }
        public double volumeBtc { get; set; }
        public double volumeUSDT { get; set; }
    }

}
