using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace BitWhiskey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Helper.Init();

            toolStripComboBoxMarket.SelectedIndex = 0;
            timerUpdate.Enabled = false;
        }
        private void toolStripComboBoxMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTickers();
        }
    private void LoadTickers()
        {

            Market market=GetMarketByMarketName(toolStripComboBoxMarket.Text);

            market.GetTradePairs();
            //Global.market.tradePairs = new Dictionary<string, TradePair>();
            //Global.market.tradePairs.Add("BTC_LTC",new TradePair{currency1 = "BTC",currency2 = "LTC",ticker = "BTC_LTC",isActive =true});

            int btctickerCount = 0;
            foreach (KeyValuePair<string,TradePair> pair in market.tradePairs)
            {
                if (pair.Key.StartsWith("BTC"))
                    btctickerCount++;
            }

            ToolStripDropDown drop_downbtc = new  ToolStripDropDown();
            drop_downbtc.LayoutStyle = ToolStripLayoutStyle.Table;
//            drop_downbtc.
            ((TableLayoutSettings)drop_downbtc.LayoutSettings).ColumnCount =btctickerCount/23+1;
            toolDropDownTickerBtc.DropDown = drop_downbtc;

            ToolStripDropDown drop_downusdt = new ToolStripDropDown();
            drop_downusdt.LayoutStyle = ToolStripLayoutStyle.Table;
            ((TableLayoutSettings)drop_downusdt.LayoutSettings).ColumnCount = (market.tradePairs.Count - btctickerCount) / 23 + 1; 
            toolDropDownTickerUsdt.DropDown = drop_downusdt;

            foreach (KeyValuePair<string,TradePair> pair in market.tradePairs.OrderBy(p => p.Key))
            {
                if(pair.Key.StartsWith("BTC"))
                  toolDropDownTickerBtc.DropDown.Items.Add(pair.Key);
                else //if (pair.Key.StartsWith("USDT"))
                    toolDropDownTickerUsdt.DropDown.Items.Add(pair.Key);
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
        }

        //mouse scroll https://stackoverflow.com/questions/8543802/set-autoscroll-position-on-mouse-move

        private void listBoxTicker_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private TradeDriver GetTradeDriverByMarketName(string marketName,string ticker)
        {
            switch(marketName)
            {
                case "Poloniex":
                    return new TradeDriverPoloniex(ticker);
                case "Bittrex":
                    return new TradeDriverBittrex(ticker);
            };

            return null;
        }
        private Market GetMarketByMarketName(string marketName)
        {
            switch (marketName)
            {
                case "Poloniex":
                    return new Poloniex();
                case "Bittrex":
                    return new Bittrex();
            };

            return null;
        }

        private void ShowTradeForm(string ticker)
        {
            FormTrade form = new FormTrade(GetTradeDriverByMarketName(toolStripComboBoxMarket.Text,ticker));
            form.parent = this;
            form.Show();
        }
        private void ShowChartForm()
        {
            FormChart form = new FormChart(GetMarketByMarketName(toolStripComboBoxMarket.Text));
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
    }
}
