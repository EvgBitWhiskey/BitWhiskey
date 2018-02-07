using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWhiskey
{
 /*   public class ExchangeInfo
    {
        public bool enabled;
    }
    */
        public class MarketsState
    {
      //  public Dictionary<string, ExchangeInfo> marketsInfo = new Dictionary<string, ExchangeInfo>();
        public Dictionary<string, List<MarketCurrentView>> curMarkets = new Dictionary<string, List<MarketCurrentView>>();
        public Dictionary<string, Dictionary<string, TradePair>> curMarketPairs = new Dictionary<string, Dictionary<string, TradePair>>();

        public void Init()
        {
            foreach (var market in Global.markets.GetMarketList())
            {
                curMarkets.Add(market.ToString(), null);
                curMarketPairs.Add(market.ToString(), null);
            }
        }
        public void SetPairs(string market, Dictionary<string, TradePair> pairs)
        {
            curMarketPairs[market] = pairs;
        }
        public void Update(string market, List<MarketCurrentView> curMarket)
        {
            curMarkets[market] = curMarket;
        }
    }
}
