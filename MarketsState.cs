using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWhiskey
{
    public class MarketsState
    {
        public Dictionary<string, List<MarketCurrentView>> curMarkets = new Dictionary<string, List<MarketCurrentView>>();

        public void Init()
        {
            foreach (var market in Global.markets.GetMarketList())
                curMarkets.Add(market.ToString(),null);
        }
        public void Update(string market, List<MarketCurrentView> curMarket)
        {
            curMarkets[market] = curMarket;
        }
    }
}
