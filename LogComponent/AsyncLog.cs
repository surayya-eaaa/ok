using System.Text;

namespace LogComponent
{
    public class AsyncLog : ILog
    {
        private Thread runThread;
        private List<LogLine> lines = new List<LogLine>();
        private StreamWriter writer; 
        private bool exit;
        private bool QuitWithFlush = false;
        private DateTime curDate = DateTime.Now;


        public AsyncLog()
        {
            if (!Directory.Exists(@"C:\LogTest"))
            {
                Directory.CreateDirectory(@"C:\LogTest");
            }
            this.writer = File.AppendText(@"C:\LogTest\Log" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log");
            this.writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
            this.writer.AutoFlush = true;
            this.runThread = new Thread(this.MainLoop);
            this.runThread.Start();
        }

        
        private void MainLoop()
        {
            while (!this.exit)
            {
                if (this.lines.Count > 0)
                {
                    int f = 0;
                    List<LogLine> _handled = new List<LogLine>();

                    foreach (LogLine logLine in this.lines)
                    {
                        f++;

                        if (f > 5)
                            continue;
                        
                        if (!this.exit || this.QuitWithFlush)
                        {
                            _handled.Add(logLine);

                            StringBuilder stringBuilder = new StringBuilder();

                            if ((DateTime.Now - curDate).Days != 0)
                            {
                                doSomething(stringBuilder);
                            }

                            stringBuilder.Append(logLine.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                            stringBuilder.Append("\t");
                            stringBuilder.Append(logLine.LineText());
                            stringBuilder.Append("\t");

                            stringBuilder.Append(Environment.NewLine);

                            this.writer.Write(stringBuilder.ToString());
                        }
                    }

                    for (int y = 0; y < _handled.Count; y++)
                    {
                        this.lines.Remove(_handled[y]);   
                    }

                    if (this.QuitWithFlush == true && this.lines.Count == 0) 
                        this.exit = true;

                    Thread.Sleep(50);
                }
            }

            void doSomething(StringBuilder stringBuilder)
            {
                curDate = DateTime.Now;

                this.writer = File.AppendText(@"C:\LogTest\Log" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log");

                this.writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);

                stringBuilder.Append(Environment.NewLine);

                this.writer.Write(stringBuilder.ToString());

                this.writer.AutoFlush = true;
            }
        }

        public void StopWithoutFlush()
        {
            this.exit = true;
        }

        public void StopWithFlush()
        {
            this.QuitWithFlush = true;
        }

        public void Write(string text)
        {
            this.lines.Add(new LogLine() { Text = text, Timestamp = DateTime.Now });
        }
    }
}