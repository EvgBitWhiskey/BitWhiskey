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
    public abstract class Market
    {
        protected string key="";
        protected string secret="";
        protected string PUBLIC_API = "";
        protected string KEY_API = "";
        protected string signBaseUrl = "";
        protected string publicMethod = "";
        protected string keyMethod = "";
        protected bool   includeParametersInRequestAddress ;
        public string lastRequestMsg;
        public bool lastRequestStatus;


        public Dictionary<string, TradePair> tradePairs;
        public Dictionary<string, Balance> balances;
        public List<BuyOrder> buyOrders;
        public List<SellOrder> sellOrders;
        public List<OpenOrder> openOrders;
        public List<Trade> tradeHistory;
        public TradeLast tradelast;
        public List<OrderDone> myOrdersHistory;
        public Dictionary<int, PriceCandle> priceHistory;


        public Market()
        {
        }
        public virtual string GetResponseForRequest(WebRequest request)
        {
            using (Stream s = request.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    return jsonResponse;
                }
            }

        }
        public virtual WebRequest CreatePublicRequest(string method, string input)
        {
            string address = PUBLIC_API + input;

            WebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(address);
            webRequest.Method =method;
            webRequest.Timeout = 80000;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest = AddPublicHeaders(webRequest);

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            webRequest.ContentLength = input.Length;
            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(inputBytes, 0, input.Length);
            }

            return webRequest;

        }
        public virtual WebRequest CreateKeyRequest(Cryptor cryptor, string method, string input)
        {
            string address = KEY_API;
            if (includeParametersInRequestAddress)
                address += input;

            WebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(address);
            webRequest.Method =method;
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest = AddKeyHeaders(cryptor,webRequest);

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            webRequest.ContentLength = input.Length;
            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(inputBytes, 0, input.Length);
            }

            return webRequest;

        }
        public virtual WebRequest AddPublicHeaders(WebRequest webRequest)
        {
            webRequest.Headers.Clear();
            return webRequest;
        }
        public abstract WebRequest AddKeyHeaders(Cryptor cryptor, WebRequest webRequest);

        public virtual string DoKeyRequest(string parameters)
        {
            Cryptor cryptor = new Cryptor();
            cryptor.CalcSign(secret, signBaseUrl+parameters);
            WebRequest webRequest = CreateKeyRequest(cryptor, keyMethod, parameters);
            return GetResponseForRequest(webRequest);
        }
        public virtual string DoPublicRequest(string parameters)
        {
            WebRequest webRequest = CreatePublicRequest(publicMethod, parameters);
            return GetResponseForRequest(webRequest);
        }


        public abstract string ToOriginalTicker(string ticker);
        public abstract string ConvertPriceIntervalParam(string interval);
        public abstract void GetOrderBook(string ticker);
        public abstract void GetTradeHistory(string ticker);
        public abstract void GetTradeLast(string ticker);
        public abstract void GetBalances();
        public abstract void GetOpenOrders(string ticker);
        public abstract void GetTradePairs();
        public abstract bool OrderCancel(string uuidOrder);
        public abstract bool OrderBuyLimit(string ticker,double rate,double quantity);
        public abstract bool OrderSellLimit(string ticker, double rate, double quantity);
        public abstract void GetMyOrdersHistory(string ticker);
        public abstract string GetPriceHistoryByPeriod(string ticker,string interval, DateTime start, DateTime end);
        public abstract string MarketName();


    }




}
