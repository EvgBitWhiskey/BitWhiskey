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

        DataGridViewExWrapper gv;

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
               CalcTotalBalance(totalBalances);
               CreateBalancesGridView(totalBalances);
                requestBalancesStart = false;
            }


        }
        /*
        public void CalcTotalBalance(List<ExchangeBalance> balances)
        {
            double totalBtc = 0;
//            Dictionary<string, bool> currencyCounted = new Dictionary<string, bool>();
            double pairPrice=0;
            double BtcInUsdPrice = 0;

            foreach (ExchangeBalance balance in balances)
            {
                pairPrice = 0;

                string btcpair ="";
                if (balance.currency == "USDT")
                    btcpair = balance.currency + "_BTC";
                else if (balance.currency == "BTC")
                {
                    pairPrice = 1;
                }
                else
                    btcpair = "BTC_" + balance.currency;

                //if (currencyCounted.ContainsKey(btcpair))
                //    continue;

                if (pairPrice == 0)
                {
                    foreach (KeyValuePair<string, List<MarketCurrentView>> curMarket in Global.marketsState.curMarkets)
                    {
                        if (curMarket.Value == null)
                            continue;
                        foreach (MarketCurrentView curpair in curMarket.Value)
                        {
                            if (curpair.ticker != btcpair)
                                continue;
                            pairPrice = curpair.origPrice;
                            break;
                        }
                        if (pairPrice != 0)
                            break;
                    }
                }
                if (pairPrice != 0)
                {
                    if (balance.currency == "USDT")
                    {
                        BtcInUsdPrice = pairPrice;
                        pairPrice = 1.0 / pairPrice;
                    }
                    totalBtc += pairPrice * balance.balance;
                }

            }

            if (totalBtc!=0)
            {
                labelBalanceBaseValue.Text = Helper.PriceToStringBtc(totalBtc);
                double totalUsd = totalBtc * BtcInUsdPrice;
                labelBalanceUsd.Text ="=  "+Helper.PriceToStringFinance(totalUsd)+" $";
            }

        }
        */
        public void CalcTotalBalance(List<ExchangeBalance> balances)
        {
            foreach (ExchangeBalance balance in balances)
            {
                string btcpair = "";
                balance.balanceFound = false;
                if (balance.currency == "USDT" || balance.currency == "USD" || balance.currency == "RUR")
                    btcpair = balance.currency + "_BTC";
                else if (balance.currency == "BTC")
                {
                    balance.balanceFound = true;
                    balance.price = 1;
                }
                else
                    btcpair = "BTC_" + balance.currency;

                if (btcpair != "")
                {
                    foreach (KeyValuePair<string, List<MarketCurrentView>> curMarket in Global.marketsState.curMarkets)
                    {
                        if (curMarket.Value == null)
                            continue;
                        MarketCurrentView pair = curMarket.Value.FirstOrDefault(x => x.ticker == btcpair);
                        if (pair == null)
                            continue;
                        balance.balanceFound = true;
                        balance.price = pair.origPrice;
                        break;
                    }
                }
            }

            double BtcInUsdPrice = 0;

            foreach (KeyValuePair<string, List<MarketCurrentView>> curMarket in Global.marketsState.curMarkets)
            {
                if (curMarket.Value == null)
                    continue;
                MarketCurrentView pair = curMarket.Value.FirstOrDefault(x => x.ticker == "USDT_BTC");
                if (pair != null)
                {
                    BtcInUsdPrice = pair.origPrice;
                    break;
                }
                pair = curMarket.Value.FirstOrDefault(x => x.ticker == "USD_BTC");
                if (pair != null)
                    BtcInUsdPrice = pair.origPrice;
            }


            double totalBtc = 0;
            foreach (ExchangeBalance balance in balances)
            {
                balance.btcBalance = 0;
                if (balance.exchangeName == "Yobit")
                {
                    if (balance.currency == "USD" && BtcInUsdPrice!=0)
                        balance.btcBalance = (1.0 / BtcInUsdPrice) * balance.balance;
                    else if (balance.currency == "RUR" && BtcInUsdPrice != 0)
                        balance.btcBalance = (1.0 / (BtcInUsdPrice*56)) * balance.balance;
                    else
                        balance.btcBalance = balance.price * balance.balance;
                }
                else
                {
                    if (balance.currency == "USDT" || balance.currency == "USD" || balance.currency == "RUR" && balance.price!=0)
                        balance.btcBalance = (1.0 / balance.price) * balance.balance;
                    else
                        balance.btcBalance = balance.price * balance.balance;
                }

                balance.usdBalance = balance.btcBalance* BtcInUsdPrice;
                totalBtc += balance.btcBalance;
            }


            if (totalBtc != 0)
            {
                labelBalanceBaseValue.Text = Helper.PriceToStringBtc(totalBtc);
                double totalUsd = totalBtc* BtcInUsdPrice;
                labelBalanceUsd.Text = "=  " + ((int)totalUsd)+ " $";// Helper.PriceToStringFinance(totalUsd) + " $";
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
                btcBalance = Helper.PriceToStringBtc(item.btcBalance),
                usdBalance = ((int)item.usdBalance).ToString()
            }).OrderBy(p => p.exchangeName).ThenBy(p => p.currency).ToList();

            List<GVColumn> columns = new List<GVColumn>()
            {
                new GVColumn( "exchangeName", "Exchange","string") ,
                new GVColumn( "currency", "Currency","string") ,
                new GVColumn( "balance", "Balance","string"),
                new GVColumn( "btcBalance", "BTC Balance","string"),
                new GVColumn( "usdBalance", "USD Balance","string")
            };
            if (gv == null)
                gv = new DataGridViewExWrapper();
            gv.Create(dgridBalance, dataView, columns, true);
            DataGridViewCellStyle styleBTC = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Regular), ForeColor = Color.OrangeRed };
            DataGridViewCellStyle styleUSD = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Regular), ForeColor = Color.Green };
            gv.SetColumnStyle("btcBalance", styleBTC);
            gv.SetColumnStyle("usdBalance", styleUSD);

        }

    }
}
