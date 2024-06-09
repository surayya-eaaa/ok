using System.Text;

namespace LogComponent
{
    public class LogLine
    {
   
        public LogLine()
        {
            this.Text = "";
        }
        public virtual string LineText()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Text.Length > 0)
            {
                sb.Append(this.Text);
                sb.Append(". ");
            }

            sb.Append(this.CreateLineText());

            return sb.ToString();
        }

        public virtual string CreateLineText()
        {
            return "";
        }


        public string Text { get; set; }

        public virtual DateTime Timestamp { get; set; }
  
    }
}