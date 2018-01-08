namespace VSTouchbarTools.Lib.Elements
{
    public class SegmentElement : ButtonElement
    {
        protected override string ElementName => "Segment";

        public SegmentElement(string id, string keyCode, string title = null, string imageData = null) 
            : base(id, keyCode, title, imageData)
        {
        }
    }
}