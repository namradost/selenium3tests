using System;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class ItemPage: Page
    {    

        public ItemPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);

        }

        internal void AddProductInCart()
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
    }
}
