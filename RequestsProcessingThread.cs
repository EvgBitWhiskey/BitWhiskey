using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BitWhiskey
{
    public class RequestResult
    {
        public string error = "";
        public Exception exception = null;
        public object resultData = "";
    }
    public class RequestParams
    {
        public string ticker;
    }
        public class RequestItem
    {
        public string requestString ="";
        public RequestParams reqparam = new RequestParams();
        public RequestResult result = new RequestResult();
        public Action<RequestItem> ProcessResultAction;
        //        public int waitnext;
    }
    public class RequestItemGroup
    {
        public string market;
        public List<RequestItem> items;
        public Action<RequestItemGroup> ProcessResultUIAction;
        // priority

        public RequestItemGroup(Action<RequestItemGroup> ProcessResultUIAction_)
        {
            items = new List<RequestItem>();
            ProcessResultUIAction = ProcessResultUIAction_;
        }
        public void AddItem(string requestString, Action<RequestItem> ProcessResultAction, RequestParams reqparam=null)
        {
            RequestItem reqitem = new RequestItem();
            reqitem.reqparam = reqparam;
            reqitem.requestString = requestString;
            reqitem.ProcessResultAction = ProcessResultAction;
            items.Add(reqitem);
        }

    }
    public class RequestManager
    {
        static readonly object _locker = new object();

        private Dictionary<string, BlockingCollection<RequestItemGroup>> requestQueue = new Dictionary<string, BlockingCollection<RequestItemGroup>>();

        public void Create(List<string> marketList)
        {
            foreach(string market in marketList)
              requestQueue.Add(market, new BlockingCollection<RequestItemGroup>());
        }

        public BlockingCollection<RequestItemGroup> GetQueue(string marketName)
        {
            BlockingCollection<RequestItemGroup> queue;
            lock (_locker)
            {
                queue = requestQueue[marketName];
            }
            return queue;
        }
        public void AddItemGroup(string marketName, RequestItemGroup itemgroup)
        {
            if (itemgroup.items.Count > 5)
                throw new Exception("Can't process request, Limit=5 requests in sec");

            BlockingCollection<RequestItemGroup> queue;
            lock (_locker)
            {
                queue = requestQueue[marketName];
            }
            queue.Add(itemgroup);
        }

        public static bool IsResultHasErrors(RequestItemGroup requestGroup, bool log = true, bool display = true)
        {
            foreach (RequestItem item in requestGroup.items)
            {
                if (item.result.error != "")
                {
                    string msg = "Error UIErr->" + item.result.error;
                    if (log)
                        Logman.logger.Error(msg);
                    if (display)
                        Helper.Display(msg);
                    return true;
                }
            }

            return false;
        }

    }

    public class RequestConsumer
    {
        public static RequestManager requestManager = new RequestManager();
        public static Dictionary<string, Task> consumeTasks = new Dictionary<string, Task>();
        public static void CreateRequestThreads(List<string> marketList)
        {
            foreach (string market in marketList)
            {
                Task consumeTask = Task.Run(() => ProcessAPIRequest(market));
                consumeTasks.Add(market, consumeTask);
            }
        }
        public static void ProcessAPIRequest(string marketName)
        {
            Stopwatch stopTimer = new Stopwatch();
            List<long> elapsedTime = new List<long>();
            stopTimer.Start();
            elapsedTime.Add(stopTimer.ElapsedMilliseconds);
            while (true)
            {
                // = new RequestItemGroup();
                BlockingCollection<RequestItemGroup> requestQueue = requestManager.GetQueue(marketName);
                RequestItemGroup itemgroup = requestQueue.Take();
                int curItemCount = itemgroup.items.Count;
                int maxPerSecond = 5;
                if (elapsedTime.Count > maxPerSecond - curItemCount)
                {
                    long startTime = elapsedTime[elapsedTime.Count - (maxPerSecond - curItemCount)];
                    while (stopTimer.ElapsedMilliseconds - startTime < 1100)
                    {
                        int totalReqDelay = (int)(stopTimer.ElapsedMilliseconds - startTime);
                        //itemgroup.items[0].item = itemgroup.items[0].item + "..Wait.." + (1170 - totalReqDelay).ToString() + "ms";
                        Thread.Sleep(1170 - totalReqDelay);
                    }
                }
                itemgroup.market = marketName;
                foreach (RequestItem reqitem in itemgroup.items)
                {
                    elapsedTime.Add(stopTimer.ElapsedMilliseconds);
                    if (elapsedTime.Count > 19)
                        elapsedTime.RemoveAt(0);
                }
                var taskList = new List<Task>();
                foreach (RequestItem reqitem in itemgroup.items)
                {
                    var task = Task.Run(() => reqitem.ProcessResultAction(reqitem));
                    /*                    var task = Task.Run(() =>
                    {try  {  reqitem.ProcessResultAction(reqitem);}catch (Exception e)
                        {  //                              reqitem.result.error         }     }              );
                        */
                    taskList.Add(task);
                }
                try
                {
                    Task.WaitAll(taskList.ToArray());
                }
                catch (Exception e)
                {
                    string msg = "Unhandled Thread ProcessResultAction error -> " + e.Message;
                    Logman.logger.Error(e, msg);
                }

                try
                {
                   Task.Factory.StartNew(() =>
                   {
                     try
                     {
                         itemgroup.ProcessResultUIAction(itemgroup);
                     }
                     catch (ObjectDisposedException ex)
                     {
                     }
                     catch (Exception e)
                     {
                           string msg = "UI Not Handled error -> " + e.Message;
                           Logman.logger.Error(e,msg);
                     }
                   }, CancellationToken.None, TaskCreationOptions.None, Global.uiScheduler).Wait();
                }
                catch (Exception e)
                {
                    string msg = "UI Not Handled error -> " + e.Message;
                    Logman.logger.Error(e, msg);
                }

            }

        }

    }

}
