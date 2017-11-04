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
    class Yobit
    {
        string key="";
        string secret="";
        string PUBLIC_API = "https://yobit.net/api/3/";
        string KEY_API = "https://yobit.net/tapi/";

        public Yobit()
        {
        }
        public string GetResponseForRequest(WebRequest request)
        {
            using (Stream s = request.GetResponse().GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    var jsonResponse = sr.ReadToEnd();
                    return jsonResponse;
                    //                        Console.WriteLine(String.Format("Response: {0}", jsonResponse));
                }
            }
            
        }
        public WebRequest CreateKeyRequest(Cryptor cryptor,string input)
        {
            string address = KEY_API + "?" + input;

            WebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(address);
            webRequest.Method = "POST";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Headers.Add("Key", key);
            webRequest.Headers.Add("Sign", cryptor.sign);

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            webRequest.ContentLength = input.Length;
            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(inputBytes, 0, input.Length);
            }

            return webRequest;

        }
        public WebRequest CreatePublicRequest(string input)
        {
            string address = PUBLIC_API + "?" + input;

            WebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(address);
            webRequest.Method = "POST";
            webRequest.Timeout = 20000;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Headers.Clear();
            
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            webRequest.ContentLength = input.Length;
            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(inputBytes, 0, input.Length);
            }

            return webRequest;

        }
        public string DoKeyRequest(string parameters)
        {
            Cryptor cryptor = new Cryptor();
            cryptor.CalcSign(secret, parameters);
            WebRequest webRequest = CreateKeyRequest(cryptor, parameters);
            return GetResponseForRequest(webRequest);
        }
        public string DoPublicRequest(string parameters)
        {
            WebRequest webRequest = CreatePublicRequest(parameters);
            return GetResponseForRequest(webRequest);
        }
        public int GetNonce()
        {
            return Helper.ToUnixTimeStamp(DateTime.UtcNow);
            //(int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public void GetInfo()
        { 
            //https://yobit.net/api/3/info;
            //https://yobit.net/api/3/ticker/ltc_btc-nmc_btc
            string parameters ="method=getInfo&nonce=" + GetNonce();
            string response = DoPublicRequest(parameters);
        }
        public void Trade(string otype,string pair,float rate,float amount)
        { 
            string parameters = "method=Trade&nonce=" + GetNonce();
            parameters += "&pair=" + pair;
            parameters += "&type=" + otype;
            parameters += "&rate=" + rate;
            parameters += "&amount=" + amount;
            string response = DoKeyRequest(parameters);
        }
    
    }


}
