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
    public class Application
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public MainPage mainPage;
        public CartPage cartPage;
        public ItemPage itemPage;

        public Application()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            mainPage = new MainPage(driver);
            cartPage = new CartPage(driver);
            itemPage = new ItemPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

    }
}
