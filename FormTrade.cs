using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitWhiskey
{
    public partial class FormTrade : Form
    {
        public enum UpdateAction
        {
            UPDATE_IF_VISIBLE=0,
            UPDATE_FORCE = 1
        }

        public Form parent;
//        public string ticker;
        Color buttoncolor;
        TradeDriver tradeDriver;

        public FormTrade(TradeDriver tradeDriver_)
        {
            InitializeComponent();
            tradeDriver=tradeDriver_;

        }

        private void FormTrade_Load(object sender, EventArgs e)
        {

            buttoncolor = Color.FromArgb(buttonBuy.BackColor.ToArgb());
            Text = tradeDriver.GetMarketName() + "  " + tradeDriver.ticker + "  Trade";
            checkBoxLimitOrder.Checked = Global.settingsMain.defaultlimitorders;
            CollapsePanels(true);
            try
            {
                UpdateBalance();
            }
            catch (Exception ex) { Helper.logger.Error(ex, "UpdateBalance Error"); }

            try
            {
                UpdateLastMarketPrice();
                UpdateTradeHistory(UpdateAction.UPDATE_IF_VISIBLE);
            }
            catch (Exception ex)
            { Helper.logger.Error(ex, "Update Last Prices Error"); }

            timerTradeLast.Enabled = true;
            timerTradeHistory.Enabled = true;
        }

        private void DataGridHideSelection(DataGridView grid)
        {
          grid.DefaultCellStyle.SelectionBackColor = grid.DefaultCellStyle.BackColor;
          grid.DefaultCellStyle.SelectionForeColor = grid.DefaultCellStyle.ForeColor;
        }
        public void UpdateBalance()
        {
            tradeDriver.GetBalances();
            labelBalanceBase.Text = tradeDriver.baseCurrencyName;
            labelBalanceMarket.Text = tradeDriver.counterCurrencyName;
            labelBalanceBaseValue.Text = Helper.PriceToStringBtc(tradeDriver.balanceBase.balance);
            labelBalanceMarketValue.Text = Helper.PriceToStringBtc(tradeDriver.balanceCounter.balance);
            labelAmount.Text = "Amount " + tradeDriver.counterCurrencyName;
        }
        public void UpdateOrderBook(UpdateAction updateAction=UpdateAction.UPDATE_FORCE)
        {
            if (!panelOrderBook.Visible && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            EnableUI(false);
            tradeDriver.GetOrderBook();

            DataTable dtSell = new DataTable();
            dtSell.Columns.Add("Amount", typeof(string));
            dtSell.Columns.Add("Price", typeof(string));

            int n = 0;
            foreach (SellOrder sorder in tradeDriver.sellOrders)
            {
                n++;
                if (n > 25)
                    break;
                DataRow row = dtSell.NewRow();
                row["Amount"] = Helper.PriceToStringBtc(sorder.quantity);
                row["Price"] = Helper.PriceToStringBtc(sorder.rate);
                dtSell.Rows.Add(row);
            }

            dgridSellOrders.DataSource = dtSell;
            dgridSellOrders.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgridSellOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridHideSelection(dgridSellOrders);

            DataTable dtBuy = new DataTable();
            dtBuy.Columns.Add("Price", typeof(string));
            dtBuy.Columns.Add("Amount", typeof(string));

            n = 0;
            foreach (BuyOrder border in tradeDriver.buyOrders)
            {
                n++;
                if (n > 25)
                    break;
                DataRow row = dtBuy.NewRow();
                row["Price"] = Helper.PriceToStringBtc(border.rate);
                row["Amount"] = Helper.PriceToStringBtc(border.quantity);
                dtBuy.Rows.Add(row);
            }

            dgridBuyOrders.DataSource = dtBuy;
            dgridBuyOrders.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridBuyOrders.Columns[0].Resizable = DataGridViewTriState.True;
            dgridBuyOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgridBuyOrders.Columns[1].Resizable = DataGridViewTriState.True;
            DataGridHideSelection(dgridBuyOrders);

            EnableUI(true);
        }

        public void UpdateTradeHistory(UpdateAction updateAction = UpdateAction.UPDATE_FORCE)
        {
            if ((!panelTabMain.Visible || tabMain.SelectedTab!=tabPageTradeHistory) && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            EnableUI(false);
            tradeDriver.GetTradeHistory();

            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Fill", typeof(string));

            int n = 0;
            foreach (Trade trade in tradeDriver.tradeHistory)
            {
                n++;
                if (n > 40)
                    break;
                DataRow row = dt.NewRow();
                row["Date"] = trade.tradeDate;
                row["Type"] = trade.orderType;
                row["Price"] = Helper.PriceToStringBtc(trade.price);
                row["Amount"] = Helper.TradeAmountToString(trade.quantity);
//                row["Amount"] = Helper.PriceToString(trade.quantity);
                row["Fill"] = trade.fillType;
                dt.Rows.Add(row);
            }

            dgridTradeHistory.DataSource = dt;
            dgridTradeHistory.Columns[0].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            dgridTradeHistory.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridTradeHistory.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgridTradeHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgridTradeHistory.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgridTradeHistory.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewRow row in dgridTradeHistory.Rows)
            {
                if (Convert.ToString(row.Cells["Type"].Value) == "SELL")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }
            DataGridHideSelection(dgridTradeHistory);
            EnableUI(true);
        }
        public void UpdateMyOpenOrders(UpdateAction updateAction = UpdateAction.UPDATE_FORCE)
        {
            if ((!panelTabMain.Visible || tabMain.SelectedTab != tabPageMyOrders) && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            EnableUI(false);
            tradeDriver.GetMyOpenOrders();

            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("AmountRemaining", typeof(string));

            foreach (OpenOrder order in tradeDriver.myOpenOrders)
            {
                DataRow row = dt.NewRow();
                row["Date"] = order.openedDate;
                row["Type"] = order.orderType;
                row["Price"] = Helper.PriceToStringBtc(order.price);
                row["Amount"] = Helper.PriceToStringBtc(order.quantity);
                row["AmountRemaining"] = Helper.PriceToStringBtc(order.quantityRemaining);
                dt.Rows.Add(row);
            }

            dgridOpenOrders.DataSource = dt;
            dgridOpenOrders.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridOpenOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridOpenOrders.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgridOpenOrders.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridOpenOrders.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            foreach (DataGridViewRow row in dgridOpenOrders.Rows)
            {
                if (Convert.ToString(row.Cells["Type"].Value) == "SELL LIMIT")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
            }
            DataGridHideSelection(dgridOpenOrders);
            EnableUI(true);
        }

        public void UpdateMyOrdersHistory(UpdateAction updateAction = UpdateAction.UPDATE_FORCE)
        {
            if ((!panelTabMain.Visible || tabMain.SelectedTab != tabPageMyOrders) && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            EnableUI(false);
            tradeDriver.GetMyOrdersHistory();

            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            dt.Columns.Add("S/B", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            dt.Columns.Add("Remain", typeof(string));


            foreach (OrderDone order in tradeDriver.myOrdersHistory)
            {
                DataRow row = dt.NewRow();
                row["Date"] = order.doneDate;
                row["S/B"] = order.orderType;
                row["Price"] = Helper.PriceToStringBtc(order.price);
                row["Amount"] = Helper.PriceToStringBtc(order.quantity);
                row["Remain"] = Helper.PriceToStringBtc(order.quantityRemaining);
                dt.Rows.Add(row);
            }

            dgridMyOrdersHistory.DataSource = dt;
            dgridMyOrdersHistory.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridMyOrdersHistory.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridMyOrdersHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridMyOrdersHistory.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgridMyOrdersHistory.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            DataGridHideSelection(dgridMyOrdersHistory);
            EnableUI(true);
        }

        public void UpdateLastMarketPrice()
        {
            tradeDriver.GetLastMarketPrice();
            tradeDriver.GetLastMarketPriceBtcUsdt();
            TradeLast trade = tradeDriver.lastMarketPrice;

            buttonBuy.Text = "BUY  " + Helper.PriceToStringBtc(trade.ask);
            buttonSell.Text = "SELL  " + Helper.PriceToStringBtc(trade.bid);
            buttonBuy.BackColor = Color.FromArgb(buttoncolor.ToArgb());
            buttonSell.BackColor = Color.FromArgb(buttoncolor.ToArgb());
            if (trade.last >= trade.ask)
            {
                buttonBuy.BackColor = Color.Aquamarine;
            }
            if (trade.last <=trade.bid)
            {
                buttonSell.BackColor = Color.LightPink;//Color.LightPink;; 
            }
            labelSpread.Text = Helper.CalcSpread(trade.ask, trade.bid).ToString("0.00") + " %";
            double averageBtcPrice = (trade.ask + trade.bid)/2;
            double btcInUsd = (tradeDriver.lastbtcInUsdtPrice.bid + tradeDriver.lastbtcInUsdtPrice.ask) / 2;
            double averageUsdPrice = averageBtcPrice ;
            if(tradeDriver.baseCurrencyName!="USDT")
              averageUsdPrice = averageBtcPrice * btcInUsd;
            labelAverage.Text = "Average: "+Helper.PriceToStringBtc(averageBtcPrice)+" = "+ Helper.PriceToString(averageUsdPrice) + "$";
        }
       
        private void EnableUI(bool isenabled)
        {
            buttonBuy.Enabled = isenabled;
            buttonSell.Enabled = isenabled;
        }
        private void timerTradeLast_Tick(object sender, EventArgs e)
        {
            UpdateLastMarketPrice();
        }
        private void timerTradeHistory_Tick(object sender, EventArgs e)
        {
           UpdateTradeHistory(UpdateAction.UPDATE_IF_VISIBLE);
        }
        private bool CheckTradeFields()
        {
            if (!Helper.IsDouble(textAmount.Text))
            {
                MessageBox.Show("Amount is Invalid");
                return false;
            }
            if (checkBoxLimitOrder.Checked && !Helper.IsDouble(textPrice.Text))
            {
                MessageBox.Show("Price is Invalid");
                return false;
            }

            double quantity = Helper.ToDouble(textAmount.Text);
            if (quantity <= 0)
            {
                MessageBox.Show("Amount must be > 0");
                return false;
            }

            if (checkBoxLimitOrder.Checked)
            {
                double price = Helper.ToDouble(textPrice.Text);
                if (price <= 0)
                {
                    MessageBox.Show("Price must be > 0");
                    return false;
                }
            }
            return true;
        }
        private void UpdateTradeInfo()
        {
            UpdateBalance();
            UpdateMyOpenOrders(UpdateAction.UPDATE_IF_VISIBLE);
            UpdateMyOrdersHistory(UpdateAction.UPDATE_IF_VISIBLE);
            UpdateLastMarketPrice();
        }
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            if(!CheckTradeFields())
                return;
            double quantity = Helper.ToDouble(textAmount.Text);

            try
            {
                if (checkBoxLimitOrder.Checked)
                {
                    double price = Helper.ToDouble(textPrice.Text);
                    tradeDriver.BuyLimit(quantity, price);
                }
                else
                {
                    tradeDriver.BuyMarket(quantity);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    Helper.LogAndDisplay(ex, "Can't process buy order.Request TimeOut.");
                    return;
                }
                else throw;
            }
            catch (MarketAPIException ex)
            {
                Helper.LogAndDisplay(ex);
                return;
            }
            
            string msg = tradeDriver.myTradeState.errMsg;
            if (msg != "")
                MessageBox.Show(msg);

            if (tradeDriver.myTradeState.completedOk)
            {
                Thread.Sleep(700);
                UpdateTradeInfo();
            }


        }

        private void buttonSell_Click(object sender, EventArgs e)
        {

            if (!CheckTradeFields())
                return;
            double quantity = Helper.ToDouble(textAmount.Text);
            try
            {


                if (checkBoxLimitOrder.Checked)
                {
                    double price = Helper.ToDouble(textPrice.Text);
                    tradeDriver.SellLimit(quantity, price);
                }
                else
                {
                    tradeDriver.SellMarket(quantity);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    Helper.LogAndDisplay(ex, "Can't process sell order.Request TimeOut.");
                    return;
                }
                else throw;
            }
            catch (MarketAPIException ex)
            {
                Helper.LogAndDisplay(ex);
                return;
            }

            string msg = tradeDriver.myTradeState.errMsg;
            if (msg != "")
                MessageBox.Show(msg);

            if (tradeDriver.myTradeState.completedOk)
            {
                Thread.Sleep(700);
                UpdateTradeInfo();
            }

        }

        private void buttonUpdateOrderBook_Click(object sender, EventArgs e)
        {
           UpdateOrderBook();
//            UpdateMyOpenOrders();
        }
        private void buttonMyOrdersHistory_Click(object sender, EventArgs e)
        {
            UpdateMyOrdersHistory();
        }
        private void buttonUpdateTradeHistory_Click(object sender, EventArgs e)
        {
            UpdateTradeHistory();
        }

        private void buttonUpdateMyOpenOrders_Click(object sender, EventArgs e)
        {
            UpdateMyOpenOrders();
        }

        private void buttonCancellAllOrders_Click(object sender, EventArgs e)
        {
            EnableUI(false);
            tradeDriver.GetMyOpenOrders();

            foreach (OpenOrder order in tradeDriver.myOpenOrders)
            {
                tradeDriver.CancellMyOrder(order);
                Thread.Sleep(400);
            }
            UpdateMyOpenOrders();
            UpdateBalance();
            EnableUI(true);
        }

        private void FormTrade_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Activate();
            if (parent.WindowState == FormWindowState.Minimized)
                parent.WindowState = FormWindowState.Normal;
        }
        private void dgridOpenOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {        }

        private void buttonCollapsePanelOrderBook_Click(object sender, EventArgs e)
        {
            panelOrderBook.Visible = !panelOrderBook.Visible;
            ResizeForm();
        }
        private void buttonCollapsePanelTabMain_Click(object sender, EventArgs e)
        {
            panelTabMain.Visible = !panelTabMain.Visible;
            ResizeForm();
        }
        private void ResizeForm()
        {
            Height = panelTabMain.Top;
        }
        private void CollapsePanels(bool collapse)
        {
            if (collapse)
            {
                panelOrderBook.Visible =false;
                panelTabMain.Visible = false;
            }
            else
            {
                panelOrderBook.Visible = true;
                panelTabMain.Visible = true;
            }
            ResizeForm();
        }

        private void buttonSetAskPrice_Click(object sender, EventArgs e)
        {
            double quantity=0;
            if (Helper.IsDouble(textAmount.Text))
                quantity = Helper.ToDouble(textAmount.Text);
            else
            {
                MessageBox.Show("Amount is Invalid");
                return;
            }
            UpdateOrderBook();
            double price = tradeDriver.LastAskFromOrderBook(quantity);
            if(price!=0)
              textPrice.Text = Helper.PriceToStringBtc(price);
        }

        private void buttonSetBidPrice_Click(object sender, EventArgs e)
        {
            double quantity = 0;
            if (Helper.IsDouble(textAmount.Text))
                quantity = Helper.ToDouble(textAmount.Text);
            else
            {
                MessageBox.Show("Amount is Invalid");
                return;
            }
            UpdateOrderBook();
            double price = tradeDriver.LastBidFromOrderBook(quantity);
            if (price != 0)
                textPrice.Text = Helper.PriceToStringBtc(price);

        }

        private void checkBoxLimitOrder_CheckedChanged(object sender, EventArgs e)
        {
            textPrice.ReadOnly = !checkBoxLimitOrder.Checked;

        }

    }
}
