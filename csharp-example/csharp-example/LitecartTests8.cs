using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class MyCart
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
        public void WorkWithCart()
        {
            driver.Url = "http://localhost/litecart/en/";
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#logotype-wrapper")));
            AddProductInCart();
            DeleteFromCart();

        }
        public void AddProductInCart()
        {

            IWebElement incart = driver.FindElement(By.CssSelector("div#cart span.quantity"));
            string iCount = incart.GetAttribute("textContent");
            int num = int.Parse(iCount);
            By locator = By.CssSelector("#box-product div.content tr:nth-child(1) strong");
            By locator2 = By.CssSelector("#box-product div.content tr:nth-child(1) select");

            for (int i = num; i <= 2; i++) 
            {
                driver.FindElement(By.CssSelector("div.content div.name")).Click();
                if (IsSelect(driver, By.CssSelector("#box-product div.content tr:nth-child(1) select")))
                {
                        SelectSize(By.CssSelector("#box-product div.content tr:nth-child(1) select"));
                }
                driver.FindElement(By.Name("add_cart_product")).Click();
                i++;
                string j = i.ToString();
                IWebElement incart2 = driver.FindElement(By.CssSelector("div#cart span.quantity"));
                string iCount2 = incart2.GetAttribute("textContent");
                wait.Until(ExpectedConditions.TextToBePresentInElement(incart2, $"{j}"));
                i = int.Parse(j);
                i--;
                driver.FindElement(By.CssSelector("div#logotype-wrapper")).Click();
            }
        }
        public void DeleteFromCart()
        {
            driver.FindElement(By.CssSelector("#cart a.content")).Click();      
            List<IWebElement> tableItems = driver.FindElements(By.CssSelector("#order_confirmation-wrapper td.item")).ToList();
            int itemsCount = tableItems.Count;

            for (int i = itemsCount; i > 1; i--)
            {
               IWebElement el2 = driver.FindElement(By.CssSelector("#order_confirmation-wrapper tr:nth-child(3) td.item"));
               string el2text = el2.GetAttribute("textContent");

               driver.FindElement(By.CssSelector("li.shortcut")).Click();
               wait.Until(ExpectedConditions.ElementExists(By.Name("remove_cart_item")));
               driver.FindElement(By.Name("remove_cart_item")).Click();

               IWebElement el1 = driver.FindElement(By.CssSelector("#order_confirmation-wrapper tr:nth-child(2) td.item"));
               string el1text = el1.GetAttribute("textContent");

                wait.Until(ExpectedConditions.TextToBePresentInElement(el1, $"{el2text}"));
                }

            driver.FindElement(By.Name("remove_cart_item")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#checkout-cart-wrapper p:nth-child(6) a")));
            driver.FindElement(By.CssSelector("#checkout-cart-wrapper p:nth-child(6) a")).Click();          
        }

        private void SelectSize(By locator2)
        {
            IWebElement sz = driver.FindElement(locator2);
            Actions actions = new Actions(driver);
            actions.MoveToElement(sz)
            .Click()
            .SendKeys("Small")
            .SendKeys(Keys.Enter)
            .Perform();
        }
 
        public static bool IsSelect(IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
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
