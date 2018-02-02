using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BitWhiskey
{


    public class FormTradeLogic
    {

        public TradeRequestHandlers tradeHandlers;

        public string ticker { get; set; }
        Pair tickerinfo;
        public string baseCurrencyName { get; set; }
        public string counterCurrencyName { get; set; }

        public Balance balanceBase= new Balance();
        public Balance balanceCounter= new Balance();
        public Balance prevBalanceBase = new Balance();
        public Balance prevBalanceCounter = new Balance();

        public TradeLast lastMarketPrice=null;

        protected Market market;

        public FormTradeLogic(string ticker_, Market market_)
        {
            ticker = ticker_;
            tickerinfo = new Pair(ticker);
            baseCurrencyName = tickerinfo.currency1;
            counterCurrencyName = tickerinfo.currency2;
            market = market_;
            tradeHandlers = new TradeRequestHandlers(market);
        }
        public virtual string GetMarketName()
        {
            return market.MarketName();
        }
        public virtual Market GetMarket()
        {
            return market;
        }
        public virtual void SendRequestToQueue(string requestString, Action<RequestItem> ProcessResultAction,Action<RequestItemGroup> ProcessResultUIAction,RequestParams reqparam=null)
        {
            RequestItemGroup itemgroup = new RequestItemGroup(ProcessResultUIAction);
            itemgroup.AddItem(requestString, ProcessResultAction, reqparam);
            RequestConsumer.requestManager.AddItemGroup(GetMarketName(), itemgroup);
        }
        public virtual void GetBalances(Action<RequestItemGroup> ProcessResultUIAction)
        {
            SendRequestToQueue(market.GetBalancesBegin(), tradeHandlers.GetBalances_RequestHandler, ProcessResultUIAction);
        }
        public virtual void UpdateBalance(Dictionary<string, Balance> balances)
        {
            prevBalanceBase = balanceBase;
            prevBalanceCounter = balanceCounter;

            if (balances.ContainsKey(baseCurrencyName))
                balanceBase = balances[baseCurrencyName];
            else
                balanceBase = new Balance() { currency = baseCurrencyName, balance = 0 };

            if (balances.ContainsKey(counterCurrencyName))
                balanceCounter = balances[counterCurrencyName];
            else
                balanceCounter = new Balance() { currency = counterCurrencyName, balance = 0 };

        }
        public virtual void UpdateTradeStateRequest(Action<RequestItemGroup> ProcessResultUIAction)
        {
            string usdTicker = "USD_BTC";
            if (Global.marketsState.curMarketPairs[GetMarketName()].ContainsKey("USDT_BTC"))
                usdTicker = "USDT_BTC";

            RequestParams reqparam = new RequestParams() { ticker = this.ticker };
            RequestItemGroup itemgroup = new RequestItemGroup(ProcessResultUIAction);
            itemgroup.AddItem(market.GetBalancesBegin(), tradeHandlers.GetBalances_RequestHandler);
            itemgroup.AddItem(market.GetTradeLastBegin(ticker), tradeHandlers.GetTradeLast_RequestHandler, reqparam);
            RequestParams reqparamusd = new RequestParams() { ticker = usdTicker };
            itemgroup.AddItem(market.GetTradeLastBegin(usdTicker), tradeHandlers.GetTradeLast_RequestHandler, reqparamusd);
            RequestConsumer.requestManager.AddItemGroup(GetMarketName(), itemgroup);
        }

        public virtual void GetTradePairs(Action<RequestItemGroup> ProcessResultUIAction)
        {
            SendRequestToQueue(market.GetTradePairsBegin(), tradeHandlers.GetTradePairs_RequestHandler, ProcessResultUIAction);
        }

        public virtual void GetOrderBook(Action<RequestItemGroup> ProcessResultUIAction)
        {
            SendRequestToQueue(market.GetOrderBookBegin(ticker), tradeHandlers.GetOrderBook_RequestHandler, ProcessResultUIAction);
        }

        public virtual void GetTradeHistory(Action<RequestItemGroup> ProcessResultUIAction)
        {
            SendRequestToQueue(market.GetTradeHistoryBegin(ticker), tradeHandlers.GetTradeHistory_RequestHandler, ProcessResultUIAction);
        }
        public virtual void GetMyOpenOrders(Action<RequestItemGroup> ProcessResultUIAction)
        {
            RequestParams reqparam = new RequestParams() { ticker = this.ticker };
            SendRequestToQueue(market.GetOpenOrdersBegin(ticker), tradeHandlers.GetOpenOrders_RequestHandler, ProcessResultUIAction, reqparam);
        }
        public virtual void CancellMyOrder(OpenOrder order, Action<RequestItemGroup> ProcessResultUIAction)
        {
            SendRequestToQueue(market.OrderCancelBegin(order.openUuid), tradeHandlers.OrderCancel_RequestHandler, ProcessResultUIAction);
        }

        public virtual void GetMyOrdersHistory(Action<RequestItemGroup> ProcessResultUIAction)
        {
            RequestParams reqparam = new RequestParams() { ticker = this.ticker };
            SendRequestToQueue(market.GetMyOrdersHistoryBegin(ticker), tradeHandlers.GetMyOrdersHistory_RequestHandler, ProcessResultUIAction, reqparam);
        }
        public virtual void GetLastMarketPrice(Action<RequestItemGroup> ProcessResultUIAction)
        {
            RequestParams reqparam = new RequestParams() {ticker=this.ticker };
            SendRequestToQueue(market.GetTradeLastBegin(ticker), tradeHandlers.GetTradeLast_RequestHandler, ProcessResultUIAction, reqparam);
        }
        public virtual void GetLastMarketPriceBtcUsdt(Action<RequestItemGroup> ProcessResultUIAction)
        {
            string usdTicker = "USD_BTC";
            if (Global.marketsState.curMarketPairs[GetMarketName()].ContainsKey("USDT_BTC"))
                usdTicker = "USDT_BTC";
            RequestParams reqparam = new RequestParams() { ticker = usdTicker };
            SendRequestToQueue(market.GetTradeLastBegin(usdTicker), tradeHandlers.GetTradeLast_RequestHandler, ProcessResultUIAction, reqparam);

            //            lastbtcInUsdtPrice = market.tradelast;
        }
        //            market.GetPriceHistoryByPeriod(ticker, "Min5", DateTime.Now.AddDays(-6), DateTime.Now.AddYears(10));
        public virtual void GetPriceHistoryByPeriod(string interval, DateTime start, DateTime end,Action<RequestItemGroup> ProcessResultUIAction)
        {
            SendRequestToQueue(market.GetPriceHistoryByPeriodBegin(ticker, interval,start,end), tradeHandlers.GetPriceHistoryByPeriod_RequestHandler, ProcessResultUIAction);
        }

        public virtual string BuyLimit(double quantity, double price, Action<RequestItemGroup> ProcessResultUIAction)
        {
            //GetLastMarketPrice();
            //double diff = Helper.CalcSpread(lastMarketPrice,price);
            //    if (diff > 6)  myTradeState.errMsg = "Spread is too high: " + spread.ToString();
            if (price * quantity > balanceBase.balance)
                return "Not enough balance in " + baseCurrencyName;
            SendRequestToQueue(market.OrderBuyLimitBegin(ticker, price, quantity), tradeHandlers.OrderBuyLimit_RequestHandler, ProcessResultUIAction);

            return "";
        }
        public virtual string SellLimit(double quantity, double price, Action<RequestItemGroup> ProcessResultUIAction)
        {
            if (quantity > balanceCounter.balance)
                return "Not enough balance in " + counterCurrencyName;
            SendRequestToQueue(market.OrderSellLimitBegin(ticker, price, quantity), tradeHandlers.OrderSellLimit_RequestHandler, ProcessResultUIAction);

            return "";
        }
        /*
        public double LastAskFromOrderBook(double quantityToBuy)
        {
            List<SellOrder> sellOrders = market.sellOrders.Where(o => o.quantity >= quantityToBuy).OrderBy(o => o.rate).ToList();
            if (sellOrders.Count > 0)
                return sellOrders[0].rate;

            return 0;
        }
*/

        /*
    public virtual void BuyMarket(double quantity, Action<RequestItem> ProcessResultUIAction)
    {
        myTradeState = new MyTradeState();
        myTradeState.completedOk = false;
        myTradeState.errMsg = "";


        if (quantity > 0)
        {
            GetOrderBook();

            double sellerOrderPrice = 0;
            double buyerOrderPrice = 0;
            double spread = 0;
            List<SellOrder> sellOrders = market.sellOrders.Where(o => o.quantity >= quantity).OrderBy(o => o.rate).ToList();
            List<BuyOrder> buyOrders = market.buyOrders.Where(o => o.quantity >= quantity).OrderByDescending(o => o.rate).ToList();

            if (sellOrders.Count > 0 && buyOrders.Count > 0)
            {
                sellerOrderPrice = sellOrders[0].rate;
                buyerOrderPrice = buyOrders[0].rate;
                spread = Helper.CalcSpread(sellerOrderPrice, buyerOrderPrice);
                bool checkSmallQuantity = true;
                //                    if(baseCurrencyName == "USDT" || baseCurrencyName == "BTC")
                //                    {
                //                       if (quantity * sellerOrderPrice < 0.00070000)
                //                           checkSmallQuantity = false;
                //                   }
                //                   if (checkSmallQuantity)
                //                   {

                if (spread < 2)
                {
                    if (sellerOrderPrice * quantity <= market.balances[baseCurrencyName].balance)
                    {
                        myTradeState.completedOk = market.OrderBuyLimit(ticker, sellerOrderPrice, quantity);
                    }
                    else
                    {
                        myTradeState.errMsg = "Not enough balance in " + baseCurrencyName;
                    }
                }
                else
                {
                    myTradeState.errMsg = "Spread is too high: " + spread.ToString();
                }
                //                    }   else   {    myTradeState.errMsg = "Amount is very small: " + quantity.ToString();  }
            }
            else
            {
                myTradeState.errMsg = "Not found matching orders in orderbook";
            }
        }
        else
        {
            myTradeState.errMsg = "Amount must be > 0";
        }


    }
    */


        /*
        public virtual void SellMarket(double quantity)
        {
            myTradeState = new MyTradeState();
            myTradeState.completedOk = false;
            myTradeState.errMsg = "";

            if (quantity > 0)
            {
                GetOrderBook();

                double sellerOrderPrice = 0;
                double buyerOrderPrice = 0;
                double spread = 0;
                List<SellOrder> sellOrders = market.sellOrders.Where(o => o.quantity >= quantity).OrderBy(o => o.rate).ToList();
                List<BuyOrder> buyOrders = market.buyOrders.Where(o => o.quantity >= quantity).OrderByDescending(o => o.rate).ToList();

                if (sellOrders.Count > 0 && buyOrders.Count > 0)
                {
                    sellerOrderPrice = sellOrders[0].rate;
                    buyerOrderPrice = buyOrders[0].rate;
                    spread = Helper.CalcSpread(sellerOrderPrice, buyerOrderPrice);
                    //                    if (quantity * buyerOrderPrice > 0.00070000)
                    //                    {
                    if (spread < 2)
                    {
                        if (quantity <= market.balances[counterCurrencyName].balance)
                        {
                            myTradeState.completedOk = market.OrderSellLimit(ticker, buyerOrderPrice, quantity);
                            //                            myTradeState.errMsg =
                        }
                        else
                        {
                            myTradeState.errMsg = "Not enough balance in " + counterCurrencyName;
                        }
                    }
                    else
                    {
                        myTradeState.errMsg = "Spread is too high: " + spread.ToString();
                    }
                    //                    }   else   {    myTradeState.errMsg = "Amount is very small: " + quantity.ToString();  }
                }
                else
                {
                    myTradeState.errMsg = "Not found matching orders in orderbook";
                }
            }
            else
            {
                myTradeState.errMsg = "Amount must be > 0";
            }

        }
        */


    }



    public class BlinkControl
    {
        private int blinkCount = 0;
        private int maxBlinkCount = 3;
        private Label label;
        private Timer tmrBlink;
        private Font origFont;

        public void Create(Label label_)
        {
            label = label_;
            origFont = label.Font;
        }
        public void Start(int interval=700)
        {
            blinkCount = 0;
            label.Font=new Font(label.Font, FontStyle.Bold);
            if (tmrBlink != null)
              tmrBlink.Dispose();
            tmrBlink = new Timer();
            tmrBlink.Interval = interval;
            tmrBlink.Tick += new EventHandler(tmrBlink_Tick);
            tmrBlink.Start();
        }

        private void tmrBlink_Tick(object sender, EventArgs e)
        {
            blinkCount++;
            if (blinkCount == maxBlinkCount)
            {
                tmrBlink.Stop();
                label.Font = origFont;
            }
        }
    }

}
