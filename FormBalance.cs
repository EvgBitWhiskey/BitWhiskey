using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BitWhiskey
{
    public partial class FormBalance : Form
    {
        public Form parent;
        bool requestBalancesStart = false;
        Dictionary<string, List<Balance>> marketBalance;
        List<string> marketFilter = new List<string>();

        public FormBalance()
        {
            InitializeComponent();

            comboBoxExchange.Items.Add("All");
            foreach (var market in Global.markets.GetMarketList())
            {
                if(ExchangeManager.GetMarketByMarketName(market.ToString()).HaveKey())
                  comboBoxExchange.Items.Add(market.ToString());
            }
            comboBoxExchange.SelectedIndex = 0;
        }

        private void comboBoxExchange_SelectedIndexChanged(object sender, EventArgs e)
        {
            marketFilter = new List<string>();
            foreach (var market in comboBoxExchange.Items)
            {
                if (comboBoxExchange.Text == "All" || comboBoxExchange.Text == market.ToString())
                {
                    if (market.ToString() != "All" )
                    {
                        marketFilter.Add(market.ToString());
                    }
                }
            }
            UpdateBalances(marketFilter);
        }
        private void buttonUpdateBalances_Click(object sender, EventArgs e)
        {
            UpdateBalances(marketFilter);
        }
        private void UpdateBalances(List<string> marketFilter)
        {
            if (requestBalancesStart)
                return;
            if (marketFilter.Count == 0)
                return;
            requestBalancesStart = true;
            marketBalance = new Dictionary<string, List<Balance>>();
            dgridBalance.DataSource = null;

            foreach (var itemMarket in marketFilter)
            {
                RequestItemGroup itemgroup = new RequestItemGroup(UpdateBalances_UIResultHandler);
                Market market = ExchangeManager.GetMarketByMarketName(itemMarket);
                TradeRequestHandlers tradeHandlers = new TradeRequestHandlers(market);
                itemgroup.AddItem(market.GetBalancesBegin(), tradeHandlers.GetBalances_RequestHandler);
                RequestConsumer.requestManager.AddItemGroup(market.MarketName(), itemgroup);
            }
            /*
            foreach (var itemMarket in marketFilter)
            {
                RequestItemGroup igroup = new RequestItemGroup(UpdateBala_UIRndler);
                Market imarket = ExchangeManager.GetMarketByMarketName("Bittrex");
                TradeRequestHandlers itradeHandlers = new TradeRequestHandlers(imarket);
                igroup.AddItem(imarket.GetBalan(), itradeHandlers.GetBala);
                RequestConsumer.requestManager.AddItemGroup(imarket.MarketName(), igroup);
            }
            */
        }

        public void UpdateBalances_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            
            Dictionary<string, Balance> balancesDict = (Dictionary<string, Balance>)resultResponse.items[0].result.resultData;
            List<Balance> balances = balancesDict.Values.ToList();
            balances = balances.Where(i => i.balance != 0).ToList();
            marketBalance.Add(resultResponse.market, balances);

            if (marketBalance.Count == marketFilter.Count)
            {
                //            labelBalanceBaseValue.Text = Helper.PriceToStringBtc(tradeLogic.balanceBase.balance);
                //            TradeLast lastbtcInUsdtPrice = (TradeLast)resultResponse.items[2].result.resultData;
                //            double btcInUsd = (lastbtcInUsdtPrice.bid + lastbtcInUsdtPrice.ask) / 2;
                //                averageUsdPrice = averageBtcPrice * btcInUsd;
                //            labelAverage.Text = "Average: " + Helper.PriceToStringBtc(averageBtcPrice) + " = " + Helper.PriceToString(averageUsdPrice) + "$";
               List<ExchangeBalance> totalBalances =ConvertBalancesToList();
               CreateBalancesGridView(totalBalances);
               requestBalancesStart = false;
            }


        }
        public List<ExchangeBalance> ConvertBalancesToList()
        {
            List<ExchangeBalance> balances = new List<ExchangeBalance>();

            foreach (var pairMarket in marketBalance)
            {
                string market = pairMarket.Key;
                foreach (var itemBalance in pairMarket.Value)
                {
                    ExchangeBalance b = new ExchangeBalance() { exchangeName = market, currency = itemBalance.currency, balance = itemBalance.balance };
                    balances.Add(b);
                }

            }

            return balances;
        }

        public void CreateBalancesGridView(List<ExchangeBalance> balances)
        {
            var dataView = balances.Select(item => new
            {
                exchangeName = item.exchangeName,
                currency = item.currency,
                balance = Helper.PriceToStringBtc(item.balance),
            }).OrderBy(p => p.exchangeName).ThenBy(p => p.currency).ToList();
            List<DGVColumn> columns = new List<DGVColumn>()
            {
                new DGVColumn( "exchangeName", "Exchange","string") ,
                new DGVColumn( "currency", "Currency","string") ,
                new DGVColumn( "balance", "Balance","string") 
            };
            DataGridViewWrapper gv = new DataGridViewWrapper(dgridBalance, true);
            gv.Create(dataView, columns);
            gv.AutoSizeDisplayedExcept("balance");

        }

    }
}
