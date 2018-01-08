using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSTouchbarTools.Lib;
using VSTouchbarTools.Lib.Container;
using VSTouchbarTools.Lib.Elemens;

namespace VSTouchbarTools.Tests
{
    [TestClass]
    public class LibTests
    {
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
            scroll.ChildElements.Add(new ButtonElement("s1", "1,2,3", "Scroll BT1", CustomIcons.GoToIcon));
            scroll.ChildElements.Add(new ButtonElement("s2", "ctrl+f", "Scroll BT2") { Width = 80 });
            scroll.ChildElements.Add(new ButtonElement("s3", "ctrl+f", "Scroll BT3") { Width = 80 });
            scroll.ChildElements.Add(new ButtonElement("s4", "ctrl+f", "Scroll BT4") { Width = 80 });

            var pop = new PopoverControl("Y", "Pop Items", CustomIcons.SurroundIcon);
            pop.ChildElements.Add(new ButtonElement("p2", "ctrl+f", "Pop BT1") { Width = 80 });
            pop.PressAndHoldChildElements.Add(new ButtonElement("p1", "1,2,3", "PopHold BT1", CustomIcons.FormatDocIcon));
            var button3 = new ButtonElement("p3", "alt+f1", "PopHold BT2");
            pop.PressAndHoldChildElements.Add(button3);

            var root = new RootTouchbar();
            root.Elements.Add(new ButtonElement("r1", "1,2,3", "Button1", SystemStandardIcons.Play));
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
