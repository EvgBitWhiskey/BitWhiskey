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
        public FormTradeLogic tradeLogic;
        Market market;

        public FormAlertAdd(Market market_, string ticker_="")
        {
            InitializeComponent();
            market = market_;
            ticker = ticker_;
            tradeLogic = new FormTradeLogic("BTC_BTC", market);
        }
        private void FormAlertAdd_Load(object sender, EventArgs e)
        {
            textBoxAlertName.Text ="Alert "+ Alert.GetNewId().ToString();
            LoadTickers();
        }
        private void LoadTickers()
        {
            listBoxTicker.Items.Clear();
            tradeLogic.GetTradePairs(LoadTickers_UIResultHandler);
        }
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
        private void listBoxTicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ticker = listBoxTicker.SelectedItem.ToString();
            textBoxPriceCurrent.Text = Helper.PriceToStringBtc(Global.GetCurrentPrice(market.MarketName(), ticker));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                Alert alertTmp = new Alert(textBoxAlertName.Text);
                alertTmp.createPrice = Helper.ToDouble(textBoxPriceCurrent.Text);
                alertTmp.priceAlert = Helper.ToDouble(textBoxPriceAlert.Text);
                alertTmp.showInChart = checkBoxDisplayInChart.Checked;
                alertTmp.tickerPair = listBoxTicker.SelectedItem.ToString();
                alertTmp.playSound = checkBoxPlaySound.Checked;
                alert = alertTmp;
            }
            catch(Exception ex)
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
