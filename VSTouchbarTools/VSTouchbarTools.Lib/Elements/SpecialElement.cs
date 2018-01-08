using System.Xml;

namespace VSTouchbarTools.Lib.Elements
{
    public class SpecialElement : ITouchbarElement
    {
        private const string SmallSpace = "NSTouchBarItemIdentifierFixedSpaceSmall";
        private const string LargeSpace = "NSTouchBarItemIdentifierFixedSpaceLarge";
        private const string FlexibleSpace = "NSTouchBarItemIdentifierFlexibleSpace";
        private const string EmojiPicker = "NSTouchBarItemIdentifierCharacterPicker";

        public enum Space
        {
            Flexible,
            Small,
            Large,
            Emoji
        }

        public string Id { get; set; }

        public SpecialElement(Space space)
        {
            switch (space)
            {
                case Space.Flexible:
                    Id = FlexibleSpace;
                    break;
                case Space.Small:
                    Id = SmallSpace;
                    break;
                case Space.Large:
                    Id = LargeSpace;
                    break;
                case Space.Emoji:
                    Id = EmojiPicker;
                    break;
            }
        }

        public XmlNode ToNode(XmlDocument doc)
        {
            return null;
        }
    }
}