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
    public class TestsForAdmin
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
        public void AdminMenu()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            List<IWebElement> menuItemsToClick = driver.FindElement(By.CssSelector("div#box-apps-menu-wrapper")).FindElements(By.CssSelector(".name")).ToList();
            int menuItemsCount = menuItemsToClick.Count;

            for (int i = 0; i <= menuItemsCount - 1; i++)
            {
                driver.FindElement(By.CssSelector(".logotype")).Click();
                menuItemsToClick = driver.FindElement(By.CssSelector("div#box-apps-menu-wrapper")).FindElements(By.CssSelector(".name")).ToList();
                menuItemsToClick[i].Click();
                List<IWebElement> menuChildItemsToClick;
                int menuChildItemsCount = 0;
                By MySelector = By.CssSelector("ul.docs");
                IWebElement item = null;
                if (MySelector != null)
                {
                    try
                    {
                        item = driver.FindElement(MySelector);
                    }
                    catch { item = null; }
                    if (item != null)
                    {
                        menuChildItemsToClick = item.FindElements(By.CssSelector(".name")).ToList();
                        menuChildItemsCount = menuChildItemsToClick.Count;
                    }
                    else { continue; }
                }
                else { continue; };
                wait.Until(ExpectedConditions.ElementExists(By.TagName("h1")));
                
                if (menuChildItemsCount > 0)
                {
                    int a;
                    for (a = 1; a <= menuChildItemsCount - 1; a++)
                    {
                        menuChildItemsToClick = driver.FindElement(By.CssSelector("ul.docs")).FindElements(By.CssSelector(".name")).ToList();
                        menuChildItemsToClick[a].Click();
                        wait.Until(ExpectedConditions.ElementExists(By.TagName("h1")));
                    }

                }
                
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
