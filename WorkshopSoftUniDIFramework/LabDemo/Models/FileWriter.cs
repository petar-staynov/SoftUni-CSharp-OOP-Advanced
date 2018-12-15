using System.IO;
using LabDemo.Models.Contracts;

namespace LabDemo.Models
{
    public class FileWriter : IWriter
    {
        private const string FilePath = "log.txt";

        public void Write(string content)
        {
            File.AppendAllText(FilePath, content);
        }
    }
}