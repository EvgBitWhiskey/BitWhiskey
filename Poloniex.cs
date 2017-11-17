using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Newtonsoft.Json;


namespace BitWhiskey
{
    public class PPriceHistoryResult
    {
        public int date;
        public float high;
        public float low;
        public float open;
        public float close;
        public float volume;
        public float quoteVolume;
        public float weightedAverage;
    }

    public class PTradeHistory
    {
        public int globalTradeID { get; set; }
        public int tradeID { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string rate { get; set; }
        public string amount { get; set; }
        public string total { get; set; }
    }

    public class POrderBook
    {
        public List<List<object>> asks { get; set; }
        public List<List<object>> bids { get; set; }
        public string isFrozen { get; set; }
        public int seq { get; set; }
    }


    public class PMyTradeHistory
    {
        public int globalTradeID { get; set; }
        public string tradeID { get; set; }
        public string date { get; set; }
        public string rate { get; set; }
        public string amount { get; set; }
        public string total { get; set; }
        public string fee { get; set; }
        public string orderNumber { get; set; }
        public string type { get; set; }
        public string category { get; set; }
    }


    public class POpenOrders
    {
        public string orderNumber { get; set; }
        public string type { get; set; }
        public string rate { get; set; }
        public string amount { get; set; }
        public string total { get; set; }
    }


    public class PCancelOrder
    {
        public int success { get; set; }
    }

    public class PBuySellResultingTrade
    {
        public string amount { get; set; }
        public string date { get; set; }
        public string rate { get; set; }
        public string total { get; set; }
        public string tradeID { get; set; }
        public string type { get; set; }
    }

    public class PBuySell
    {
        public string orderNumber { get; set; }
        public List<PBuySellResultingTrade> resultingTrades { get; set; }
    }

    public class PError
    {
        public string error { get; set; }
    }

    


    //https://poloniex.com/support/api/

    public class Poloniex : Market
    {
        static readonly object _locker = new object();
        protected static int currentNonce;

        public Poloniex()
        {
            PUBLIC_API = "https://poloniex.com/public?command=";
            KEY_API = "https://poloniex.com/tradingApi";
            signBaseUrl = "";
            publicMethod = "POST";
            keyMethod = "POST";
            includeParametersInRequestAddress = false;

            int datenonce = Helper.ToUnixTimeStamp(DateTime.UtcNow);
            lock (_locker)
            {
                currentNonce = datenonce;
            }
            key = "";
            secret = "";
            if (Global.settingsMain.poloniexkey != "")
            {
                key = AppCrypt.DecryptData(Global.settingsMain.poloniexkey);
                haveKey = true;
            }
            if (Global.settingsMain.poloniexsecret != "")
            { 
                secret = AppCrypt.DecryptData(Global.settingsMain.poloniexsecret);
            }
        }

        private void CheckResponseAndThrow(string response)
        {
            if (response.Length > 13 && response.Substring(0, 13).Contains("\"error\""))
            {
                PError err = Newtonsoft.Json.JsonConvert.DeserializeObject<PError>(response);
                //lastRequestMsg = err.error;
                //lastRequestStatus = false;
                throw new MarketAPIException("Market API Error:" + err.error);
            }
        }

    public int GetNonce()
        {
            int nonce = 0;
            lock (_locker)
            {
                currentNonce++;
                nonce = currentNonce;
            }
            return nonce;
            //            return Helper.ToUnixTimeStamp(DateTime.UtcNow);
        }

        public override string MarketName()
        {
            return this.GetType().Name;
        }

        public override WebRequest AddKeyHeaders(Cryptor cryptor, WebRequest webRequest)
        {
            webRequest.Headers.Add("Key", key);
            webRequest.Headers.Add("Sign", cryptor.sign);
            return webRequest;
        }
        public override string ToOriginalTicker(string ticker)
        {
            return ticker.Replace("-", "_");
        }
        public override string ConvertPriceIntervalParam(string interval)
        {
            switch (interval)
            {
                case "Day":
                    return "86400";
                case "Hour4":
                    return "14400";
                case "Hour2":
                    return "7200";
                case "Min30":
                    return "1800";
                case "Min15":
                    return "900";
                case "Min5":
                    return "300";

            }

            return "";
        }

        public override string GetOrderBookBegin(string ticker)
        {
            //https://poloniex.com/public?command=returnOrderBook&currencyPair=BTC_NXT&depth=10
            ticker = ToOriginalTicker(ticker);

            string parameters = "returnOrderBook&currencyPair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&depth = 10";
            return parameters;

        }
        public override AllOrders GetOrderBookEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            POrderBook items = Newtonsoft.Json.JsonConvert.DeserializeObject<POrderBook>(response);
            AllOrders orders = new AllOrders();
            orders.buyOrders = new List<BuyOrder>();
            orders.sellOrders = new List<SellOrder>();
            int n = 0;
            foreach (var item in items.bids)
            {
                n++;
                orders.buyOrders.Add(new BuyOrder { quantity = Helper.ToDouble(item[1].ToString()), rate = Helper.ToDouble((string)item[0]) });
            }
            n = 0;
            foreach (var item in items.asks)
            {
                n++;
                orders.sellOrders.Add(new SellOrder { quantity = Helper.ToDouble(item[1].ToString()), rate = Helper.ToDouble((string)item[0]) });
            }
            orders.sellOrders = orders.sellOrders.OrderBy(o => o.rate).ToList();
            orders.buyOrders = orders.buyOrders.OrderByDescending(o => o.rate).ToList();

            return orders;

        }
        public override string GetTradePairsBegin()
        {
            //https://poloniex.com/public?command=returnCurrencies
            //https://poloniex.com/public?command=returnTicker
            string parameters = "returnTicker&nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, TradePair> GetTradePairsEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

            Dictionary<string, TradePair>  tradePairs = new Dictionary<string, TradePair>();
            int n = 0;
            foreach (var item in stuff)
            {
                n++;
                TradePair pair = new TradePair
                {
                    currency1 = "",
                    currency2 = "",
                    ticker = item.Name,
                    isActive = true
                };
                //                if (item.Value["disabled"].ToString() == "1")
                //                    pair.isActive = false;

                Pair pairinfo = new Pair(pair.ticker);
                pair.currency1 = pairinfo.currency1;
                pair.currency2 = pairinfo.currency2;
                tradePairs.Add(pair.ticker, pair);
            }
            return tradePairs;
        }
        public override string GetTradeHistoryBegin(string ticker)
        {
            //https://poloniex.com/public?command=returnTradeHistory&currencyPair=BTC_NXT&start=1410158341&end=1410499372
            ticker = ToOriginalTicker(ticker);

            string parameters = "returnTradeHistory&currencyPair=" + ticker + "&nonce=" + GetNonce();
            return parameters;
        }
        public override List<Trade> GetTradeHistoryEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            List<PTradeHistory> items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PTradeHistory>>(response);
            int n = 0;
            List<Trade> tradeHistory = new List<Trade>();
            foreach (PTradeHistory item in items)
            {
                n++;
                Trade trade = new Trade
                {
                    id = item.globalTradeID,
                    tradeDate = Helper.StringToDateTimeExact(item.date),
                    quantity = Helper.ToDouble(item.amount),
                    price = Helper.ToDouble(item.rate),
                    total = Helper.ToDouble(item.total),
                    orderType = item.type.ToUpper(),
                    fillType = ""
                };

                tradeHistory.Add(trade);
            }
            tradeHistory = tradeHistory.OrderByDescending(o => o.tradeDate).ToList();

            return tradeHistory;
        }

        public override string GetTradeLastBegin(string ticker)
        {
            //https://poloniex.com/public?command=returnTicker
            string parameters = "returnTicker&nonce=" + GetNonce();
            return parameters;
        }
        public override TradeLast GetTradeLastEnd(string parameters, string ticker)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

            Pair tickerinfo = new Pair(ticker);

            TradeLast tradelast =new TradeLast();
            foreach (var item in stuff)
            {
                Pair iteminfo = new Pair(item.Name);
                if (tickerinfo.currency1 == iteminfo.currency1 && tickerinfo.currency2 == iteminfo.currency2)
                {
                    tradelast = new TradeLast { ask = Helper.ToDouble((string)item.Value["lowestAsk"]), bid = Helper.ToDouble((string)item.Value["highestBid"]), last = Helper.ToDouble((string)item.Value["last"]) };
                    break;
                }
            }

            return tradelast;
        }
        public override string GetBalancesBegin()
        {
            string parameters = "command=returnBalances" + "&nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, Balance> GetBalancesEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);
            CheckResponseAndThrow(response);
            dynamic jdata = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

            Dictionary<string, Balance> balances;
            balances = new Dictionary<string, Balance>();
            int n = 0;
            foreach (var item in jdata)
            {
                n++;
                balances.Add(item.Name, new Balance { currency = item.Name, balance = Helper.ToDouble(item.Value.ToString().Replace("\"", "")) });
            }

            return balances;
        }
        public override string GetOpenOrdersBegin(string ticker)
        {
            // returnOpenOrders   
            string parameters = "command=returnOpenOrders&currencyPair=" + ticker + " &nonce=" + GetNonce();
            return parameters;
        }
        public override List<OpenOrder> GetOpenOrdersEnd(string parameters, string ticker)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            List<POpenOrders> jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<POpenOrders>>(response);

            List<OpenOrder> openOrders = new List<OpenOrder>();
            foreach (POpenOrders item in jdata)
            {
                OpenOrder order = new OpenOrder
                {
                    uuid = item.orderNumber,
                    openUuid = item.orderNumber,
                    orderType = item.type,
                    ticker = ticker,
                    quantity = Helper.ToDouble(item.amount),
                    quantityRemaining = 0,
                    price = Helper.ToDouble(item.rate),
                    openedDate = DateTime.Now
                };
                if (order.orderType == "sell")
                    order.orderType = "SELL LIMIT";
                if (order.orderType == "buy")
                    order.orderType = "BUY LIMIT";

                openOrders.Add(order);

            }
            openOrders = openOrders.OrderByDescending(o => o.openedDate).ToList();
            return openOrders;
        }
        public override string OrderCancelBegin(string idOrder)
        {
            // cancelOrder   
            string parameters = "command=cancelOrder&orderNumber=" + idOrder + "&nonce=" + GetNonce();
            return parameters;
        }
        public override string OrderCancelEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            PCancelOrder jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PCancelOrder>(response);
            return "";
        }
        public override string OrderBuyLimitBegin(string ticker, double rate, double quantity)
        {
            // buy   
            ticker = ToOriginalTicker(ticker);
            string parameters = "command=buy&currencyPair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&amount=" + quantity.ToString();
            parameters += "&rate=" + rate.ToString();
            return parameters; 
        }
        public override string OrderBuyLimitEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            PBuySell jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PBuySell>(response);
            return "";
        }
        public override string OrderSellLimitBegin(string ticker, double rate, double quantity)
        {
            // buy   
            ticker = ToOriginalTicker(ticker);
            string parameters = "command=sell&currencyPair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&amount=" + quantity.ToString();
            parameters += "&rate=" + rate.ToString();
            return parameters;
        }
        public override string OrderSellLimitEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            PBuySell jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PBuySell>(response);
            return "";
        }
        public override string GetMyOrdersHistoryBegin(string ticker)
        {
            // returnTradeHistory + all   
            string parameters = "command=returnTradeHistory&currencyPair=" + ticker + " &nonce=" + GetNonce();
            return parameters;
        }
        public override List<OrderDone> GetMyOrdersHistoryEnd(string parameters, string ticker)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            List<PMyTradeHistory> jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PMyTradeHistory>>(response);

            List<OrderDone> myOrdersHistory = new List<OrderDone>();
            foreach (PMyTradeHistory item in jdata)
            {
                OrderDone order = new OrderDone
                {
                    uuid = item.globalTradeID.ToString(),
                    ticker = ticker,
                    doneDate = Helper.StringToDateTimeExact(item.date),
                    orderType = item.type,
                    price = Helper.ToDouble(item.rate),
                    quantity = Helper.ToDouble(item.amount),
                    quantityRemaining = 0,
                    commission = Helper.ToDouble(item.fee),
                    totalSum = Helper.ToDouble(item.total)
                };
                if (order.orderType == "LIMIT_SELL")
                    order.orderType = "S";
                if (order.orderType == "LIMIT_BUY")
                    order.orderType = "B";

                myOrdersHistory.Add(order);

            }
            myOrdersHistory = myOrdersHistory.OrderByDescending(o => o.doneDate).ToList();
            return myOrdersHistory;

        }
        public override string GetPriceHistoryByPeriodBegin(string ticker, string interval, DateTime start, DateTime end)
        {
            //https://poloniex.com/public?command=returnChartData&currencyPair=USDT_BTC&start=1494304000&end=9999999999&period=900
            ticker = ToOriginalTicker(ticker);
            interval = ConvertPriceIntervalParam(interval);

            string parameters = "returnChartData&currencyPair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&start=" + Helper.ToUnixTimeStamp(start);
            parameters += "&end=" + Helper.ToUnixTimeStamp(end);
            parameters += "&period=" + interval;
            return parameters;

        }
        public override Dictionary<int, PriceCandle> GetPriceHistoryByPeriodEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            List<PPriceHistoryResult> items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PPriceHistoryResult>>(response);
            int n = 0;
            Dictionary<int, PriceCandle> priceHistory = new Dictionary<int, PriceCandle>();
            foreach (PPriceHistoryResult item in items)
            {
                if (item.high / item.low > 3000)
                    continue;
                n++;
                priceHistory.Add(item.date, new PriceCandle { date = item.date, open = item.open, high = item.high, low = item.low, close = item.close, volume = item.volume });
            }
            return priceHistory;

        }
        public override string GetMarketCurrentBegin()
        {
            //https://poloniex.com/public?command=returnTicker
            string parameters = "returnTicker&nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, MarketCurrent>  GetMarketCurrentEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

            Dictionary<string, MarketCurrent> markets = new Dictionary<string, MarketCurrent>();
            foreach (var item in stuff)
            {
                Pair iteminfo = new Pair(item.Name);
                MarketCurrent mkt = new MarketCurrent();
                mkt.ticker = item.Name;
                mkt.lastPrice = Helper.ToDouble((string)item.Value["last"]);
                mkt.volumeBtc = 0;
                mkt.volumeUSDT = 0;
                if (iteminfo.currency1=="BTC")
                  mkt.volumeBtc = Helper.ToDouble((string)item.Value["baseVolume"]);
                else if (iteminfo.currency1 == "USDT")
                    mkt.volumeUSDT = Helper.ToDouble((string)item.Value["baseVolume"]);
//                mkt.volumeBtc = Helper.ToDouble((string)item.Value["quoteVolume"]);
                mkt.percentChange = Helper.ToDouble((string)item.Value["percentChange"])*100.0;
                markets.Add(item.Name,mkt);
            }

            return markets;
        }


    }



}
