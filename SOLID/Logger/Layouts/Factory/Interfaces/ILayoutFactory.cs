using LoggerApp.Layouts.Interfaces;

namespace LoggerApp.Layouts.Factory.Interfaces
{
    public interface ILayoutFactory
    {
        ILayout CreateLayout(string layoutType);
    }
}