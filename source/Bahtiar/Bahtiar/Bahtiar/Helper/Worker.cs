using System.ComponentModel;

namespace Bahtiar.Helper
{
    public class Worker : BackgroundWorker
    {
        public Worker(DoWorkEventHandler doWork, 
            RunWorkerCompletedEventHandler runWorkerCompleted = null, 
            ProgressChangedEventHandler progressChanged = null)
        {
            DoWork += doWork;
            RunWorkerCompleted += runWorkerCompleted;
            ProgressChanged += progressChanged;
        }
    }
}
