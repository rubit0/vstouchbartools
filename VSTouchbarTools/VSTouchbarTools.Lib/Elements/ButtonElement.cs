using System;
using System.Xml;

namespace VSTouchbarTools.Lib.Elements
{
    public class ButtonElement : ITouchbarElement
    {
        public string Id { get; set; }
        public string KeyCode { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool ScaleImage2X { get; set; } = true;
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public int Width { get; set; }

        protected virtual string ElementName => "Button";

        public ButtonElement(string id, string keyCode, string title = null, string imageData = null)
        {
            Id = id;
            KeyCode = keyCode;
            Title = title;
            Image = imageData;

            if(string.IsNullOrEmpty(keyCode))
                throw new InvalidOperationException("You must provide a keycode sequence this Element.");

            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(imageData))
                throw new InvalidOperationException("You must either specify a title or image for this Element.");
        }

        public XmlNode ToNode(XmlDocument doc)
        {
            var node = doc.CreateElement(ElementName);

            //Create atrributes
            var idAtr = doc.CreateAttribute("id");
            idAtr.Value = Id;
            node.Attributes.Append(idAtr);

            var keyAtr = doc.CreateAttribute("keyCode");
            keyAtr.Value = KeyCode;
            node.Attributes.Append(keyAtr);

            if (!string.IsNullOrEmpty(Title))
            {
                var atr = doc.CreateAttribute("title");
                atr.Value = Title;
                node.Attributes.Append(atr);
            }

            if (!string.IsNullOrEmpty(Image))
            {
                var atr = doc.CreateAttribute("image");
                if (Image.Contains("Template"))
                    atr.Value = Image;
                else
                    atr.Value = !ScaleImage2X ? $"base64:{Image}" : $"base64:2x:{Image}";

                node.Attributes.Append(atr);
            }

            if (!string.IsNullOrEmpty(BackgroundColor))
            {
                var atr = doc.CreateAttribute("backColor");
                atr.Value = BackgroundColor;
                node.Attributes.Append(atr);
            }

            if (!string.IsNullOrEmpty(TextColor))
            {
                var atr = doc.CreateAttribute("textColor");
                atr.Value = TextColor;
                node.Attributes.Append(atr);
            }

            if (Width > 0)
            {
                var atr = doc.CreateAttribute("width");
                atr.Value = Width.ToString();
                node.Attributes.Append(atr);
            }

            return node;
        }
    }
}
