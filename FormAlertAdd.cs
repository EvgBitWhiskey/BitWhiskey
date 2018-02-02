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
    public partial class FormAlertAdd : Form
    {
        public Alert alert=null;
        public string ticker="";
//        public FormTradeLogic tradeLogic;
        Market market;
        public bool alertAdded = false;
        public bool alertChanged = false;

        public FormAlertAdd(Market market_, string ticker_="",int alertId=-1)
        {
            InitializeComponent();
            market = market_;
            //            tradeLogic = new FormTradeLogic("BTC_BTC", market);
            if(alertId!=-1)
              alert= AlertManager.alerts[alertId];

            if (alert == null)
            {
                ticker = ticker_;
                textBoxAlertName.Text = ticker;
            }
            else
            {
                textBoxAlertName.Text = alert.caption;
                textBoxPriceCurrent.Text = Helper.PriceToStringBtc(Global.GetCurrentPrice(alert.market,alert.tickerPair));
                textBoxPriceAlert.Text = alert.priceAlert.ToString();
                checkBoxDisplayInChart.Checked = alert.showInChart;
                checkBoxPlaySound.Checked= alert.playSound;

                listBoxTicker.Enabled = false;
                ticker = alert.tickerPair;
            }
            LoadTickers();
        }
        private void FormAlertAdd_Load(object sender, EventArgs e)
        { }
        private void LoadTickers()
        {
            listBoxTicker.Items.Clear();
            foreach (KeyValuePair<string, TradePair> pair in Global.marketsState.curMarketPairs[market.MarketName()].OrderBy(p => p.Key))
                listBoxTicker.Items.Add(pair.Key);
            if (ticker != "")
            {
                listBoxTicker.SelectedItem = ticker;
                if (alert == null)
                    textBoxPriceCurrent.Text = Helper.PriceToStringBtc(Global.GetCurrentPrice(market.MarketName(), ticker));
            }

            //  tradeLogic.GetTradePairs(LoadTickers_UIResultHandler);
        }
        /*
        public void LoadTickers_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            Dictionary<string, TradePair> tradePairs = (Dictionary<string, TradePair>)resultResponse.items[0].result.resultData;

            foreach (KeyValuePair<string, TradePair> pair in tradePairs.OrderBy(p => p.Key))
                listBoxTicker.Items.Add(pair.Key);

            if(ticker!="")
            {
                listBoxTicker.SelectedItem = ticker;
                textBoxPriceCurrent.Text=Helper.PriceToStringBtc(Global.GetCurrentPrice(market.MarketName(), ticker));
            }
        }
        */
        private void listBoxTicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ticker = listBoxTicker.SelectedItem.ToString();
            textBoxAlertName.Text = ticker;
            if (alert == null)
                textBoxPriceCurrent.Text = Helper.PriceToStringBtc(Global.GetCurrentPrice(market.MarketName(), ticker));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (alert != null)
                {
                    alert.caption=textBoxAlertName.Text;
                    alert.createPrice = Helper.ToDouble(textBoxPriceCurrent.Text);
                    alert.priceAlert = Helper.ToDouble(textBoxPriceAlert.Text);
                    alert.showInChart = checkBoxDisplayInChart.Checked;
                    alert.tickerPair = listBoxTicker.SelectedItem.ToString();
                    alert.playSound = checkBoxPlaySound.Checked;
                    alertChanged = true;
                }
                else
                {
                    Alert alertTmp = new Alert(textBoxAlertName.Text);
                    alertTmp.createPrice = Helper.ToDouble(textBoxPriceCurrent.Text);
                    alertTmp.market = market.MarketName();
                    alertTmp.priceAlert = Helper.ToDouble(textBoxPriceAlert.Text);
                    alertTmp.showInChart = checkBoxDisplayInChart.Checked;
                    alertTmp.tickerPair = listBoxTicker.SelectedItem.ToString();
                    alertTmp.playSound = checkBoxPlaySound.Checked;
                    alert = alertTmp;

                    AlertManager.alerts.Add(alert.id, alert);
                    alertAdded = true;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void MakeAlertText()
        {
            labelAlertText.Text = "Fill Price Textboxes first....";
            double priceAlert = 0;
            double priceCurrent = 0;
            if (Helper.IsDouble(textBoxPriceAlert.Text))
            {
                priceAlert = Helper.ToDouble(textBoxPriceAlert.Text);
            }
            if (Helper.IsDouble(textBoxPriceCurrent.Text))
            {
                priceCurrent = Helper.ToDouble(textBoxPriceCurrent.Text);
            }
            if (priceAlert != 0 && priceCurrent != 0)
            {
                if (priceAlert > priceCurrent)
                    labelAlertText.Text = "Alert When market PRICE is ABOVE(>=) Alert Price";
                else
                    labelAlertText.Text = "Alert When market PRICE is BELOW(<=) Alert Price";
            }
        }

        private void textBoxPriceAlert_TextChanged(object sender, EventArgs e)
        {
            MakeAlertText();
        }
        private void textBoxPriceCurrent_TextChanged(object sender, EventArgs e)
        {
            MakeAlertText();
        }
    }
}
