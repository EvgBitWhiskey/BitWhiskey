using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Media;
using System.Drawing;
using System.Linq;
using System.Net;
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
        DataGridViewExWrapper gvMarkets;
        private AppTimer timerMarkets;
        private AppTimer timerCheckAlerts;
        private bool marketsLoaded = false;
        //        private AppTimer timerAlerts;

//        Dictionary<string, TradePair> tradePairs;


        public Form1()
        {
            InitializeComponent();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            labelInfo.Text = "Please wait..Loading";
            Helper.Init();
            AppSettingsManager.LoadSettings();

            Global.uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Global.marketsState.Init();

            toolStripComboBoxMarket.Items.Clear();
            foreach (var market in Global.markets.GetMarketList())
            {
                toolStripComboBoxMarket.Items.Add(market.ToString());
            }
            RequestConsumer.requestManager.Create(Global.markets.GetMarketList());
            RequestConsumer.CreateRequestThreads(Global.markets.GetMarketList());

            timerMarkets = new AppTimer(26000, TimerMarkets_Tick,this);
            timerCheckAlerts = new AppTimer(5000, TimerCheckAlerts_Tick, this);
            timerCheckAlerts.Start();
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
            //toolDropDownTickerBtc.DropDown.Items.Clear();
            //toolDropDownTickerUsdt.DropDown.Items.Clear();
            //toolDropDownTickerBtc.Enabled = false;
            //toolDropDownTickerUsdt.Enabled = false;
            toolButtonTickerBtc.Enabled = false;
            toolButtonTickerUsd.Enabled = false;
            toolStripButtonChart.Enabled = false;
            toolStripButtonAlerts.Enabled = false;
            toolStripComboBoxMarket.Enabled = false;
            toolStripButtonBalance.Enabled = false;

            // if (!ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text).HaveKey())
            //     return;

            Market market = ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text);
            tradeHandlers = new TradeRequestHandlers(market);

            /*
            RequestItemGroup itemgroup = new RequestItemGroup(LoadTickers_UIResultHandler);
            itemgroup.AddItem(market.GetTradePairsBegin(), tradeHandlers.GetTradePairs_RequestHandler);
            RequestConsumer.requestManager.AddItemGroup(market.MarketName(), itemgroup);
            */

            UpdateMarkets();
        }
        private void UpdateMarket(string marketName)
        {
            Market market = ExchangeManager.GetMarketByMarketName(marketName);
            tradeHandlers = new TradeRequestHandlers(market);
            RequestItemGroup itemgroup = new RequestItemGroup(GetMarketCurrent_UIResultHandler);
            itemgroup.AddItem(market.GetMarketCurrentBegin(), tradeHandlers.GetMarketCurrent_RequestHandler);
            RequestConsumer.requestManager.AddItemGroup(market.MarketName(), itemgroup);
        }
        private void UpdateMarkets()
        {
            if (marketsLoaded)
                UpdateMarket(toolStripComboBoxMarket.Text);
            else
            {
                foreach (var marketName in Global.markets.GetMarketList())
                    UpdateMarket(marketName);
            }
        }
        public void LoadTickers_UIResultHandler(RequestItemGroup resultResponse)
        {

            /*
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
            */
/*
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            Dictionary<string, TradePair> pairs = (Dictionary<string, TradePair>)resultResponse.items[0].result.resultData;

            tradePairs = new Dictionary<string, TradePair>();

            foreach (KeyValuePair<string, TradePair> pair in pairs)
            {
                tradePairs.Add(pair.Key, pair.Value.Copy());
            }

            if (ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text).HaveKey())
            {
                toolButtonTickerBtc.Enabled = true;
                toolButtonTickerUsd.Enabled = true;
//                toolDropDownTickerBtc.Enabled = true;
//                toolDropDownTickerUsdt.Enabled = true;
                toolStripButtonAlerts.Enabled = true;
            }
            toolStripButtonChart.Enabled = true;
            labelInfo.Text = "";
            */
        }
        public void GetMarketCurrent_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            Dictionary<string, MarketCurrent> currenciesDict = (Dictionary<string, MarketCurrent>)resultResponse.items[0].result.resultData;

            //            Dictionary<string, TradePair> pairs = (Dictionary<string, TradePair>)resultResponse.items[0].result.resultData;

            if (!marketsLoaded)
            {
                if (Global.marketsState.curMarketPairs[resultResponse.market] == null)
                {
                    Dictionary<string, TradePair> tradePairs = new Dictionary<string, TradePair>();
                    foreach (KeyValuePair<string, MarketCurrent> pair in currenciesDict)
                    {
                        Pair pairinfo = new Pair(pair.Value.ticker);
                        TradePair tpair = new TradePair
                        {
                            currency1 = pairinfo.currency1,
                            currency2 = pairinfo.currency2,
                            isActive = true,
                            ticker = pair.Value.ticker
                        };

                        tradePairs.Add(pair.Key, tpair);
                    }
                    Global.marketsState.SetPairs(resultResponse.market, tradePairs);
                }


                bool allMarketsReady = true;
                foreach (var marketName in Global.markets.GetMarketList())
                {
                    if (Global.marketsState.curMarketPairs[marketName] == null)
                    {
                        allMarketsReady = false;
                        break;
                    }
                }
                if (allMarketsReady)
                    marketsLoaded = true;
            }

            if (marketsLoaded)
            {
                Market curmarket = ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text);
                if (curmarket.HaveKey())
                {
                    toolButtonTickerBtc.Enabled = true;
                    toolButtonTickerUsd.Enabled = true;
                    if (curmarket.Options().AllPairRatesSupported)
                        toolStripButtonAlerts.Enabled = true;
                }
                if(curmarket.Options().ChartDataSupported)
                  toolStripButtonChart.Enabled = true;
                toolStripButtonBalance.Enabled = true;
                toolStripComboBoxMarket.Enabled = true;
                labelInfo.Text = "";
            }

            List<MarketCurrentView> currencies = currenciesDict.Values.Select(item => new MarketCurrentView
            {
                ticker = item.ticker,
                origPrice = item.lastPrice,
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
                        item.lastPrice = item.lastPrice / btcPrice;
                    }
                }

            }
            currencies = currencies.OrderByDescending(x => x.volumeUSDT).ToList();

            List<MarketCurrentView> currenciesCopy = new List<MarketCurrentView>();
            foreach (MarketCurrentView item in currencies)
            {
                MarketCurrentView newItem = new MarketCurrentView()
                {
                    ticker = item.ticker,
                    origPrice = item.origPrice,
                    lastPrice = item.lastPrice,
                    lastPriceUSD = item.lastPriceUSD,
                    percentChange = item.percentChange,
                    volumeBtc = item.volumeBtc,
                    volumeUSDT = item.volumeUSDT
                };
                currenciesCopy.Add(newItem);
            }
            if (ExchangeManager.GetMarketByMarketName(resultResponse.market).Options().AllPairRatesSupported)
                Global.marketsState.Update(resultResponse.market, currenciesCopy);


            if (resultResponse.market != toolStripComboBoxMarket.Text)
                return;


            var dataView = currencies.Select(item => new
            {
                ticker = item.ticker,
                lastPrice = item.lastPrice,
                lastPriceUSDT = item.lastPriceUSD,
                percentChange = item.percentChange,
                volumeBtc = item.volumeBtc,
                volumeUSDT = item.volumeUSDT,
                lastPriceDisplay = Helper.PriceToStringBtc(item.lastPrice),
                lastPriceUSDTDisplay = item.lastPriceUSD.ToString("N3")+" $",
                percentChangeDisplay = item.percentChange.ToString("0") + " %",
                volumeBtcDisplay = item.volumeBtc.ToString("N2"),
                volumeUSDTDisplay = item.volumeUSDT.ToString("N0")+ " $"
            }).ToList();
            List<GVColumn> columns = new List<GVColumn>()
            {
                new GVColumn( "ticker", "Currency","string") ,
                new GVColumn( "lastPrice", "Price BTC","DisplayField","lastPriceDisplay") ,
                new GVColumn( "lastPriceUSDT", "Price $","DisplayField","lastPriceUSDTDisplay") ,
                new GVColumn( "percentChange", "Change %","DisplayField","percentChangeDisplay") ,
                new GVColumn( "volumeBtc", "Volume BTC","DisplayField","volumeBtcDisplay"),
                new GVColumn( "volumeUSDT", "Volume $","DisplayField","volumeUSDTDisplay"),
                new GVColumn( "lastPriceDisplay", "Price BTC","string","",false) ,
                new GVColumn( "lastPriceUSDTDisplay", "Price $","string","",false) ,
                new GVColumn( "percentChangeDisplay", "Change %","string","",false) ,
                new GVColumn( "volumeBtcDisplay", "Volume BTC","string","",false),
                new GVColumn( "volumeUSDTDisplay", "Volume $","string","",false)
            };
            if(gvMarkets==null)
              gvMarkets = new DataGridViewExWrapper();
            gvMarkets.Create(dgridMarkets,dataView, columns, true);
            DataGridViewCellStyle styleTicker = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Bold), ForeColor = Color.Black };
            DataGridViewCellStyle stylePrice = new DataGridViewCellStyle { Font = new Font("Tahoma", 9.0F, FontStyle.Regular), ForeColor = Color.Black };
            gvMarkets.SetColumnStyle("ticker", styleTicker);
            gvMarkets.SetColumnStyle("lastPrice", stylePrice);
            gvMarkets.SetColumnStyle("lastPriceUSDT", stylePrice);
            //gvMarkets.AutoSizeFillExcept("volumeUSDT");
            //            gv.RowColorByCondition("orderType", (string s) => { return s == "1"; }, Color.LightPink);


            timerMarkets.Start();

        }
        private void TimerMarkets_Tick(object sender, ElapsedEventArgs e)
        {
            timerMarkets.Stop();
            UpdateMarkets();
        }
        private void TimerCheckAlerts_Tick(object sender, ElapsedEventArgs e)
        {
            if(marketsLoaded)
              AlertManager.CheckAlerts();
        }

        private void dgridMarkets_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gvMarkets.RowColorByCondition("percentChange", s => { return (double)s<0; }, Color.MistyRose);
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
           // ShowTradeForm(e.ClickedItem.Text);
        }

        private void toolDropDownTickerUsdt_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //ShowTradeForm(e.ClickedItem.Text);
        }
        private string ChooseTicker(string filter)
        {
            FormTicker form = new FormTicker(Global.marketsState.curMarketPairs[toolStripComboBoxMarket.Text], filter);
            form.ShowDialog();
            return form.selectedTicker;
//            MessageBox.Show(form.selectedTicker);

        }
        private void toolButtonTickerBtc_Click(object sender, EventArgs e)
        {
           string ticker = ChooseTicker("BTC");
            if(ticker!="")
             ShowTradeForm(ticker);
        }

        private void toolButtonTickerUsd_Click(object sender, EventArgs e)
        {
            string ticker = ChooseTicker("USD");
            if (ticker != "")
                ShowTradeForm(ticker);
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
            form.Show();
        }

        private void toolStripButtonAlerts_Click(object sender, EventArgs e)
        {
            FormAlertMain form = new FormAlertMain(ExchangeManager.GetMarketByMarketName(toolStripComboBoxMarket.Text));
//            form.parent = this;
            form.Show();

        }

    }
}
