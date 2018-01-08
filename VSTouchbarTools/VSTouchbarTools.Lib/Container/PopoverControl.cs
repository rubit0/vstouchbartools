using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using VSTouchbarTools.Lib.Elemens;

namespace VSTouchbarTools.Lib.Container
{
    public class PopoverControl : ITouchbarElement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public List<ITouchbarElement> ChildElements { get; set; } = new List<ITouchbarElement>();
        public List<ITouchbarElement> PressAndHoldChildElements { get; set; } = new List<ITouchbarElement>();

        private const string RootElementName = "Popover";
        private const string ChildElementName = "PopoverTouchBar";
        private const string PressAndHoldChildElementName = "PressAndHoldTouchBar";

        public PopoverControl(string id, string title = null, string image = null)
        {
            Id = id;
            Title = title;
            Image = image;

            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(image))
                throw new InvalidOperationException("You must either specify a title or image for this Element.");
        }

        public XmlNode ToNode(XmlDocument doc)
        {
            var node = doc.CreateElement(RootElementName);

            var idAtr = doc.CreateAttribute("id");
            idAtr.Value = Id;
            node.Attributes.Append(idAtr);

            if (!string.IsNullOrEmpty(Title))
            {
                var atr = doc.CreateAttribute("title");
                atr.Value = Title;
                node.Attributes.Append(atr);
            }

            if (!string.IsNullOrEmpty(Image))
            {
                var atr = doc.CreateAttribute("image");
                atr.Value = Image.Contains("Template") ? Image : $"base64:{Image}";

                atr.Value = $"base64:{Image}";
                node.Attributes.Append(atr);
            }

            if (ChildElements.Any())
                node.AppendChild(CreateIdentifiedChildNode(doc, ChildElements, ChildElementName, "PopoverChild"));

            if (PressAndHoldChildElements.Any())
                node.AppendChild(CreateIdentifiedChildNode(doc, PressAndHoldChildElements, PressAndHoldChildElementName, "PressAndHoldChild"));

            return node;
        }

        private XmlNode CreateIdentifiedChildNode(XmlDocument doc, List<ITouchbarElement> elements, string elementName, string idSuffix)
        {
            var node = doc.CreateElement(elementName);

            //Add attributes
            var idAtr = doc.CreateAttribute("id");
            idAtr.Value = Id + idSuffix;
            node.Attributes.Append(idAtr);

            var elementsAtr = doc.CreateAttribute("defaultItemIdentifiers");
            elementsAtr.Value = GetElemenIds(elements);
            node.Attributes.Append(elementsAtr);

            //Add child nodes
            foreach (var element in elements)
            {
                var child = element.ToNode(doc);
                if (child != null)
                    node.AppendChild(child);
            }

            return node;
        }

        private string GetElemenIds(List<ITouchbarElement> elements)
        {
            var builder = new StringBuilder();
            foreach (var element in elements)
                builder.Append(element.Id + ",");

            //Remove ',' from tail
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
    }
}