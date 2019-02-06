using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class Logs
    {
        private EventFiringWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            ChromeOptions co = new ChromeOptions();
            co.SetLoggingPreference(LogType.Browser, LogLevel.All);
            driver = new EventFiringWebDriver(new ChromeDriver(co));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));   
            
        }

        [Test]
        public void ProductLogs()
        {            
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            driver.FindElement(By.CssSelector("td:nth-child(3) > a")).Click();

            products(By.CssSelector("td:nth-child(3) > a"));
        }

        private void products(By locator)
        {
            List<IWebElement> listProd = driver.FindElements(locator).ToList();
            int countProd = listProd.Count();
            string first = driver.FindElement(By.CssSelector("table tr:nth-child(5)")).GetAttribute("rowIndex");
            int productItem = Convert.ToInt32(first) + 1;

            IWebElement item;
            for (int i = productItem; i < countProd + productItem; i++)
            {      
                item = driver.FindElement(By.CssSelector($"tr:nth-child({i}) td:nth-child(3) > a"));
                item.Click();
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
                driver.FindElement(By.Name("cancel")).Click();               
            }
            Console.WriteLine("LOGS");
            logs();
        }

        private void logs()
        {

            foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
            {
                Console.WriteLine(l);
            }

        }


        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;       
        }

    }
}
