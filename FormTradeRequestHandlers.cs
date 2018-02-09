using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitWhiskey
{
    public class TradeRequestHandlers
    {
        protected Market market;

        public TradeRequestHandlers(Market market_)
        {
            market = market_;
        }
        public void HandleError(RequestItem request, Exception ex)
        {
            request.result.error = "Request:" + request.requestString;
            request.result.exception = ex;
            if (ex == null)
            {
                request.result.errorLevel = ResultErrorLevel.ERROR;
                Logman.Log(ex, request.result.error);
                return;
            }
            if (ex.Message == null)
            {
                request.result.errorLevel = ResultErrorLevel.ERROR;
                Logman.Log(ex, request.result.error);
                return;
            }

            if 
            (
               (
                 ex.Message.Contains("422")
                 || ex.Message.Contains("503")
                 || ex.Message.Contains("502")
                 || ex.Message.Contains("500")
               )
               || (ex.Message.Contains("Ddos") && ex.Message.Contains("20-50 mins"))
               ||  ex.Message.Contains("DataResult=Null")
            )
            {
                request.result.errorLevel = ResultErrorLevel.IGNORE;
                Logman.Log(request.result.error + " -> " + ex.Message);
            }
            else
            {
                request.result.errorLevel = ResultErrorLevel.ERROR;
                Logman.Log(ex, request.result.error);
            }
        }

        public void GetBalances_RequestHandler(RequestItem request)
        {
            try { request.result.resultData = market.GetBalancesEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); }
        }
        public void OrderBuyLimit_RequestHandler(RequestItem request)
        {
            try { request.result.resultData = market.OrderBuyLimitEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); }
        }
        public void GetTradeLast_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetTradeLastEnd(request.requestString, request.reqparam.ticker); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetOrderBook_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetOrderBookEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetTradeHistory_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetTradeHistoryEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetOpenOrders_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetOpenOrdersEnd(request.requestString, request.reqparam.ticker); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetTradePairs_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetTradePairsEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }
        public void OrderCancel_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.OrderCancelEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }
        public void OrderSellLimit_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.OrderSellLimitEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetMyOrdersHistory_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetMyOrdersHistoryEnd(request.requestString, request.reqparam.ticker); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetPriceHistoryByPeriod_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetPriceHistoryByPeriodEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }
        public void GetMarketCurrent_RequestHandler(RequestItem request)
        { try { request.result.resultData = market.GetMarketCurrentEnd(request.requestString); } catch (Exception ex) { HandleError(request, ex); } }

    }
}
