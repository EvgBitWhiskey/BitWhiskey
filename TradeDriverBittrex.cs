using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;

namespace BitWhiskey
{
    public class TradeDriverBittrex : TradeDriver
    {
        public TradeDriverBittrex( string ticker_): base(ticker_)
        {
            market = new Bittrex();
        }
    }
}
