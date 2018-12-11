using System;
using System.Reflection.Metadata.Ecma335;
using LoggerApp.Layouts.Factory.Interfaces;
using LoggerApp.Layouts.Interfaces;

namespace LoggerApp.Layouts.Factory
{
    public class LayoutFactory : ILayoutFactory
    {
        public ILayout CreateLayout(string layoutType)
        {
            string typeToLowerCase = layoutType.ToLower();

            switch (typeToLowerCase)
            {
                case "simplelayout":
                    return new SimpleLayout();
                case "xmllayout":
                    return new XmlLayout();
                default: throw new ArgumentException("Invalid layout type");
            }
        }
    }
}