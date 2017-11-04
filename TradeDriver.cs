using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows.Forms;

namespace BitWhiskey
{
    public abstract class TradeDriver
    {
        public string ticker { get; set; }
        Pair tickerinfo;

        public string baseCurrencyName { get; set; }
        public string counterCurrencyName { get; set; }
        protected Market market;

        public Balance balanceBase { get; set; }
        public Balance balanceCounter { get; set; }
        public List<SellOrder> sellOrders { get; set; }
        public List<BuyOrder> buyOrders { get; set; }
        public List<Trade> tradeHistory { get; set; }
        public List<OpenOrder> myOpenOrders { get; set; }
        public List<OrderDone> myOrdersHistory { get; set; }
        public TradeLast lastMarketPrice { get; set; }
        public MyTradeState myTradeState { get; set; }
        public TradeLast lastbtcInUsdtPrice { get; set; }

        public TradeDriver(string ticker_)
        {
            ticker = ticker_;
            tickerinfo = new Pair(ticker);
            baseCurrencyName = tickerinfo.currency1;
            counterCurrencyName = tickerinfo.currency2;
        }
        public virtual string GetMarketName()
        {
            return market.MarketName();
        }
        public virtual void GetBalances()
        {
            market.GetBalances();
            Dictionary<string, Balance> balances = market.balances;
            if (balances.ContainsKey(baseCurrencyName))
                balanceBase = balances[baseCurrencyName];
            else
                balanceBase = new Balance() { currency = baseCurrencyName, balance = 0 };
            if (balances.ContainsKey(counterCurrencyName))
                balanceCounter = balances[counterCurrencyName];
            else
                balanceCounter = new Balance() { currency = counterCurrencyName, balance = 0 };
        }
        public virtual void GetOrderBook()
        {
            sellOrders = null;
            buyOrders = null;
            market.GetOrderBook(ticker);
            List<SellOrder> sOrders = market.sellOrders;
            List<BuyOrder> bOrders = market.buyOrders;

            sellOrders = sOrders.OrderBy(o => o.rate).ToList();
            buyOrders = bOrders.OrderByDescending(o => o.rate).ToList();

        }

        public virtual void GetTradeHistory()
        {
            tradeHistory = null;
            market.GetTradeHistory(ticker);
            List<Trade> trades = market.tradeHistory;
            tradeHistory = trades.OrderByDescending(o => o.tradeDate).ToList();
        }
        public virtual void GetMyOpenOrders()
        {
            myOpenOrders = null;
            market.GetOpenOrders(ticker);
            List<OpenOrder> orders = market.openOrders;
            myOpenOrders = orders.OrderByDescending(o => o.openedDate).ToList();
        }
        public virtual void CancellMyOrder(OpenOrder order)
        {
            market.OrderCancel(order.openUuid);
        }

        public virtual void GetMyOrdersHistory()
        {
            myOrdersHistory = null;
            market.GetMyOrdersHistory(ticker);
            List<OrderDone> orders = market.myOrdersHistory;
            myOrdersHistory = orders.OrderByDescending(o => o.doneDate).ToList();
        }

        public virtual void GetLastMarketPrice()
        {
            lastMarketPrice = null;
            market.GetTradeLast(ticker);
            lastMarketPrice = market.tradelast;
        }
        public virtual void GetLastMarketPriceBtcUsdt()
        {
            lastbtcInUsdtPrice = null;
            market.GetTradeLast("USDT_BTC");
            lastbtcInUsdtPrice = market.tradelast;

        }

        public virtual void BuyMarket(double quantity)
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
                    /*
                    if(baseCurrencyName == "USDT" || baseCurrencyName == "BTC")
                    {
                        if (quantity * sellerOrderPrice < 0.00070000)
                            checkSmallQuantity = false;
                    }
                    if (checkSmallQuantity)
                    {
                    */

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
        public virtual void BuyLimit(double quantity, double price)
        {
            myTradeState = new MyTradeState();
            myTradeState.completedOk = false;
            myTradeState.errMsg = "";

            //GetLastMarketPrice();
            //double diff = Helper.CalcSpread(lastMarketPrice,price);
            //    if (diff > 6)  myTradeState.errMsg = "Spread is too high: " + spread.ToString();
            if (price * quantity > market.balances[baseCurrencyName].balance)
            {
                myTradeState.errMsg = "Not enough balance in " + baseCurrencyName;
                return;
            }

            myTradeState.completedOk = market.OrderBuyLimit(ticker, price, quantity);
        }

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
        public virtual void SellLimit(double quantity, double price)
        {
            myTradeState = new MyTradeState();
            myTradeState.completedOk = false;
            myTradeState.errMsg = "";

            if (quantity > market.balances[counterCurrencyName].balance)
            {
                myTradeState.errMsg = "Not enough balance in " + counterCurrencyName;
                return;
            }
            myTradeState.completedOk = market.OrderSellLimit(ticker, price, quantity);
        }
        public double LastAskFromOrderBook(double quantityToBuy)
        {
            List<SellOrder> sellOrders = market.sellOrders.Where(o => o.quantity >= quantityToBuy).OrderBy(o => o.rate).ToList();
            if (sellOrders.Count > 0)
                return sellOrders[0].rate;

            return 0;
        }
        public double LastBidFromOrderBook(double quantityToSell)
        {
            List<BuyOrder> buyOrders = market.buyOrders.Where(o => o.quantity >= quantityToSell).OrderByDescending(o => o.rate).ToList();
            if (buyOrders.Count > 0)
                return buyOrders[0].rate;

            return 0;
        }



    }
}
