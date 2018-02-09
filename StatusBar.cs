using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitWhiskey
{
    public static class StatusBar
    {
        static ToolStripStatusLabel statusLabelDate;
        static ToolStripStatusLabel statusLabelMain;

        public static void Init(ToolStripStatusLabel statusLabelDate_, ToolStripStatusLabel statusLabelMain_)
        {
            statusLabelDate= statusLabelDate_;
            statusLabelMain = statusLabelMain_;
            statusLabelDate.Text = "";
            statusLabelMain.Text = "";
        }
        public static void ShowMsg( string msg)
        {
            statusLabelDate.Text =DateTime.Now.ToString("HH:mm:ss"); 
            statusLabelMain.Text = msg;
        }

    }


}
