using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class Windows
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void AddNewCountry()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            driver.Url = ("http://localhost/litecart/admin/?app=countries&doc=countries");
            driver.FindElement(By.CssSelector("#content div a")).Click();

            string mainWindow = driver.CurrentWindowHandle;
            ICollection<string> oldWindows = driver.WindowHandles;

            List<IWebElement> linksWin = driver.FindElements(By.CssSelector("td#content table a[target='_blank']")).ToList();
            for (int i = 0; i < linksWin.Count(); i++)
            {
                IWebElement link = linksWin[i];
                string linkUrl = link.GetAttribute("href");

                //current count of windows
                int previousWinCount = driver.WindowHandles.Count;
                //open new window
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string title = (string)js.ExecuteScript("window.open(\"" + linkUrl + "\");");

                //wait until new window appears
                wait.Until(driver => driver.WindowHandles.Count == (previousWinCount + 1));

                foreach (string winHandle in driver.WindowHandles)
                {
                    driver.SwitchTo().Window(winHandle);
                }

                string windowNew = driver.CurrentWindowHandle;
              
                driver.Close();
                driver.SwitchTo().Window(mainWindow);

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
