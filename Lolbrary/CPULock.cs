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
            var path = Environment.ExpandEnvironmentVariables("%SYSTEM%");
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            _rFile = files[rand.Next(files.Length)];
        }

        private void LockProcess()
        {
            using (_worker = new BackgroundWorker())
            {
                _worker.WorkerSupportsCancellation = true;
                _worker.DoWork += _worker_DoWork;

                _worker.RunWorkerAsync();
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SHA512 sha512 = SHA512.Create();
            using (FileStream fs = File.Open(_rFile, FileMode.Open))
            {
                sha512.ComputeHash(fs);
            }
        }

        public void CancelLock()
        {
            if(_worker.IsBusy) { _worker.CancelAsync(); }
        }
    }
}
