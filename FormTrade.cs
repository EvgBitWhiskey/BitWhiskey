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
        List<SellOrder> contrSellOrdersInGrid = null;
        List<BuyOrder> contrBuyOrdersInGrid = null;

        public Form parent;
        Color buttoncolor;
        BlinkControl labelBalanceMarketBold=new BlinkControl();
        BlinkControl labelBalanceBaseBold=new BlinkControl();
        FormTradeLogic tradeLogic;


//        var dataViewContrSell;

        public FormTrade(Market market,string ticker)
        {
            InitializeComponent();
            tradeLogic = new FormTradeLogic(ticker, market); 
        }
        private void FormTrade_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Activate();
            if (parent.WindowState == FormWindowState.Minimized)
                parent.WindowState = FormWindowState.Normal;
        }
        private void dgridOpenOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { }
        private void buttonCollapsePanelOrderBook_Click(object sender, EventArgs e)
        {
            panelTabMain.Visible = !panelTabMain.Visible;
//            panelOrderBook.Visible = !panelOrderBook.Visible;
            ResizeForm();
        }
        private void buttonCollapsePanelTabMain_Click(object sender, EventArgs e)
        {
//            panelOBookButton.Visible = !panelOBookButton.Visible;
//            ResizeForm();
        }
        private void ResizeForm()
        {
            Height = panelOBookButton.Top;
        }
        private void CollapsePanels(bool collapse)
        {
            /*
            if (collapse)
            {
                panelOrderBook.Visible = false;
                panelOBookButton.Visible = false;
            }
            else
            {
                panelOrderBook.Visible = true;
                panelOBookButton.Visible = true;
            }
            ResizeForm();
            */
        }

        private void FormTrade_Load(object sender, EventArgs e)
        {

            buttoncolor = Color.FromArgb(buttonBuy.BackColor.ToArgb());
            Text = tradeLogic.GetMarketName() + "  " + tradeLogic.ticker + "  Trade";
         //   checkBoxLimitOrder.Checked = Global.settingsMain.defaultlimitorders;
            CollapsePanels(true);

            UpdateTradeState();
            labelBalanceMarketBold.Create(labelBalanceMarketValue);
            labelBalanceBaseBold.Create(labelBalanceBaseValue);

            timerTradeLast.Enabled = true;
            timerTradeHistory.Enabled = true;
        }

        public void UpdateOrderBook(UpdateAction updateAction=UpdateAction.UPDATE_FORCE)
        {
            if (!panelOrderBook.Visible && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;

            dgridSellOrders.DataSource = null;
            dgridBuyOrders.DataSource = null;
            tradeLogic.GetOrderBook(UpdateOrderBook_UIResultHandler);
        }
        public void UpdateOrderBook_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            AllOrders orders = (AllOrders)resultResponse.items[0].result.resultData;

            var dataViewSell = orders.sellOrders.Select(item => new
            {
                amount = Helper.PriceToStringBtc(item.quantity),
                price = Helper.PriceToStringBtc(item.rate)
            }).Take(150).ToList();
            List<DGVColumn> columnsSell = new List<DGVColumn>()
            {
                new DGVColumn( "amount", "Amount","string") ,
                new DGVColumn( "price", "Price","string") 
            };
            DataGridViewWrapper gvSell = new DataGridViewWrapper(dgridSellOrders, true);
            gvSell.Create(dataViewSell, columnsSell);
            gvSell.AutoSizeFillExcept("amount");

            var dataViewBuy = orders.buyOrders.Select(item => new
            {
                price = Helper.PriceToStringBtc(item.rate),
                amount = Helper.PriceToStringBtc(item.quantity)
            }).Take(150).ToList();
            DataGridViewWrapper gvBuy = new DataGridViewWrapper(dgridBuyOrders, true);
            gvBuy.Create(dataViewBuy, columnsSell);
            // gvBuy.AutoSizeFillExcept("amount");

            // contr orderbook data grid
            var dataViewContrSell = orders.sellOrders.Select(item => new
            {
                price = Helper.PriceToStringBtc(item.rate),
                amount = Helper.PriceToStringBtc(item.quantity)
            }).OrderByDescending(o => o.price).Take(150).ToList();

            DataGridViewWrapper gvContrSell = new DataGridViewWrapper(dGridContrSell, true);
            gvContrSell.Create(dataViewContrSell, columnsSell);
            dGridContrSell.FirstDisplayedScrollingRowIndex = dGridContrSell.RowCount - 1;

            var dataViewContrBuy = orders.buyOrders.Select(item => new
            {
                price = Helper.PriceToStringBtc(item.rate),
                amount = Helper.PriceToStringBtc(item.quantity)
            }).Take(150).ToList();
            DataGridViewWrapper gvContrBuy = new DataGridViewWrapper(dGridContrBuy, true);
            gvContrBuy.Create(dataViewContrBuy, columnsSell);
            gvContrBuy.ShowColumnHeaders(false);

        }
        private void tabPageOrderBookContr_Click(object sender, EventArgs e)
        {
        }
        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            if(e.TabPageIndex==1)
            {
                dGridContrSell.Visible = false;
                Thread.Sleep(500);
                dGridContrSell.Visible = true;
            }
            /*
            if (dGridContrSell.RowCount > 0)
            {
                dGridContrSell.Rows[dGridContrSell.RowCount - 1].Selected=true;
                dGridContrSell.FirstDisplayedScrollingRowIndex = dGridContrSell.RowCount - 2;
            }
            */
        }

        public void UpdateTradeHistory(UpdateAction updateAction = UpdateAction.UPDATE_FORCE)
        {
            if ((!panelOrderBook.Visible || tabMain.SelectedTab!=tabPageTradeHistory) && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            dgridTradeHistory.DataSource = null;
            tradeLogic.GetTradeHistory(UpdateTradeHistory_UIResultHandler);
        }
        public void UpdateTradeHistory_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            List<Trade> tradeHistory = (List<Trade>)resultResponse.items[0].result.resultData;

            var dataView = tradeHistory.Select(item => new
            {
                date = item.tradeDate,
                orderType = item.orderType,
                price = Helper.PriceToStringBtc(item.price),
                amount = Helper.TradeAmountToString(item.quantity),
                fillType = item.fillType
            }).Take(40).ToList();
            List<DGVColumn> columns = new List<DGVColumn>()
            {
                new DGVColumn( "date", "Date","string") ,
                new DGVColumn( "orderType", "Type","string") ,
                new DGVColumn( "price", "Price","string") ,
                new DGVColumn( "amount", "Amount","string") ,
                new DGVColumn( "fillType", "Fill","string")
            };
            DataGridViewWrapper gv = new DataGridViewWrapper(dgridTradeHistory, true);
            gv.Create(dataView, columns);
            gv.AutoSizeFillExcept("date");
            gv.RowColorByCondition("orderType", "SELL", Color.LightPink);

        }
        public void UpdateMyOpenOrders(UpdateAction updateAction = UpdateAction.UPDATE_FORCE)
        {
            if ((!panelOrderBook.Visible || tabMain.SelectedTab != tabPageMyOrders) && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            dgridOpenOrders.DataSource = null;
            tradeLogic.GetMyOpenOrders(UpdateMyOpenOrders_UIResultHandler);
        }
        public void UpdateMyOpenOrders_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            List<OpenOrder> myOpenOrders = (List<OpenOrder>)resultResponse.items[0].result.resultData;

            var dataView = myOpenOrders.Select(item => new
            {
                date = item.openedDate,
                orderType = item.orderType,
                price = Helper.PriceToStringBtc(item.price),
                amount = Helper.PriceToStringBtc(item.quantity),
                remain = Helper.PriceToStringBtc(item.quantityRemaining)
            }).ToList();
            List<DGVColumn> columns = new List<DGVColumn>()
            {
                new DGVColumn( "date", "Date","string") ,
                new DGVColumn( "orderType", "Type","string") ,
                new DGVColumn( "price", "Price","string") ,
                new DGVColumn( "amount", "Amount","string") ,
                new DGVColumn( "remain", "Remain","string")
            };
            DataGridViewWrapper gv = new DataGridViewWrapper(dgridOpenOrders, true);
            gv.Create(dataView, columns);
            gv.AutoSizeDisplayedExcept("price");
            gv.RowColorByCondition("orderType", "SELL LIMIT", Color.LightPink);
        }

        public void UpdateMyOrdersHistory(UpdateAction updateAction = UpdateAction.UPDATE_FORCE)
        {
            if ((!panelOrderBook.Visible || tabMain.SelectedTab != tabPageMyOrders) && updateAction == UpdateAction.UPDATE_IF_VISIBLE)
                return;
            dgridMyOrdersHistory.DataSource = null;
            tradeLogic.GetMyOrdersHistory(UpdateMyOrdersHistory_UIResultHandler);
        }
        public void UpdateMyOrdersHistory_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            List<OrderDone> ordersHistory = (List<OrderDone>)resultResponse.items[0].result.resultData;

            var dataView = ordersHistory.Select(item => new
            {
                date = item.doneDate,
                orderType = item.orderType,
                price = Helper.PriceToStringBtc(item.price),
                amount = Helper.TradeAmountToString(item.quantity),
                remain = item.quantityRemaining
            }).ToList();
            List<DGVColumn> columns = new List<DGVColumn>()
            {
                new DGVColumn( "date", "Date","string") ,
                new DGVColumn( "orderType", "Type","string") ,
                new DGVColumn( "price", "Price","string") ,
                new DGVColumn( "amount", "Amount","string") ,
                new DGVColumn( "remain", "Remain","string")
            };
            DataGridViewWrapper gv = new DataGridViewWrapper(dgridMyOrdersHistory,true);
            gv.Create(dataView, columns);
            gv.AutoSizeDisplayedExcept("remain");
        }

        private void UpdateTradeState()
        {
            tradeLogic.UpdateTradeStateRequest(UpdateTradeState_UIResultHandler);
        }

        public void UpdateTradeState_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;

            Dictionary<string, Balance> balances = (Dictionary<string, Balance>)resultResponse.items[0].result.resultData;
            tradeLogic.UpdateBalance(balances);

            if (labelBalanceBase.Text != "_")
            {
                if (tradeLogic.balanceBase.balance != tradeLogic.prevBalanceBase.balance)
                    labelBalanceBaseBold.Start();
                if (tradeLogic.balanceCounter.balance != tradeLogic.prevBalanceCounter.balance)
                    labelBalanceMarketBold.Start();
            }
            labelBalanceBase.Text = tradeLogic.baseCurrencyName;
            labelBalanceMarket.Text = tradeLogic.counterCurrencyName;
            labelBalanceBaseValue.Text = Helper.PriceToStringBtc(tradeLogic.balanceBase.balance);
            labelBalanceMarketValue.Text = Helper.PriceToStringBtc(tradeLogic.balanceCounter.balance);
            labelAmountBuy.Text =  tradeLogic.counterCurrencyName;
            labelAmountSell.Text = tradeLogic.counterCurrencyName;

            TradeLast tradelast = (TradeLast)resultResponse.items[1].result.resultData;
            tradeLogic.lastMarketPrice = tradelast;

            TradeLast lastbtcInUsdtPrice = (TradeLast)resultResponse.items[2].result.resultData;
            TradeLast trade = tradeLogic.lastMarketPrice;

            buttonBuy.Text = "BUY  " + Helper.PriceToStringBtc(trade.ask);
            buttonSell.Text = "SELL  " + Helper.PriceToStringBtc(trade.bid);
            buttonBuy.BackColor = Color.FromArgb(buttoncolor.ToArgb());
            buttonSell.BackColor = Color.FromArgb(buttoncolor.ToArgb());
            if (trade.last >= trade.ask)
            {
                buttonBuy.BackColor = Color.Aquamarine;
            }
            if (trade.last <= trade.bid)
            {
                buttonSell.BackColor = Color.LightPink;//Color.LightPink;; 
            }
            labelSpread.Text = Helper.CalcSpread(trade.ask, trade.bid).ToString("0.00") + " %";
            double averageBtcPrice = (trade.ask + trade.bid) / 2;
            double btcInUsd = (lastbtcInUsdtPrice.bid + lastbtcInUsdtPrice.ask) / 2;
            double averageUsdPrice = averageBtcPrice;
            if (tradeLogic.baseCurrencyName != "USDT")
                averageUsdPrice = averageBtcPrice * btcInUsd;
            labelAverage.Text = "Average: " + Helper.PriceToStringBtc(averageBtcPrice) + " = " + Helper.PriceToString(averageUsdPrice) + "$";


        }
        private void timerTradeLast_Tick(object sender, EventArgs e)
        {
            UpdateTradeState();
        }
        private void timerTradeHistory_Tick(object sender, EventArgs e)
        {
            UpdateOrderBook(UpdateAction.UPDATE_IF_VISIBLE);
//            UpdateTradeHistory(UpdateAction.UPDATE_IF_VISIBLE);
        }

        private void buttonUpdateOrderBook_Click(object sender, EventArgs e)
        {
           UpdateOrderBook();
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
            tradeLogic.GetMyOpenOrders(buttonCancellAllOrders_UIResultHandler);
        }
        public void buttonCancellAllOrders_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            List<OpenOrder> myOpenOrders = (List<OpenOrder>)resultResponse.items[0].result.resultData;
            foreach (OpenOrder order in myOpenOrders)
            {
                tradeLogic.CancellMyOrder(order, CancellOrder_UIResultHandler);
                break;
            }
        }
        public void CancellOrder_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            //            List<OpenOrder> myOpenOrders = (List<OpenOrder>)resultResponse.result.resultData;
            UpdateMyOpenOrders();
        }
        private bool CheckTradeFieldsBuy()
        {
            if (!Helper.IsDouble(textAmountBuy.Text))
            {
                MessageBox.Show("Amount is Invalid");
                return false;
            }
            if (!Helper.IsDouble(textPriceBuy.Text))
            {
                MessageBox.Show("Price is Invalid");
                return false;
            }

            double quantity = Helper.ToDouble(textAmountBuy.Text);
            if (quantity <= 0)
            {
                MessageBox.Show("Amount must be > 0");
                return false;
            }

            double price = Helper.ToDouble(textPriceBuy.Text);
            if (price <= 0)
            {
                MessageBox.Show("Price must be > 0");
                return false;
            }
            return true;
        }
        private bool CheckTradeFieldsSell()
        {
            if (!Helper.IsDouble(textAmountSell.Text))
            {
                MessageBox.Show("Amount is Invalid");
                return false;
            }
            if (!Helper.IsDouble(textPriceSell.Text))
            {
                MessageBox.Show("Price is Invalid");
                return false;
            }

            double quantity = Helper.ToDouble(textAmountSell.Text);
            if (quantity <= 0)
            {
                MessageBox.Show("Amount must be > 0");
                return false;
            }

            double price = Helper.ToDouble(textPriceSell.Text);
            if (price <= 0)
            {
                MessageBox.Show("Price must be > 0");
                return false;
            }
            return true;
        }
        private void buttonBuy_Click(object sender, EventArgs e)
        {
            if (!CheckTradeFieldsBuy())
                return;
            double quantity = Helper.ToDouble(textAmountBuy.Text);
            double price = Helper.ToDouble(textPriceBuy.Text);

            string msg;
            msg = tradeLogic.BuyLimit(quantity, price, Buy_UIResultHandler);
            if (msg != "")
                Helper.Display(msg);

            /*
//                msg = tradeLogic.BuyMarket(quantity, Buy_UIResultHandler);
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    Helper.LogAndDisplay(ex, "Can't process buy order.Request TimeOut.");
                    return;
                }
                else throw;
            }
//            catch (MarketAPIException ex)
*/
        }
        public void Buy_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            UpdateMyOpenOrders();
        }
        private void buttonSell_Click(object sender, EventArgs e)
        {
            if (!CheckTradeFieldsSell())
                return;
            double quantity = Helper.ToDouble(textAmountSell.Text);
            double price = Helper.ToDouble(textPriceSell.Text);

            string msg;
            msg = tradeLogic.SellLimit(quantity, price, Sell_UIResultHandler);
            if (msg != "")
                Helper.Display(msg);
        }
        public void Sell_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            UpdateMyOpenOrders();
        }

        private void buttonSetAskPrice_Click(object sender, EventArgs e)
        {
            if (!Helper.IsDouble(textAmountBuy.Text))
            {   MessageBox.Show("Amount is Invalid");   return;       }
            //            tradeLogic.GetLastMarketPrice(SetAsk_UIResultHandler);
            textPriceBuy.Text = "";
            tradeLogic.GetOrderBook(SetAsk_UIResultHandler);
        }
        public void SetAsk_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            //            TradeLast tradelast = (TradeLast)resultResponse.result.resultData;
            //            if (tradelast.ask != 0) textPrice.Text = Helper.PriceToStringBtc(tradelast.ask);
            AllOrders orders = (AllOrders)resultResponse.items[0].result.resultData;

            double quantityToBuy = 0;
            if (Helper.IsDouble(textAmountBuy.Text))
                quantityToBuy = Helper.ToDouble(textAmountBuy.Text);
            else
            {
                MessageBox.Show("Amount is Invalid");
                return;
            }

            List<SellOrder> sellOrders = orders.sellOrders.Where(o => o.quantity >= quantityToBuy).OrderBy(o => o.rate).ToList();
            if (sellOrders.Count > 0)
            {
//                double price = tradeLogic.LastAskFromOrderBook(quantity);
                  textPriceBuy.Text = Helper.PriceToStringBtc(sellOrders[0].rate);
            }

        }

        private void buttonSetBidPrice_Click(object sender, EventArgs e)
        {
            textPriceSell.Text = "";
            tradeLogic.GetOrderBook(SetBid_UIResultHandler);
        }
        public void SetBid_UIResultHandler(RequestItemGroup resultResponse)
        {
            if (RequestManager.IsResultHasErrors(resultResponse))
                return;
            AllOrders orders = (AllOrders)resultResponse.items[0].result.resultData;

            double quantityToSell = 0;
            if (Helper.IsDouble(textAmountSell.Text))
                quantityToSell = Helper.ToDouble(textAmountSell.Text);
            else
            {  MessageBox.Show("Amount is Invalid");  return;  }

            List<BuyOrder> buyOrders = orders.buyOrders.Where(o => o.quantity >= quantityToSell).OrderByDescending(o => o.rate).ToList();
            if (buyOrders.Count > 0)
                textPriceSell.Text = Helper.PriceToStringBtc(buyOrders[0].rate);
        }

        private void buttonSellAmountAll_Click(object sender, EventArgs e)
        {
            textAmountSell.Text = Helper.PriceToStringBtc(tradeLogic.balanceCounter.balance);
        }

        private void buttonSellAmount50_Click(object sender, EventArgs e)
        {
            textAmountSell.Text = Helper.PriceToStringBtc(tradeLogic.balanceCounter.balance/2);
        }

        private void buttonBuyAmountAll_Click(object sender, EventArgs e)
        {
            textAmountBuy.Text = "0";
            if (tradeLogic.lastMarketPrice!=null)
            {
                double ask=tradeLogic.lastMarketPrice.ask;
                double balanceBase=tradeLogic.balanceBase.balance;
                double amount = balanceBase / ask;
                textAmountBuy.Text = Helper.PriceToStringBtc(amount*0.98);
            }
        }

        private void buttonBuyAmount50_Click(object sender, EventArgs e)
        {
            textAmountBuy.Text = "0";
            if (tradeLogic.lastMarketPrice != null)
            {
                double ask = tradeLogic.lastMarketPrice.ask;
                double balanceBase = tradeLogic.balanceBase.balance;
                double amount = balanceBase / ask;
                textAmountBuy.Text = Helper.PriceToStringBtc(amount/2);
            }
        }

        private void buttonShowGraphic_Click(object sender, EventArgs e)
        {
            FormChart form = new FormChart(ExchangeManager.GetMarketByMarketName(tradeLogic.GetMarketName()), tradeLogic.ticker);
            form.parent = this;
            form.Show();

        }
    }
}
