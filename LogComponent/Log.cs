using Microsoft.VisualBasic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace LogComponent
{
    public class Log : ILog
    {
        private const string PATH = @"C:\logs";
        private string pFile;

        private List<LogLine> lines = new List<LogLine>();
        private DateTime fileNameDate = DateTime.Now;
        public Log()
        {       

            if (!Directory.Exists(PATH))
            {
                try
                {
                    Directory.CreateDirectory(PATH);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The directory " + PATH + " could not be created due " + e.Message + "error. Fix the problem or create the folder manually");
                }
            }

            string filename = fileNameDate.ToString("yyyyMMdd-HHmmss-fff");
            this.pFile = PATH + @"\" + filename + ".log";

            string logHeader = "Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine;

            try
            {
                using (StreamWriter writer = File.AppendText(pFile))
                {
                    writer.Write(logHeader);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("The log file " + pFile + " could not be created due " + e.Message + "error. Please, correct the access rights");
            }
            catch (Exception e)
            {
                Console.WriteLine("The log file " + pFile + " could not be created due " + e.Message + "error");
            }

        }



        public void StopWithoutFlush()
        {
        }

        public void StopWithFlush()
        {
        }

        public void Write(string text)
        {
            LogLine prettyLine = new LogLine(text);
            using (StreamWriter writer = File.AppendText(pFile))
            {
                writer.Write(prettyLine.CreateLineText());
            }
        }


     /*   private void MainLoop()
        {
            while (!this.exit)
            {
                if (this.lines.Count > 0)
                {
                    int f = 0;
                    List<LogLine> handled = new List<LogLine>();

                    foreach (LogLine logLine in lines)
                    {
                        f++;

                        if (f > 5)
                            continue;

                        if (!this.exit || this.QuitWithFlush)
                        {
                            handled.Add(logLine);


                            if ((DateTime.Now - fileNameDate).Days != 0)
                            {
                                fileNameDate = DateTime.Now;

                                using (StreamWriter writer = File.AppendText(pFile))
                                {
                                    writer.Write(logLine.Text + Thread.CurrentThread.ManagedThreadId);
                                }
                            }
                        }
                    }

                    for (int y = 0; y < handled.Count; y++)
                    {
                        this.lines.Remove(handled[y]);
                    }

                    if (this.QuitWithFlush == true && this.lines.Count == 0)
                        this.exit = true;

                    Thread.Sleep(50);
                }
            }

        }*/
    }

}
