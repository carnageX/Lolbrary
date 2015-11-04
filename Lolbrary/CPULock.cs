using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Lolbrary
{
    public class CPULock
    {
        private BackgroundWorker _worker;
        private string _rFile;

        public CPULock()
        {
            LockInit();
            LockProcess();
        }

        private void LockInit()
        {
            var rand = new Random();
            var files = Directory.GetFiles(@"C:\Windows\System32", "*.*", SearchOption.TopDirectoryOnly);
            _rFile = files[rand.Next(files.Length)];
        }

        private void LockProcess()
        {
            using (_worker = new BackgroundWorker())
            {
                _worker.WorkerSupportsCancellation = true;
                _worker.DoWork += _worker_DoWork;
                _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

                while (true && !_worker.IsBusy)
                {
                    _worker.RunWorkerAsync();
                }
            }
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine("Finish - {0}", _rFile);
            LockInit();
            LockProcess();
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SHA512Managed sha512managed = new SHA512Managed();
                using (FileStream fs = File.Open(_rFile, FileMode.Open))
                {
                    //Console.WriteLine("Start - {0}", _rFile);
                    sha512managed.ComputeHash(fs);
                }
            }
            catch
            {
                LockInit();
            }
        }

        public void CancelLock()
        {
            if(_worker.IsBusy) { _worker.CancelAsync(); }
        }
    }
}
