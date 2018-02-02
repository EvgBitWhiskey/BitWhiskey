using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitWhiskey
{
    public class Balance
    {
        public string currency { get; set; }
        public double balance { get; set; }

        public Balance()
        {
            balance = 0;
        }
    }

    public class ExchangeBalance
    {
        public string exchangeName { get; set; }
        public string currency { get; set; }
        public double balance { get; set; }
        public double btcBalance { get; set; }
        public double usdBalance { get; set; }
        public double price { get; set; }
        public bool balanceFound { get; set; }
    }

    public class BuyOrder
    {
        public double quantity { get; set; }
        public double rate { get; set; }
    }
    public class SellOrder
    {
        public double quantity { get; set; }
        public double rate { get; set; }
    }

    public class AllOrders
    {
        public List<BuyOrder> buyOrders;
        public List<SellOrder> sellOrders;
    }

    public class OpenOrder
    {
        public string uuid { get; set; }
        public string openUuid { get; set; }
        public string ticker { get; set; }
        public string orderType { get; set; }
        public double quantity { get; set; }
        public double quantityRemaining { get; set; }
        public double price { get; set; }
        public DateTime openedDate { get; set; }
        //        public double Limit { get; set; }
    }
    
    public class TradePair
    {
        public string ticker { get; set; }
        public string currency1 { get; set; }
        public string currency2 { get; set; }
        public bool isActive { get; set; }

        public TradePair Copy()
        {
            return new TradePair
            {
               currency1 = currency1,
                currency2 =currency2 ,
                isActive = isActive,
                ticker =ticker 
            };
        }
    }

    public class Trade
    {
        public int id { get; set; }
        public DateTime tradeDate { get; set; }
        public double quantity { get; set; }
        public double price { get; set; }
        public double total { get; set; }
        public string fillType { get; set; }
        public string orderType { get; set; }
    }

    public class TradeLast
    {
        public double bid { get; set; }
        public double ask { get; set; }
        public double last { get; set; }
    }

    public class MarketCurrent
    {
        public string ticker { get; set; }
        public double lastPrice { get; set; }
        public double ask { get; set; }
        public double bid { get; set; }
        public double percentChange { get; set; }
        public double volumeBtc { get; set; }
        public double volumeUSDT { get; set; }
    }

    public class OrderDone
    {
        public string uuid { get; set; }
        public string ticker { get; set; }
        public DateTime doneDate { get; set; }
        public string orderType { get; set; }
        public double price { get; set; }
        public double quantity { get; set; }
        public double quantityRemaining { get; set; }
        public double commission { get; set; }
        public double totalSum { get; set; }
    }


    public class Pair
    {
        public string currency1;
        public string currency2;

        public Pair(string pair)
        {
            var valueSplit = pair.Split('_');
            currency1 = valueSplit[0];
            currency2 = valueSplit[1];
        }
    }
    public class MyTradeState
    {
        public string errMsg;
        public bool   completedOk;
    }

    public class PriceCandle
    {
        public int date;
        public double high;
        public double low;
        public double open;
        public double close;
        public double volume;
        public PriceCandle Copy()
        {
            return new PriceCandle {date=date, open = open, high = high, low = low, close = close, volume = volume };
        }

    }

    public class TickerGridRow
    {
        public string f1 { get; set; }
        public string f2 { get; set; }
        public string f3 { get; set; }
        public string f4 { get; set; }
        public string f5 { get; set; }
        public string f6 { get; set; }
        public string f7 { get; set; }
        public string f8 { get; set; }
    }



}
