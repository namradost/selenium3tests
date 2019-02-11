using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    public class TestBase
    {
        public Application app;

        [SetUp]
        public void start()
        {
            app = new Application();
        }

        [TearDown]
        public void stop()
        {
            app.Quit();
            app = null;
        }
    }

}

