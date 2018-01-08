using System.Xml;

namespace VSTouchbarTools.Lib.Elemens
{
    public interface ITouchbarElement
    {
        string Id { get; set; }
        XmlNode ToNode(XmlDocument doc);
    }
}