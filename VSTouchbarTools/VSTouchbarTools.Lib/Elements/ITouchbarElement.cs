using System.Xml;

namespace VSTouchbarTools.Lib.Elements
{
    public interface ITouchbarElement
    {
        string Id { get; set; }
        XmlNode ToNode(XmlDocument doc);
    }
}