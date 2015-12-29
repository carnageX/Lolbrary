using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lolbrary
{
    public class MemoryLeak
    {
        private BackgroundWorker _worker;
        

        public long AllocatedMemory { get; set; }
        public bool WorkerIsRunning { get { return _worker.IsBusy; } }

        public MemoryLeak()
        {
            LeakProcess();
        }

        private void LeakProcess()
        {
            using (_worker = new BackgroundWorker())
            {
                _worker.WorkerSupportsCancellation = true;
                _worker.WorkerReportsProgress = true;

                _worker.DoWork += _worker_DoWork;
                _worker.ProgressChanged += _worker_ProgressChanged;
                _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

                while(true && !_worker.IsBusy)
                {
                    _worker.RunWorkerAsync();
                }
            }
        }

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AllocatedMemory = GC.GetTotalMemory(false);
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LeakProcess();
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var xlist = new List<int>(1000);
            while (true)
            {
                Random x = new Random(DateTime.Now.Second);
                xlist.Add(x.Next());
                GC.KeepAlive(xlist);
                _worker.ReportProgress(0);
            }
        }

        public void CancelLeak()
        {
            if (_worker.IsBusy) { _worker.CancelAsync(); }
        }

        public long GetCurrentMemoryUse()
        {
            return AllocatedMemory;
        }
    }
}
