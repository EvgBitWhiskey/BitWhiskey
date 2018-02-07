using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;


namespace BitWhiskey
{
    public class YGetInfoRights
    {
        public int info { get; set; }
        public int trade { get; set; }
        public int deposit { get; set; }
        public int withdraw { get; set; }
    }
    /*
    public class YGetInfoFunds
    {
        public Dictionary<string,double> tpair { get; set; }
//        public double eth { get; set; }
    }

    public class YGetInfoFundsInclOrders
    {
        public double eth { get; set; }
    }
    */
    public class YGetInfoResult
    {
        public YGetInfoRights rights { get; set; }
        public Dictionary<string,double> funds { get; set; }
        public Dictionary<string, double> funds_incl_orders { get; set; }
//        public YGetInfoFundsInclOrders funds_incl_orders { get; set; }
        public int transaction_count { get; set; }
        public int open_orders { get; set; }
        public int server_time { get; set; }
    }

    public class YGetInfo
    {
        public int success { get; set; }
        public YGetInfoResult @return { get; set; }
    }


    public class YInfoCurrency
    {
        public int decimal_places { get; set; }
        public double min_price { get; set; }
        public int max_price { get; set; }
        public double min_amount { get; set; }
        public double min_total { get; set; }
        public int hidden { get; set; }
        public double fee { get; set; }
        public double fee_buyer { get; set; }
        public double fee_seller { get; set; }
    }

        public class YInfo
        {
            public int server_time { get; set; }
            public Dictionary<string, YInfoCurrency> pairs { get; set; }
        }

    public class YDepthRates
    {
        public List<List<double>> asks { get; set; }
        public List<List<double>> bids { get; set; }
    }
    /*
    public class YDepth
    {
        public LtcBtc ltc_btc { get; set; }
    }
    */


    public class YTickerCurrency
    {
        public double high { get; set; }
        public double low { get; set; }
        public double avg { get; set; }
        public double vol { get; set; }
        public double vol_cur { get; set; }
        public double last { get; set; }
        public double buy { get; set; }
        public double sell { get; set; }
        public int updated { get; set; }
    }


    public class YTrade
    {
        public string type { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
        public int tid { get; set; }
        public int timestamp { get; set; }
    }


    public class YActiveOrder
    {
        public string pair { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
        public double rate { get; set; }
        public int timestamp_created { get; set; }
        public int status { get; set; }
    }


    public class YActiveOrders
    {
        public int success { get; set; }
        public Dictionary<string,YActiveOrder> @return { get; set; }
    }


    public class YMyTrade
    {
        public string pair { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
        public double rate { get; set; }
        public string order_id { get; set; }
        public int is_your_order { get; set; }
        public int timestamp { get; set; }
    }
    public class YMyTrades
    {
        public int success { get; set; }
        public Dictionary<string, YMyTrade> @return { get; set; }
    }


    //https://www.yobit.net/ru/api/

    public class Yobit : Market
    {
        static readonly object _locker = new object();
        protected static int currentNonce;

        public Yobit()
        {
            PUBLIC_API = " https://yobit.io/api/3/";
            KEY_API = "https://yobit.io/tapi/";
            signBaseUrl = "";
            publicMethod = "POST";
            keyMethod = "POST";
            includeParametersInRequestAddress = false;

            options.AllPairRatesSupported = false;
            options.ChartDataSupported = false;

            int datenonce = Helper.ToUnixTimeStamp(DateTime.UtcNow);
            lock (_locker)
            {
                currentNonce = datenonce;
            }

            enabled = !Global.settingsMain.yobitdisabled;
            if (Global.settingsMain.yobitkey != "")
            {
                key = AppCrypt.DecryptData(Global.settingsMain.yobitkey);
                haveKey = true;
            }
            if (Global.settingsMain.yobitsecret != "")
            { 
                secret = AppCrypt.DecryptData(Global.settingsMain.yobitsecret);
            }
        }
        
        private void CheckResponseAndThrow(string response)
        {
            //            if (response.Length > 13)
            if (response.Contains("\"success\":"))
            {
                if (response.Contains("\"success\":1"))
                    return;

                dynamic jdata = JsonConvert.DeserializeObject<ExpandoObject>(response, new ExpandoObjectConverter());
                if (jdata.success == 0)
                {
                    throw new MarketAPIException("Market API Error:" + jdata.error);
                }
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
            Pair pairinfo = new Pair(ticker);
            string t = pairinfo.currency2 + "_" + pairinfo.currency1;
            t = t.ToLower().Replace("-", "_");
            return t;
        }
        public override string ToUITicker(string ticker)
        {
            string t=ticker.ToUpper();
            Pair pairinfo = new Pair(t);
            t = pairinfo.currency2 + "_" + pairinfo.currency1;
            return t;
                //.Replace("-", "_");
        }
        public override string ConvertPriceIntervalParam(string interval)
        {
            return "";
        }

        public override string GetBalancesBegin()
        {
            string parameters = "method=getInfo" + "&nonce=" + GetNonce();
            return parameters;
        }
        public override Dictionary<string, Balance> GetBalancesEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);
            
            CheckResponseAndThrow(response);

            YGetInfo jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<YGetInfo>(response);
            Dictionary<string, Balance> balances;
            balances = new Dictionary<string, Balance>();

            foreach (var item in jdata.@return.funds)
            {
                if(item.Value!=0)
                  balances.Add(item.Key.ToUpper(), new Balance { currency = item.Key.ToUpper(), balance = item.Value });
            }
            return balances;

        }
        public override string GetMarketCurrentBegin()
        {
            //https://yobit.net/api/3/info
            string parameters = "info";
            return parameters;
        }
        public override Dictionary<string, MarketCurrent>  GetMarketCurrentEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);
            CheckResponseAndThrow(response);
            YInfo jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<YInfo>(response);

            Dictionary<string, MarketCurrent> markets = new Dictionary<string, MarketCurrent>();
            
            foreach (var item in jdata.pairs)
            {
                string ticker= ToUITicker(item.Key);
                Pair pairinfo = new Pair(ticker);
                if (ticker != "RUR_BTC" && ticker != "USD_BTC" && pairinfo.currency1 != "BTC" && pairinfo.currency1 != "USD")
                    continue;

                MarketCurrent mkt = new MarketCurrent();
                mkt.ticker = ticker;
                mkt.lastPrice =0;
                mkt.ask =0;
                mkt.bid =0;
                mkt.volumeBtc = 0;
                mkt.volumeUSDT = 0;
                mkt.percentChange = 0;
                markets.Add(ticker, mkt);
            }
            return markets;
        }

        public string GetTickerCurrentBegin(string ticker)
        {
            //https://yobit.net/api/3/ticker/ltc_btc
            ticker = ToOriginalTicker(ticker);

            string parameters = @"ticker/" + ticker;
            return parameters;

        }
        public Dictionary<string, MarketCurrent> GetTickerCurrentEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);
            Dictionary<string, YTickerCurrency> items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, YTickerCurrency>>(response);
            
            Dictionary<string, MarketCurrent> markets = new Dictionary<string, MarketCurrent>();

            foreach (var item in items)
            {
                Pair iteminfo = new Pair(ToUITicker(item.Key));
                MarketCurrent mkt = new MarketCurrent();
                mkt.ticker = ToUITicker(item.Key);
                mkt.lastPrice =item.Value.last;
                mkt.ask = item.Value.sell;
                mkt.bid = item.Value.buy;
                mkt.volumeBtc = 0;
                mkt.volumeUSDT = 0;
                if (iteminfo.currency2=="BTC")
                  mkt.volumeBtc = item.Value.vol;
                else if (iteminfo.currency2 == "USD")
                    mkt.volumeUSDT = item.Value.vol;
                mkt.percentChange = 0;
                markets.Add(ToUITicker(item.Key), mkt);
            }
            return markets;
        }

        public override string GetOrderBookBegin(string ticker)
        {
            //https://yobit.net/api/3/depth/ltc_btc
            ticker = ToOriginalTicker(ticker);

            string parameters = @"depth/"+ ticker;
            return parameters;
        }
        public override AllOrders GetOrderBookEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            Dictionary<string,YDepthRates> items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, YDepthRates>>(response);
            AllOrders orders = new AllOrders();
            orders.buyOrders = new List<BuyOrder>();
            orders.sellOrders = new List<SellOrder>();
            int n = 0;
            foreach (var item in items.Values.ToList()[0].bids)
            {
                n++;
                orders.buyOrders.Add(new BuyOrder { quantity = item[1], rate = item[0] });
            }
            n = 0;
            foreach (var item in items.Values.ToList()[0].asks)
            {
                n++;
                orders.sellOrders.Add(new SellOrder { quantity =item[1], rate = item[0] });
            }
            orders.sellOrders = orders.sellOrders.OrderBy(o => o.rate).ToList();
            orders.buyOrders = orders.buyOrders.OrderByDescending(o => o.rate).ToList();

            return orders;

        }
        public override string GetTradePairsBegin()
        {
            //https://yobit.net/api/3/info
            string parameters = "info";
            return parameters;
        }
        public override Dictionary<string, TradePair> GetTradePairsEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);

            YInfo jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<YInfo>(response);

            Dictionary<string, TradePair> tradePairs = new Dictionary<string, TradePair>();

            foreach (var item in jdata.pairs)
            {
                TradePair pair = new TradePair
                {
                    currency1 = "",
                    currency2 = "",
                    ticker = ToUITicker(item.Key),
                    isActive = true
                };
                //                if (item.Value["disabled"].ToString() == "1")
                //                    pair.isActive = false;
                Pair pairinfo = new Pair(pair.ticker);
                if (pair.ticker != "RUR_BTC" && pair.ticker != "USD_BTC" && pairinfo.currency1 != "BTC" && pairinfo.currency1 != "USD")
                    continue;
                pair.currency1 = pairinfo.currency1;
                pair.currency2 = pairinfo.currency2;
                tradePairs.Add(pair.ticker, pair);

            }
            /*
                        dynamic jdata = JsonConvert.DeserializeObject<ExpandoObject>(response, new ExpandoObjectConverter());

                        Dictionary<string, TradePair> tradePairs = new Dictionary<string, TradePair>();
                        //            object aa = jdata.success;
                        //            object r = jdata["return"];
                        //            var returnList = jdata.@return;
                        //            var fundsList = returnList["funds"];
                        var pairsList = (IDictionary<string, object>)jdata.pairs;

                        foreach (var item in pairsList.Keys)
                        {
                            TradePair pair = new TradePair
                            {
                                currency1 = "",
                                currency2 = "",
                                ticker = item,
                                isActive = true
                            };
                            //                if (item.Value["disabled"].ToString() == "1")
                            //                    pair.isActive = false;
                            Pair pairinfo = new Pair(pair.ticker.ToUpper());
                            if (pair.ticker!="BTC_RUR" && pair.ticker != "BTC_USD" && pairinfo.currency2 != "BTC" && pairinfo.currency2 != "USD")
                                continue;
                            pair.currency1 = pairinfo.currency1;
                            pair.currency2 = pairinfo.currency2;
                            tradePairs.Add(pair.ticker, pair);
                        }
                        */

            return tradePairs;

        }
        public override string GetTradeHistoryBegin(string ticker)
        {
            //https://yobit.net/api/3/trades/ltc_btc
            ticker = ToOriginalTicker(ticker);

            string parameters = @"trades/" + ticker;
            return parameters;
        }
        public override List<Trade> GetTradeHistoryEnd(string parameters)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);
            Dictionary<string,List<YTrade>> items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<YTrade>>>(response);

            int n = 0;
            List<Trade> tradeHistory = new List<Trade>();
            foreach (var item in items.Values.ToList()[0])
            {
                n++;
                Trade trade = new Trade
                {
                    id = item.tid,
                    tradeDate = Helper.UnixToDateTime(item.timestamp),
                    quantity =item.amount,
                    price =item.price,
                    total = item.amount* item.price,
                    orderType = item.type.ToUpper(),
                    fillType = ""
                };
                if (trade.orderType == "ASK")
                    trade.orderType = "SELL";
                else
                    trade.orderType = "BUY";

                tradeHistory.Add(trade);
            }
            tradeHistory = tradeHistory.OrderByDescending(o => o.tradeDate).ToList();

            return tradeHistory;
        }

        public override string GetTradeLastBegin(string ticker)
        {
            //https://yobit.net/api/3/ticker/ltc_btc
            ticker = ToOriginalTicker(ticker);

            string parameters = @"ticker/" + ticker;
            return parameters;
        }
        public override TradeLast GetTradeLastEnd(string parameters, string ticker)
        {
            string response = DoPublicRequest(parameters);

            CheckResponseAndThrow(response);
            Dictionary<string, YTickerCurrency> items = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, YTickerCurrency>>(response);

            YTickerCurrency item= items.Values.ToList()[0];
            TradeLast tradelast;
            tradelast = new TradeLast { ask = item.sell, bid =item.buy, last = item.last };

            return tradelast;
        }
        public override string GetOpenOrdersBegin(string ticker)
        {
            ticker = ToOriginalTicker(ticker);
            string parameters = "method=ActiveOrders&pair=" + ticker + "&nonce=" + GetNonce();
            return parameters;

        }
        public override List<OpenOrder> GetOpenOrdersEnd(string parameters, string ticker)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            YActiveOrders jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<YActiveOrders>(response);

            List<OpenOrder> openOrders = new List<OpenOrder>();

            //            foreach (var item in jdata.@return.funds)
            if (jdata.@return != null)
            {
                foreach (var pair in jdata.@return)
                {
                    YActiveOrder item = pair.Value;
                    OpenOrder order = new OpenOrder
                    {
                        uuid = pair.Key,
                        openUuid = pair.Key,
                        orderType = item.type,
                        ticker = ToUITicker(item.pair),
                        quantity = item.amount,
                        quantityRemaining = 0,
                        price = item.rate,
                        openedDate = Helper.UnixToDateTime(item.timestamp_created)
                    };
                    if (order.orderType == "sell")
                        order.orderType = "SELL LIMIT";
                    if (order.orderType == "buy")
                        order.orderType = "BUY LIMIT";

                    openOrders.Add(order);

                }
            }
            openOrders = openOrders.OrderByDescending(o => o.openedDate).ToList();
            return openOrders;

/*

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
            */
        }
        public override string OrderCancelBegin(string idOrder)
        {
            // cancelOrder   
            string parameters = "method=CancelOrder" + "&nonce=" + GetNonce();
            parameters += "&order_id=" + idOrder;
            return parameters;
        }
        public override string OrderCancelEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            dynamic jdata = JsonConvert.DeserializeObject<ExpandoObject>(response, new ExpandoObjectConverter());
//            if(jdata.success==1)
            //            PCancelOrder jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PCancelOrder>(response);
            return "";
        }
        public override string OrderBuyLimitBegin(string ticker, double rate, double quantity)
        {
            // buy   
            ticker = ToOriginalTicker(ticker);
            string parameters = "method=Trade&type=buy&pair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&amount=" + quantity.ToString();
            parameters += "&rate=" + rate.ToString();
            return parameters;
        }
        public override string OrderBuyLimitEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            //PBuySell jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PBuySell>(response);
            return "";
        }
        public override string OrderSellLimitBegin(string ticker, double rate, double quantity)
        {
            // sell   
            ticker = ToOriginalTicker(ticker);
            string parameters = "method=Trade&type=sell&pair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&amount=" + quantity.ToString();
            parameters += "&rate=" + rate.ToString();
            return parameters;
        }
        public override string OrderSellLimitEnd(string parameters)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            //PBuySell jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<PBuySell>(response);
            return "";
        }
        public override string GetMyOrdersHistoryBegin(string ticker)
        {
            ticker = ToOriginalTicker(ticker);
            string parameters = "method=TradeHistory&pair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&count=100";
            return parameters;
        }
        public override List<OrderDone> GetMyOrdersHistoryEnd(string parameters, string ticker)
        {
            string response = DoKeyRequest(parameters);

            CheckResponseAndThrow(response);

            YMyTrades jdata = Newtonsoft.Json.JsonConvert.DeserializeObject<YMyTrades>(response);
            List<OrderDone> myOrdersHistory = new List<OrderDone>();

            if (jdata.@return != null)
            {
                foreach (var pair in jdata.@return)
                {
                    YMyTrade item = pair.Value;

                    OrderDone order = new OrderDone
                    {
                        uuid = item.order_id.ToString(),
                        ticker = ToUITicker(item.pair),
                        doneDate = Helper.UnixToDateTime(item.timestamp),
                        orderType = item.type,
                        price = item.rate,
                        quantity = item.amount,
                        quantityRemaining = 0,
                        commission = 0,
                        totalSum = item.amount* item.rate
                    };
                    if (order.orderType == "sell")
                        order.orderType = "S";
                    if (order.orderType == "buy")
                        order.orderType = "B";

                    myOrdersHistory.Add(order);

                }
            }
            myOrdersHistory = myOrdersHistory.OrderByDescending(o => o.doneDate).ToList();
            return myOrdersHistory;

        }
        public override string GetPriceHistoryByPeriodBegin(string ticker, string interval, DateTime start, DateTime end)
        {
            /*
            //https://poloniex.com/public?command=returnChartData&currencyPair=USDT_BTC&start=1494304000&end=9999999999&period=900
            ticker = ToOriginalTicker(ticker);
            interval = ConvertPriceIntervalParam(interval);

            string parameters = "returnChartData&currencyPair=" + ticker + "&nonce=" + GetNonce();
            parameters += "&start=" + Helper.ToUnixTimeStamp(start);
            parameters += "&end=" + Helper.ToUnixTimeStamp(end);
            parameters += "&period=" + interval;
            return parameters;
*/
            return "";

        }
        public override Dictionary<int, PriceCandle> GetPriceHistoryByPeriodEnd(string parameters)
        {
            Dictionary<int, PriceCandle> priceHistory = new Dictionary<int, PriceCandle>();
            return priceHistory;

            /*
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
*/

        }


    }



}
