using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSTouchbarTools.Lib.Container;
using VSTouchbarTools.Lib.Elemens;

namespace VSTouchbarTools.Tests
{
    [TestClass]
    public class LibTests
    {
        private const string Base64Image =
                "iVBORw0KGgoAAAANSUhEUgAAACwAAAAsCAYAAAAehFoBAAABG2lUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIi8+CiA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJyIj8+Gkqr6gAAAWlpQ0NQRGlzcGxheSBQMwAAGJVlkL9LAmEYx7/XDypTIZKWGg6CaLAwJaqhwR8Y/YLLHKrtPPUUzuvl7iSdWxuCFseof6CWQMR0bKgpaIqc2poSWlLe3tcTNHzg4fnw5ft+ed4HEL40JWeOAMjplhHbColHxyfiWAMOuOHCODZkxST7h9E4syAoSXsYqJ83CHy+LvGsz4q7pFajL7uPEb06my8N+v+VI5kyFTZbrH0KMSxA8DKeP7MIZ4mxx2BLMU5yVm0ucE7YfNHxxGNhxteMRSUjM69QZ+xN9OlqH+e0vNLdgW/v7LKH9RwiyMIEgQYZRYiQELBSBYsbwqekaGTVjCUGCdFS4rauLHtFv29lFeC3s2OaB51UYea5p503gM0apbTS03ZqwP0aO0O5py2uA1OTwFOZyIbckYZZD6XTwPcd4GI3mX4AJlpmOuC3f+AMAaMflDYXgLEroH1J6e8Npe1b9vgdqOt/wBNmipl5YbQAAAAJcEhZcwAACxMAAAsTAQCanBgAAAIbSURBVFiF7Zi9ahVBGIbfSQz+Q0whSsSAFxCF4B/+RBRsvQFvwNLYWViJjYVgkSIBwUa0iAl6AcHC1nCMIggpDIFAGsVCEHPOY3HmyDrZ3XznMHsmyj4wxS777fuwzLezs1JNTc3/C3AVcKk9zAAfgLfARGoXE14YoAXMAodTO5WSEe7wDbgNDKV2yyVHuMMn4LrlHtEbwDfVEUmnJI1LOprJuSlppKT8laQp59xKbK8tAOeAZ8BGwVO08hN4AByoQtIBN3znx6QJ3I0tuxt4GlkUYBEYL8rtaQ4DI5JeSposuGRDUsOPz5I2JSHpvqTRgppVSXckzTnn6MWrSPYg+d2+CTwHzpTU5tX9AO4Be6NJBqGPc0LfAScMtaHwC+B4JaI+8CztVSrLa4wdnRFuAFcqE/Vhgz4oS4MuVingDXAL2FWlayfsWiDbAk53eY89VfnlhU0HwjN9C+8WYABYD4QvpvYqBLgQyK4DAylcrKHng+MF51wrtowFq/BYcLwUW8RKr8JfYotY+eeETQBfg6bbl8pl2yfsl939mVNNP3Ye/nW2wlaWgZOp/f4CGKW9qy1iDRhO7fkHbLuJh/32KpvDlw31l2KJWMkVBgYlHTPUh6+7yskVds41JX001L+Pq7M9ZVNi3lBvuaY/0N7GhzuMLIupvtgKAYaBJ4FoC3hEotXO9F8COCRpQtIvSUvOue+VWtXU1OwcfgOgK2jvwtgz8gAAAABJRU5ErkJggg=="
            ;
        [TestMethod]
        public void RootTouchbarToXmlNotNull()
        {
            //Arrange
            var button1 = new ButtonElement("1", "1,2,3", "Button1", "ABCDEF");
            var flex = new SpecialElement(SpecialElement.Space.Flexible);
            var button2 = new ButtonElement("2", "ctrl+f", "Button2") {Width = 80};
            var root = new RootTouchbar();
            root.Elements.Add(button1);
            root.Elements.Add(flex);
            root.Elements.Add(button2);

            //Act
            var doc = root.ToXml();

            //Assert
            Assert.IsNotNull(doc);
        }

        [TestMethod]
        public void RootTouchbarToXmlAsStringNotNull()
        {
            //Arrange
            var space = new SpecialElement(SpecialElement.Space.Small);
            var segRoot = new SegmentedControl("4") {Seperated = true};
            var seg1 = new SegmentElement("seg1", "a,b,c", "Segment BT 1");
            segRoot.ChildElements.Add(seg1);

            var scroll = new ScrollViewControl("X");
            scroll.ChildElements.Add(new ButtonElement("s1", "1,2,3", "Scroll BT1", Base64Image));
            scroll.ChildElements.Add(new ButtonElement("s2", "ctrl+f", "Scroll BT2") { Width = 80 });
            scroll.ChildElements.Add(new ButtonElement("s3", "ctrl+f", "Scroll BT3") { Width = 80 });
            scroll.ChildElements.Add(new ButtonElement("s4", "ctrl+f", "Scroll BT4") { Width = 80 });


            var pop = new PopoverControl("Y", "Pop Items");
            pop.ChildElements.Add(new ButtonElement("p2", "ctrl+f", "Pop BT1") { Width = 80 });
            pop.PressAndHoldChildElements.Add(new ButtonElement("p1", "1,2,3", "PopHold BT1", Base64Image));
            var button3 = new ButtonElement("p3", "alt+f1", "PopHold BT2");
            pop.PressAndHoldChildElements.Add(button3);

            var root = new RootTouchbar();
            root.Elements.Add(new ButtonElement("r1", "1,2,3", "Button1", Base64Image));
            root.Elements.Add(space);
            root.Elements.Add(new ButtonElement("r2", "ctrl+f", "Button2") { Width = 80 });
            root.Elements.Add(segRoot);
            root.Elements.Add(scroll);
            root.Elements.Add(pop);

            //Act
            var doc = root.ToXmlAsString();

            //Assert
            Assert.IsNotNull(doc);

            Debug.Write(doc);
        }
    }
}
