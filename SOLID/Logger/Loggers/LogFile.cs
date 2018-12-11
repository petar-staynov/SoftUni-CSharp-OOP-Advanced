using System.Linq;
using LoggerApp.Loggers.Interfaces;

namespace LoggerApp.Loggers
{
    public class LogFile : ILogFile
    {
        public void Write(string message)
        {
            Size = message.Where(char.IsLetter).Sum(x => x);
        }

        public int Size { get; private set; }
    }
}