using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BitWhiskey
{
    class AppTimer
    {
        Timer timer;
        public AppTimer(double interval=1000,ElapsedEventHandler elapsed=null,System.ComponentModel.ISynchronizeInvoke threadSyncObject=null, bool start=false)
        {
            timer = new Timer();
            timer.SynchronizingObject = threadSyncObject;
            timer.Interval = interval;
            timer.Elapsed += elapsed;
            if(start)
              Start();
        }
        public  void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }


    }
}
