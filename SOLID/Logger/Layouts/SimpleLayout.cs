using LoggerApp.Layouts.Interfaces;

namespace LoggerApp.Layouts
{
    public class SimpleLayout : ILayout
    {
        public string Format
        {
            get
            {
                return "{0} - {1} - {2}";
            }
        }
    }
}