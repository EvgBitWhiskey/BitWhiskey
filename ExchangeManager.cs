using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWhiskey
{
    public class ExchangeManager
    {
        private List<string> marketList = new List<string>();

        public ExchangeManager()
        {
        }
        public void Init()
        {
            Market market=GetMarketByMarketName("Poloniex");
            if(market.IsEnabled())
              marketList.Add("Poloniex");

            market = GetMarketByMarketName("Bittrex");
            if (market.IsEnabled())
                marketList.Add("Bittrex");

            market = GetMarketByMarketName("Yobit");
            if (market.IsEnabled())
                marketList.Add("Yobit");

        }
        public List<string> GetMarketList()
        {
            return marketList;
        }
        public static Market GetMarketByMarketName(string marketName)
        {
            switch (marketName)
            {
                case "Poloniex":
                    return new Poloniex();
                case "Bittrex":
                    return new Bittrex();
                case "Yobit":
                    return new Yobit();
            };

            return null;
        }


    }


}
