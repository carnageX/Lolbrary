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

        public StorageFiller()
        {
            var envPath = @"%SystemDrive%\Temp";
            var expandedPath = Environment.ExpandEnvironmentVariables(envPath);
            FillerInit(expandedPath);
        }

        public StorageFiller(string directory)
        {
            FillerInit(directory);
        }

        private void FillerInit(string directory)
        {
            _filepath = directory;
            using (_worker = new BackgroundWorker())
            {
                _worker.WorkerSupportsCancellation = true;
                _worker.DoWork += _worker_DoWork;
                _worker.RunWorkerAsync();
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WriteFile(_filepath);
        }

        private void WriteFile(string directory)
        {
            var filename = String.Format(@"{0}\lolbrary{1}.txt", directory, DateTime.Now);
            File.Create(filename); 

            while(true)
            {
                //SHA-512 transform of "lolbrary was here"
                var s = String.Format("{0}{1}", Guid.NewGuid(), " 5d4784687f4da651b65b1ce3fefb04e12ebcecf85a505cfe0091a37bd2b1149e62028fbe6e3808ef8112d5c6da51854e45fae3a1b5556809aa326d62430e4aaf  ");
                File.AppendAllText(filename, s);
            }
        }

        public void CancelFiller()
        {
            if(_worker.IsBusy) { _worker.CancelAsync(); }
        }
    }
}
