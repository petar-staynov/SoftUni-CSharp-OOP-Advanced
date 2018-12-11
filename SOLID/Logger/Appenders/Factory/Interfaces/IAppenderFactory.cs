using LoggerApp.Appenders.Interfaces;
using LoggerApp.Layouts.Interfaces;

namespace LoggerApp.Appenders.Factory.Interfaces
{
    public interface IAppenderFactory
    {
        IAppender CreateAppender(string type, ILayout layout);

    }
}