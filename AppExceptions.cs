using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWhiskey
{
    public class MarketAPIException : Exception
    {
        public MarketAPIException() : base()
        {
        }
        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public MarketAPIException(String message): base(message)
        {
        }

    }


}
