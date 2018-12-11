namespace LoggerApp.Core.Interfaces
{
    public interface ICommandInterpreter
    {
        void AddAppender(string[] args);
        void AddMessages(string[] args);
        void PrintInfo();
    }
}