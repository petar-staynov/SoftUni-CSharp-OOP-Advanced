using System.Text;
using LoggerApp.Layouts.Interfaces;

namespace LoggerApp.Layouts
{
    public class XmlLayout : ILayout
    {
        public string Format
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<log>")
                    .AppendLine("   <date>{0}</date>")
                    .AppendLine("   <level>{1}</level>")
                    .AppendLine("   <message>{2}</message>")
                    .AppendLine("</log>");

                return sb.ToString().TrimEnd();

            }
        }
    }
}