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
    public class MarketOptions
    {
        public bool AllPairRatesSupported = true;
        public bool ChartDataSupported = true;
    }

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
        protected bool haveKey = false;
        protected bool enabled = true;

        protected MarketOptions options = new MarketOptions();

        public Market()
        {
        }
        public virtual bool IsEnabled()
        {
            return enabled;
        }
        public virtual bool HaveKey()
        {
            return haveKey;
        }
        public virtual MarketOptions Options()
        {
            return options;
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
        public virtual string ToUITicker(string ticker)
        {
            return ticker;
        }

        public abstract string MarketName();
        public abstract string ToOriginalTicker(string ticker);
        public abstract string ConvertPriceIntervalParam(string interval);
        public abstract string GetOrderBookBegin(string ticker);
        public abstract AllOrders GetOrderBookEnd(string parameters);
        public abstract string GetTradeHistoryBegin(string ticker);
        public abstract List<Trade> GetTradeHistoryEnd(string parameters);
        public abstract string GetTradeLastBegin(string ticker);
        public abstract TradeLast GetTradeLastEnd(string parameters,string ticker);
        public abstract string GetBalancesBegin();
        public abstract Dictionary<string, Balance> GetBalancesEnd(string parameters);
        public abstract string GetOpenOrdersBegin(string ticker);
        public abstract List<OpenOrder> GetOpenOrdersEnd(string parameters, string ticker);
        public abstract string GetTradePairsBegin();
        public abstract Dictionary<string, TradePair> GetTradePairsEnd(string parameters);
        public abstract string OrderCancelBegin(string uuidOrder);
        public abstract string OrderCancelEnd(string parameters);
        public abstract string OrderBuyLimitBegin(string ticker,double rate,double quantity);
        public abstract string OrderBuyLimitEnd(string parameters);
        public abstract string OrderSellLimitBegin(string ticker, double rate, double quantity);
        public abstract string OrderSellLimitEnd(string parameters);
        public abstract string GetMyOrdersHistoryBegin(string ticker);
        public abstract List<OrderDone> GetMyOrdersHistoryEnd(string parameters, string ticker);
        public abstract string GetPriceHistoryByPeriodBegin(string ticker,string interval, DateTime start, DateTime end);
        public abstract Dictionary<int, PriceCandle> GetPriceHistoryByPeriodEnd(string parameters);

        public abstract string GetMarketCurrentBegin();
        public abstract Dictionary<string, MarketCurrent> GetMarketCurrentEnd(string parameters);




    }




}
