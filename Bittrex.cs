using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Windows;
using Newtonsoft.Json;


namespace BitWhiskey
{
    public class BCurrencyBalance
    {
        public string Currency { get; set; }
        public double Balance { get; set; }
        public double Available { get; set; }
        public double Pending { get; set; }
        public string CryptoAddress { get; set; }
    }

    public class BBalances
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BCurrencyBalance> result { get; set; }
    }

    public class BOrderBookBuy
    {
        public double Quantity { get; set; }
        public double Rate { get; set; }
    }

    public class BOrderBookSell
    {
        public double Quantity { get; set; }
        public double Rate { get; set; }
    }

    public class BOrderBookResult
    {
        public List<BOrderBookBuy> buy { get; set; }
        public List<BOrderBookSell> sell { get; set; }
    }

    public class BOrderBook
    {
        public bool success { get; set; }
        public string message { get; set; }
        public BOrderBookResult result { get; set; }
    }

    public class BOpenOrdersResult
    {
        public object Uuid { get; set; }
        public string OrderUuid { get; set; }
        public string Exchange { get; set; }
        public string OrderType { get; set; }
        public double Quantity { get; set; }
        public double QuantityRemaining { get; set; }
        public double Limit { get; set; }
        public double CommissionPaid { get; set; }
        public double Price { get; set; }
        public object PricePerUnit { get; set; }
        public DateTime Opened { get; set; }
        public object Closed { get; set; }
        public bool CancelInitiated { get; set; }
        public bool ImmediateOrCancel { get; set; }
        public bool IsConditional { get; set; }
        public object Condition { get; set; }
        public object ConditionTarget { get; set; }
    }

    public class BOpenOrders
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BOpenOrdersResult> result { get; set; }
    }

    public class BMarketsResult
    {
        public string MarketCurrency { get; set; }
        public string BaseCurrency { get; set; }
        public string MarketCurrencyLong { get; set; }
        public string BaseCurrencyLong { get; set; }
        public double MinTradeSize { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }

    public class BMarkets
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BMarketsResult> result { get; set; }
    }


    public class BTradeHistoryResult
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public string FillType { get; set; }
        public string OrderType { get; set; }
    }

    public class BTradeHistory
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BTradeHistoryResult> result { get; set; }
    }


    public class BTradeLastResult
    {
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Last { get; set; }
    }

    public class BTradeLast
    {
        public bool success { get; set; }
        public string message { get; set; }
        public BTradeLastResult result { get; set; }
    }

    public class BOrderLimitResult
    {
        public string uuid { get; set; }
    }

    public class BOrderLimit
    {
        public bool success { get; set; }
        public string message { get; set; }
        public BOrderLimitResult result { get; set; }
    }

    public class BMyOrdersHistoryResult
    {
        public string OrderUuid { get; set; }
        public string Exchange { get; set; }
        public DateTime TimeStamp { get; set; }
        public string OrderType { get; set; }
        public double Limit { get; set; }
        public double Quantity { get; set; }
        public double QuantityRemaining { get; set; }
        public double Commission { get; set; }
        public double Price { get; set; }
        public double? PricePerUnit { get; set; }
        public bool IsConditional { get; set; }
        public object Condition { get; set; }
        public object ConditionTarget { get; set; }
        public bool ImmediateOrCancel { get; set; }
    }

    public class BMyOrdersHistory
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BMyOrdersHistoryResult> result { get; set; }
    }


    public class BTicksPriceHistoryResult
    {
        public double O { get; set; }
        public double H { get; set; }
        public double L { get; set; }
        public double C { get; set; }
        public double V { get; set; }
        public DateTime T { get; set; }
        public double BV { get; set; }
    }

    public class BTicksPriceHistory
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<BTicksPriceHistoryResult> result { get; set; }
    }

public class BMarket24HourSummaryResult
{
    public string MarketName { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Volume { get; set; }
    public double Last { get; set; }
    public double BaseVolume { get; set; }
    public DateTime TimeStamp { get; set; }
    public double Bid { get; set; }
    public double Ask { get; set; }
    public int OpenBuyOrders { get; set; }
    public int OpenSellOrders { get; set; }
    public double PrevDay { get; set; }
    public DateTime Created { get; set; }
    public object DisplayMarketName { get; set; }
}

public class BMarket24HourSummary
{
    public bool success { get; set; }
    public string message { get; set; }
    public List<BMarket24HourSummaryResult> result { get; set; }
}








public class Bittrex24HourSummary
{
    public string ticker;
    public double high;
    public double low;
    public double volume;
    public double last;
    public double baseVolume;
    public DateTime timeStamp; 
    public double bid ;
    public double ask ;
    public int openBuyOrders; 
    public int openSellOrders;
    public double prevDay; 
    public DateTime created; 
}
    
 // https://bittrex.com/Home/Api

    public class Bittrex : Market
    {
        static readonly object _locker = new object();
        protected static int currentNonce;

        public Dictionary<string, Bittrex24HourSummary> marketsSummary;

        public Bittrex()
        {
            PUBLIC_API = "https://bittrex.com/api/v1.1/";
            KEY_API = "https://bittrex.com/api/v1.1/";
            signBaseUrl = "https://bittrex.com/api/v1.1/";
            publicMethod = "POST";
            keyMethod = "POST";
            includeParametersInRequestAddress = true;

            int datenonce = Helper.ToUnixTimeStamp(DateTime.UtcNow);
            lock (_locker)
            {
                currentNonce = datenonce;
            }

            key = "";
            secret = "";
//            Global.settingsMain.bittrexkey = "";
            if (Global.settingsMain.bittrexkey != "")
            { 
                key = AppCrypt.DecryptData(Global.settingsMain.bittrexkey);
                haveKey = true;
            }
            if (Global.settingsMain.bittrexsecret != "")
                secret = AppCrypt.DecryptData(Global.settingsMain.bittrexsecret);
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
        }
        public override string MarketName()
        {
            return this.GetType().Name;
        }

        public override WebRequest AddKeyHeaders(Cryptor cryptor, WebRequest webRequest)
        {
            webRequest.Headers.Add("apisign", cryptor.sign);
            return webRequest;
        }
        public override string ToOriginalTicker(string ticker)
        {
            return ticker.Replace("_", "-");
        }
        public override string ConvertPriceIntervalParam(string interval)
        {
            switch (interval)
            {
                case "Day":
                    return "Day";
                case "Hour":
                    return "hour";
                case "Min30":
                    return "thirtyMin";
                case "Min5":
                    return "fiveMin";
                case "Min1":
                    return "oneMin";

            }

            return "";
        }
        public override string  GetBalancesBegin()
        {
            // account/getbalances?apikey=API_KEY    
            string parameters = "account/getbalances?apikey=" + key + "&nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, Balance> GetBalancesEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            BBalances jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BBalances>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            Dictionary<string, Balance> balances;
            balances = new Dictionary<string, Balance>();
            int n = 0;
            foreach (var item in jdata.result)
            {
                n++;
                balances.Add(item.Currency, new Balance { currency = item.Currency, balance = item.Balance });
            }

            return balances;
        }

        public override string GetOrderBookBegin(string ticker)
        {
            //https://bittrex.com/api/v2.0//pub/Market/GetMarketOrderBook?market=BTC-LTC&type=both
            //https://bittrex.com/api/v1.1/public/getorderbook?market=BTC-LTC&type=both    
            ticker = ToOriginalTicker(ticker);

            string parameters = "public/getorderbook?market=" + ticker + "&nonce=" + GetNonce();
            parameters += "&type=" + "both";
            return parameters;

        }
        public override AllOrders GetOrderBookEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            BOrderBook jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BOrderBook>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            AllOrders orders = new AllOrders();
            orders.buyOrders = new List<BuyOrder>();
            orders.sellOrders = new List<SellOrder>();

            int n = 0;
            foreach (var item in jdata.result.buy)
            {
                n++;
                orders.buyOrders.Add(new BuyOrder { quantity = item.Quantity, rate = item.Rate });
            }
            n = 0;
            foreach (var item in jdata.result.sell)
            {
                n++;
                orders.sellOrders.Add(new SellOrder { quantity = item.Quantity, rate = item.Rate });
            }

            orders.sellOrders = orders.sellOrders.OrderBy(o => o.rate).ToList();
            orders.buyOrders = orders.buyOrders.OrderByDescending(o => o.rate).ToList();

            return orders;
        }
        public override string GetOpenOrdersBegin(string ticker)
        {
            //https://bittrex.com/api/v1.1/market/getopenorders?apikey=API_KEY&market=BTC-LTC    
            ticker = ToOriginalTicker(ticker);
            string parameters = "market/getopenorders?apikey=" + key + "&nonce=" + GetNonce();
            parameters += "&market=" + ticker;
            return parameters;

        }
        public override List<OpenOrder> GetOpenOrdersEnd(string parameters, string ticker)
        {
            string response = DoKeyRequest(parameters);

            BOpenOrders jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BOpenOrders>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            List<OpenOrder>  openOrders = new List<OpenOrder>();
            int n = 0;
            foreach (var item in jdata.result)
            {
                n++;
                OpenOrder order = new OpenOrder
                {
                    uuid = item.OrderUuid,
                    openUuid = item.OrderUuid,
                    orderType = item.OrderType,
                    ticker = item.Exchange,
                    quantity = item.Quantity,
                    quantityRemaining = item.QuantityRemaining,
                    price = item.Limit,
                    openedDate = item.Opened
                };
                if (order.orderType == "LIMIT_SELL")
                    order.orderType = "SELL LIMIT";
                if (order.orderType == "LIMIT_BUY")
                    order.orderType = "BUY LIMIT";

                openOrders.Add(order);

            }
            openOrders = openOrders.OrderByDescending(o => o.openedDate).ToList();
            return openOrders;
        }
        public override string GetTradePairsBegin()
        {
            //https://bittrex.com/api/v2.0/pub/Markets/GetMarkets
            //https://bittrex.com/api/v1.1/public/getmarkets       
            string parameters = "public/getmarkets?nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, TradePair> GetTradePairsEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            BMarkets jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BMarkets>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            Dictionary<string, TradePair>  tradePairs = new Dictionary<string, TradePair>();
            int n = 0;
            foreach (var item in jdata.result)
            {
                n++;
                TradePair pair = new TradePair
                {
                    currency1 = item.BaseCurrency,
                    currency2 = item.MarketCurrency,
                    ticker = item.MarketName,
                    isActive = item.IsActive
                };
                pair.ticker = Helper.ToStandartTicker(pair.ticker);
                tradePairs.Add(pair.ticker, pair);
            }
            return tradePairs;
        }


        public override string GetTradeHistoryBegin(string ticker)
        {
            //https://bittrex.com/api/v1.1/public/getmarkethistory?market=BTC-DOGE 
            ticker = ToOriginalTicker(ticker);
            string parameters = "public/getmarkethistory?market=" + ticker + "&nonce=" + GetNonce();
            return parameters;

        }
        public override List<Trade> GetTradeHistoryEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            BTradeHistory jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BTradeHistory>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            List<Trade>  tradeHistory = new List<Trade>();
            int n = 0;
            foreach (var item in jdata.result)
            {
                n++;
                Trade trade = new Trade
                {
                    id = item.Id,
                    tradeDate = item.TimeStamp,
                    quantity = item.Quantity,
                    price = item.Price,
                    total = item.Total,
                    orderType = item.OrderType,
                    fillType = item.FillType
                };

                tradeHistory.Add(trade);
            }
            tradeHistory = tradeHistory.OrderByDescending(o => o.tradeDate).ToList();
            return tradeHistory;
        }

        public override string GetTradeLastBegin(string ticker)
        {
            //https://bittrex.com/api/v1.1/public/getticker?market=BTC-LTC 
            ticker = ToOriginalTicker(ticker);
            string parameters = "public/getticker?market=" + ticker + "&nonce=" + GetNonce();
            return parameters;
        }
        public override TradeLast GetTradeLastEnd(string parameters, string ticker)
        {
            string response = DoPublicRequest(parameters);

            BTradeLast jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BTradeLast>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            TradeLast tradelast = new TradeLast { ask = jdata.result.Ask, bid = jdata.result.Bid, last = jdata.result.Last };

            return tradelast;
        }
        public override string OrderCancelBegin(string uuidOrder)
        {
            //https://bittrex.com/api/v1.1/market/cancel?apikey=API_KEY&uuid=ORDER_UUID     
            string parameters = "market/cancel?apikey=" + key + "&nonce=" + GetNonce();
            parameters += "&uuid=" + uuidOrder;
            return parameters;
        }
        public override string OrderCancelEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            BOrderLimit jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BOrderLimit>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            return "";
        }
        public override string OrderBuyLimitBegin(string ticker, double rate, double quantity)
        {
            //https://bittrex.com/api/v1.1/market/buylimit?apikey=API_KEY&market=BTC-LTC&quantity=1.2&rate=1.3   
            ticker = ToOriginalTicker(ticker);
            string parameters = "market/buylimit?apikey=" + key + "&nonce=" + GetNonce();
            parameters += "&market=" + ticker;
            parameters += "&quantity=" + quantity.ToString();
            parameters += "&rate=" + rate.ToString();

            return parameters;
        }
        public override string OrderBuyLimitEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            BOrderLimit jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BOrderLimit>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            return "";
        }
        public override string OrderSellLimitBegin(string ticker, double rate, double quantity)
        {
            //https://bittrex.com/api/v1.1/market/selllimit?apikey=API_KEY&market=BTC-LTC&quantity=1.2&rate=1.3   
            ticker = ToOriginalTicker(ticker);
            string parameters = "market/selllimit?apikey=" + key + "&nonce=" + GetNonce();
            parameters += "&market=" + ticker;
            parameters += "&quantity=" + quantity.ToString();
            parameters += "&rate=" + rate.ToString();
            return parameters;

        }
        public override string OrderSellLimitEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            BOrderLimit jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BOrderLimit>(response);

            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);
            return "";
        }
        public override string GetMyOrdersHistoryBegin(string ticker)
        {
            //https://bittrex.com/api/v1.1/account/getorderhistory?market=BTC-LTC    
            ticker = ToOriginalTicker(ticker);
            string parameters = "account/getorderhistory?apikey=" + key + "&nonce=" + GetNonce();
            parameters += "&market=" + ticker;
            return parameters;

        }
        public override List<OrderDone> GetMyOrdersHistoryEnd(string parameters, string ticker)
        {
            string response = DoKeyRequest(parameters);

            BMyOrdersHistory jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BMyOrdersHistory>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            List<OrderDone> myOrdersHistory = new List<OrderDone>();
            int n = 0;
            foreach (var item in jdata.result)
            {
                n++;
                OrderDone order = new OrderDone
                {
                    uuid = item.OrderUuid,
                    ticker = item.Exchange,
                    doneDate = item.TimeStamp,
                    orderType = item.OrderType,
                    price = item.Limit,
                    quantity = item.Quantity,
                    quantityRemaining = item.QuantityRemaining,
                    commission = item.Commission,
                    totalSum = item.Price
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
            //https://bittrex.com/api/v2.0/pub/market/GetTicks?marketName=BTC-LTC&tickInterval=Day
            //https://bittrex.com/api/v2.0/pub/market/GetTicks?marketName=BTC-LTC&tickInterval=fiveMin
            ticker = ToOriginalTicker(ticker);
            interval = ConvertPriceIntervalParam(interval);

            ticker = ToOriginalTicker(ticker);
            string parameters = "pub/market/GetTicks?nonce=" + GetNonce();
            parameters += "&marketName=" + ticker;
            parameters += "&tickInterval=" + interval;
            return parameters;
        }

        public override Dictionary<int, PriceCandle> GetPriceHistoryByPeriodEnd(string parameters)
        {
            PUBLIC_API = "https://bittrex.com/api/v2.0/";
            string response = DoPublicRequest(parameters);
            PUBLIC_API = "https://bittrex.com/api/v1.1/";

            BTicksPriceHistory jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BTicksPriceHistory>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            int n = 0;
            Dictionary<int, PriceCandle>  priceHistory = new Dictionary<int, PriceCandle>();
            foreach (BTicksPriceHistoryResult item in jdata.result)
            {
                n++;
                int idate = Helper.ToUnixTimeStamp(item.T);
                priceHistory.Add(idate, new PriceCandle { date = idate, open = item.O, high = item.H, low = item.L, close = item.C, volume = item.V });
            }

            return priceHistory;
        }

        public override string GetMarketCurrentBegin()
        {
            //https://bittrex.com/api/v1.1/public/getmarketsummaries         
            string parameters = "public/getmarketsummaries?nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, MarketCurrent> GetMarketCurrentEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            BMarket24HourSummary jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BMarket24HourSummary>(response);
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            Dictionary<string, MarketCurrent> markets = new Dictionary<string, MarketCurrent>();
            foreach (var item in jdata.result)
            {
                Pair iteminfo = new Pair(Helper.ToStandartTicker(item.MarketName));
                MarketCurrent mkt = new MarketCurrent();
                mkt.ticker = Helper.ToStandartTicker(item.MarketName);
                mkt.lastPrice = item.Last;
                double prevPrice = item.PrevDay;
                mkt.volumeBtc = 0;
                mkt.volumeUSDT = 0;
                if (iteminfo.currency1 == "BTC")
                    mkt.volumeBtc = item.BaseVolume;
                else if (iteminfo.currency1 == "USDT")
                    mkt.volumeUSDT = item.BaseVolume;
 //               mkt.volumeBtc = item.Volume; 
                if (item.PrevDay != 0)
                    mkt.percentChange = ((item.Last- item.PrevDay) / item.PrevDay) * 100.0;
                else
                    mkt.percentChange = 0;
                markets.Add(mkt.ticker, mkt);
            }

            return markets;
        }
        /*
        public void Get24HourSummary()
        {
            //https://bittrex.com/api/v1.1/public/getmarketsummaries         
            string parameters = "public/getmarketsummaries?nonce=" + GetNonce();
            string response = DoPublicRequest(parameters);

            BMarket24HourSummary jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<BMarket24HourSummary>(response);
            //lastRequestMsg = jdata.message;
            //lastRequestStatus = jdata.success;
            if (!jdata.success)
                throw new MarketAPIException("Market API Error:" + jdata.message);

            marketsSummary = new Dictionary<string, Bittrex24HourSummary>();
            int n = 0;
            foreach (var item in jdata.result)
            {
                n++;
                Bittrex24HourSummary marketinfo = new Bittrex24HourSummary
                {
                    ticker = item.MarketName,
                    high = item.High,
                    low = item.Low,
                    volume = item.Volume,
                    last = item.Last,
                    baseVolume = item.BaseVolume,
                    timeStamp = item.TimeStamp,
                    bid = item.Bid,
                    ask = item.Ask,
                    openBuyOrders = item.OpenBuyOrders,
                    openSellOrders = item.OpenSellOrders,
                    prevDay = item.PrevDay,
                    created = item.Created
                };
                marketinfo.ticker = marketinfo.ticker.Replace("-", "_");
                marketsSummary.Add(marketinfo.ticker, marketinfo);
            }

        }
*/


    }


}
