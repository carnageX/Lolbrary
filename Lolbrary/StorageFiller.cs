using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lolbrary
{
    public class StorageFiller
    {
        private BackgroundWorker _worker;
        private string _filepath;

        public StorageFiller(string saveDirectory = @"C:\Temp")
        {
            FillerInit(saveDirectory);
        }

        private void FillerInit(string saveDirectory)
        {
            var filename = String.Format(@"{0}\lolbrary{1}.txt", saveDirectory, DateTime.Now.ToString("yyyyMMddhhmmss"));
            _filepath = filename;
            using (_worker = new BackgroundWorker())
            {
                _worker.WorkerSupportsCancellation = true;
                _worker.DoWork += _worker_DoWork;
                _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
                while(true && !_worker.IsBusy)
                {
                    _worker.RunWorkerAsync();
                }
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FillerInit(_filepath);
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WriteFile(_filepath);
        }

        private void WriteFile(string filepath)
        {
            while(true)
            {                
                //SHA-512 transform of "lolbrary was here"
                var s = String.Format("{0}{1}", Guid.NewGuid(), " 5d4784687f4da651b65b1ce3fefb04e12ebcecf85a505cfe0091a37bd2b1149e62028fbe6e3808ef8112d5c6da51854e45fae3a1b5556809aa326d62430e4aaf  ");
                File.AppendAllText(filepath, s);
            }
        }

        public void CancelFiller()
        {
            if(_worker.IsBusy) { _worker.CancelAsync(); }
        }
    }
}
