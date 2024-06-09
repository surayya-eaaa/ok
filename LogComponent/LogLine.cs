using System.Text;

namespace LogComponent
{
    public class LogLine
    {
        public string Text { get; set; }

        public DateTime Timestamp { get;}

        public LogLine(string text)
        {
            this.Text = text;
            this.Timestamp = DateTime.Now;
        }
        public string CreateLineText()
        {
            return Timestamp.ToString().PadRight(25, ' ') + "\t" + Text.ToString().PadRight(15, ' ') + "\t" + Environment.NewLine;
        }
    }
}