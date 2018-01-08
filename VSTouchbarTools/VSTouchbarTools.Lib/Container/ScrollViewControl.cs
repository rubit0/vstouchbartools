using System.Collections.Generic;
using System.Xml;
using VSTouchbarTools.Lib.Elemens;

namespace VSTouchbarTools.Lib.Container
{
    public class ScrollViewControl : ITouchbarElement
    {
        public string Id { get; set; }
        public List<ITouchbarElement> ChildElements { get; set; } = new List<ITouchbarElement>();

        private const string ElementName = "ScrollView";

        public ScrollViewControl(string id)
        {
            Id = id;
        }

        public XmlNode ToNode(XmlDocument doc)
        {
            var node = doc.CreateElement(ElementName);

            //Create atrributes
            var idAtr = doc.CreateAttribute("id");
            idAtr.Value = Id;
            node.Attributes.Append(idAtr);

            //Add child nodes
            foreach (var element in ChildElements)
            {
                var child = element.ToNode(doc);
                if (child != null)
                    node.AppendChild(child);
            }

            return node;
        }
    }
}