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
            var button1 = new ButtonElement("1", "1,2,3", "Button1", "ABCDEF");
            var space = new SpecialElement(SpecialElement.Space.Small);
            var button2 = new ButtonElement("2", "ctrl+f", "Button2") { Width = 80 };
            var segRoot = new SegmentedControl("4") {Seperated = true};
            var seg1 = new SegmentElement("A", "abc", "Hello World");
            segRoot.ChildElements.Add(seg1);

            var scroll = new ScrollViewControl("X");
            scroll.ChildElements.Add(button1);
            scroll.ChildElements.Add(button2);

            var pop = new PopoverControl("Y", "Things");
            pop.ChildElements.Add(button2);
            pop.PressAndHoldChildElements.Add(button1);
            var button3 = new ButtonElement("button3", "alt+f1", "Super");
            pop.PressAndHoldChildElements.Add(button3);

            var root = new RootTouchbar();
            root.Elements.Add(button1);
            root.Elements.Add(space);
            root.Elements.Add(button2);
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
