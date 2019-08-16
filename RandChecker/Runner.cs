using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandChecker
{
    public delegate void FinishWrite(Runner runner);

    public class Runner
    {
        public Thread RunThread;
        public FileStream Fs;
        public StreamWriter FWriter;
        public long cStart = 0;
        public long cStop = 0;
        public string filename = "";
        public long cCurrent = 0;
        public event FinishWrite Finish;

        public void Close()
        {
            FWriter?.Close();
            Fs?.Close();
        }

        public void Finished()
        {
            if (Finish != null) Finish(this);
        }

        public void Start()
        {
            RunThread?.Start();
        }
    }
}