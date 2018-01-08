using System.Collections.Generic;
using System.Xml;
using VSTouchbarTools.Lib.Elemens;

namespace VSTouchbarTools.Lib.Container
{
    public class SegmentedControl : ITouchbarElement
    {
        public string Id { get; set; }
        public List<SegmentElement> ChildElements { get; set; } = new List<SegmentElement>();
        public bool Seperated { get; set; }

        private const string TrackingMode = "momentary";
        private const string ElementName = "SegmentedControl";

        public SegmentedControl(string id)
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

            var sepAtr = doc.CreateAttribute("separated");
            sepAtr.Value = Seperated.ToString().ToLower();
            node.Attributes.Append(sepAtr);

            var trackAtr = doc.CreateAttribute("trackingMode");
            trackAtr.Value = TrackingMode;
            node.Attributes.Append(trackAtr);

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
