using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace BitWhiskey
{
    public partial class Form1 : Form
    {
        public TradeRequestHandlers tradeHandlers;
        DataGridViewWrapper gvMarkets;
        private AppTimer timerMarkets;

        public Form1()
        {
            InitializeComponent();
            Helper.Init();

            Global.uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            toolStripComboBoxMarket.Items.Clear();
            foreach (var market in Global.markets.GetMarketList())
                toolStripComboBoxMarket.Items.Add(market.ToString());

            RequestConsumer.requestManager.Create(Global.markets.GetMarketList());
            RequestConsumer.CreateRequestThreads(Global.markets.GetMarketList());

            timerMarkets = new AppTimer(45000, TimerMarkets_Tick,this);
            toolStripComboBoxMarket.SelectedIndex = 0;
        }
        private void toolStripComboBoxMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgridMarkets.DataSource = null;
            this.ActiveControl = dgridMarkets;
            timerMarkets.Stop();
            LoadTickers();
        }
        private void LoadTickers()
        {
            toolDropDownTickerBtc.DropDown.Items.Clear();
            toolDropDownTickerUsdt.DropDown.Items.Clear();
            toolDropDownTickerBtc.Enabled = false;
            toolDropDownTickerUsdt.Enabled = false;

           // if (!ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text).HaveKey())
           //     return;

            Market market = ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text);
            tradeHandlers = new TradeRequestHandlers(market);

            RequestItemGroup itemgroup = new RequestItemGroup(LoadTickers_UIResultHandler);
            itemgroup.AddItem(market.GetTradePairsBegin(), tradeHandlers.GetTradePairs_RequestHandler);
            RequestConsumer.requestManager.AddItemGroup(market.MarketName(), itemgroup);

            UpdateMarkets();
        }
        private void UpdateMarkets()
        {
            Market market = ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text);
            tradeHandlers = new TradeRequestHandlers(market);
            RequestItemGroup itemgroup = new RequestItemGroup(GetMarketCurrent_UIResultHandler);
            itemgroup.AddItem(market.GetMarketCurrentBegin(), tradeHandlers.GetMarketCurrent_RequestHandler);
            RequestConsumer.requestManager.AddItemGroup(market.MarketName(), itemgroup);
        }
        public void LoadTickers_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (Helper.IsResultHasErrors(resultResponse))
                return;
            Dictionary<string, TradePair> tradePairs = (Dictionary<string, TradePair>)resultResponse.items[0].result.resultData;

            //Global.market.tradePairs = new Dictionary<string, TradePair>();
            //Global.market.tradePairs.Add("BTC_LTC",new TradePair{currency1 = "BTC",currency2 = "LTC",ticker = "BTC_LTC",isActive =true});

            int btctickerCount = 0;
            foreach (KeyValuePair<string, TradePair> pair in tradePairs)
            {
                if (pair.Key.StartsWith("BTC"))
                    btctickerCount++;
            }

            ToolStripDropDown drop_downbtc = new ToolStripDropDown();
            drop_downbtc.LayoutStyle = ToolStripLayoutStyle.Table;
            //            drop_downbtc.
            ((TableLayoutSettings)drop_downbtc.LayoutSettings).ColumnCount = btctickerCount / 23 + 1;
            toolDropDownTickerBtc.DropDown = drop_downbtc;

            ToolStripDropDown drop_downusdt = new ToolStripDropDown();
            drop_downusdt.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)drop_downusdt.LayoutSettings).ColumnCount = (tradePairs.Count - btctickerCount) / 23 + 1;
            toolDropDownTickerUsdt.DropDown = drop_downusdt;

            foreach (KeyValuePair<string, TradePair> pair in tradePairs.OrderBy(p => p.Key))
            {
                if (pair.Key.StartsWith("BTC"))
                    toolDropDownTickerBtc.DropDown.Items.Add(pair.Key);
                else //if (pair.Key.StartsWith("USDT"))
                    toolDropDownTickerUsdt.DropDown.Items.Add(pair.Key);
            }

            if (ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text).HaveKey())
            {
                toolDropDownTickerBtc.Enabled = true;
                toolDropDownTickerUsdt.Enabled = true;
            }
            toolStripButtonChart.Enabled = true;
        }
        public void GetMarketCurrent_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (Helper.IsResultHasErrors(resultResponse))
                return;
            Dictionary<string, MarketCurrent> currenciesDict = (Dictionary<string, MarketCurrent>)resultResponse.items[0].result.resultData;

            List<MarketCurrentView> currencies = currenciesDict.Values.Select(item => new MarketCurrentView
            {
                ticker = item.ticker,
                lastPrice = item.lastPrice,
                lastPriceUSD = 0,
                percentChange = item.percentChange,
                volumeBtc = item.volumeBtc,
                volumeUSDT = item.volumeUSDT
            }).ToList();

            if (currenciesDict.ContainsKey("USDT_BTC"))
            {
                double btcPrice = currenciesDict["USDT_BTC"].lastPrice;
                foreach (var item in currencies)
                {
                    if (item.ticker.StartsWith("BTC"))
                        item.volumeUSDT = item.volumeBtc * btcPrice;

                    if (item.ticker.StartsWith("BTC"))
                        item.lastPriceUSD = item.lastPrice * btcPrice;
                    else if (item.ticker.StartsWith("USDT"))
                    {
                        double usdprice = item.lastPrice;
                        item.lastPriceUSD = usdprice;
                        item.lastPrice = item.lastPrice/ btcPrice;
                    }
                }

            }
           currencies = currencies.OrderByDescending(x => x.volumeUSDT).ToList();

            var dataView = currencies.Select(item => new
            {
                ticker = item.ticker,
                lastPrice = Helper.PriceToStringBtc(item.lastPrice),
                lastPriceUSDT =item.lastPriceUSD.ToString("N3")+" $",
                percentChange = item.percentChange.ToString("0") + " %",
                volumeBtc = item.volumeBtc.ToString("N2"),
                volumeUSDT = item.volumeUSDT.ToString("N0")+ " $"
            }).ToList();
            List<DGVColumn> columns = new List<DGVColumn>()
            {
                new DGVColumn( "ticker", "Currency","string") ,
                new DGVColumn( "lastPrice", "Price BTC","string") ,
                new DGVColumn( "lastPriceUSDT", "Price $","string") ,
                new DGVColumn( "percentChange", "Change %","string") ,
                new DGVColumn( "volumeBtc", "Volume BTC","string"),
                new DGVColumn( "volumeUSDT", "Volume $","string")
            };
            gvMarkets = new DataGridViewWrapper(dgridMarkets, true);
            gvMarkets.Create(dataView, columns);
            DataGridViewCellStyle styleTicker = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Bold), ForeColor = Color.Black };
            DataGridViewCellStyle stylePrice = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Regular), ForeColor = Color.Black };
            gvMarkets.SetColumnStyle("ticker", styleTicker);
            gvMarkets.SetColumnStyle("lastPrice", stylePrice);
            gvMarkets.SetColumnStyle("lastPriceUSDT", stylePrice);
            gvMarkets.AutoSizeFillExcept("volumeUSDT");
            //            gv.RowColorByCondition("orderType", (string s) => { return s == "1"; }, Color.LightPink);

            timerMarkets.Start();

        }
        private void dgridMarkets_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gvMarkets.RowColorByCondition("percentChange", s => { return Helper.ToDouble(s.Replace("%", "")) < 0; }, Color.MistyRose);
        }
        private void TimerMarkets_Tick(object sender, ElapsedEventArgs e)
        {
            timerMarkets.Stop();
            UpdateMarkets();
        }

        private void listBoxTicker_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ShowTradeForm(string ticker)
        {
            FormTrade form = new FormTrade(ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text), ticker);
            form.parent = this;
            form.Show();
        }
        private void ShowChartForm()
        {
            FormChart form = new FormChart(ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text));
            form.parent = this;
            form.Show();
        }
        private void toolDropDownTickerBtc_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ShowTradeForm(e.ClickedItem.Text);
        }

        private void toolDropDownTickerUsdt_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ShowTradeForm(e.ClickedItem.Text);
        }

        private void toolStripButtonChart_Click(object sender, EventArgs e)
        {
            ShowChartForm();
        }
        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            FormSettings form = new FormSettings();
            form.parent = this;
            form.ShowDialog();
        }

        private void toolStripButtonBalance_Click(object sender, EventArgs e)
        {
            FormBalance form = new FormBalance();
            form.parent = this;
            form.ShowDialog();
        }

    }
}
