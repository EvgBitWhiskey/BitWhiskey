using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Drawing;
using System.Windows.Forms;

namespace BitWhiskey
{
    public class Logman
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogAndDisplay(Exception ex, string msg = null)
        {
            string dispmsg = msg;
            if (dispmsg == null)
                dispmsg = ex.Message;
            logger.Error(ex, msg);
            MessageBox.Show(dispmsg);
        }
        public static void LogAndDisplay(string msg)
        {
            logger.Error(msg);
            MessageBox.Show(msg);
        }
        public static void Log(Exception ex, string msg = null)
        {
            logger.Error(ex, msg);
        }
        public static void Log(string msg)
        {
            logger.Error(msg);
        }

    }
}
