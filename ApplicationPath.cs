using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BitWhiskey
{
    public class ApplicationPath
    {
        static public string directory;
        static public string directoryAppBin;
        static ApplicationPath()
        {
#if DEBUG
            directory = @"t:\DevelopNew\Crypt\BitWhiskeyBIN\";
            directoryAppBin = Path.Combine(directory, @"AppBin");
#else
            directory = Environment.CurrentDirectory.Replace(@"\bin\Debug", "");
            directoryAppBin = Path.Combine(directory, @"AppBin");
#endif

        }
    }


}
