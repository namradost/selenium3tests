using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class CartPage: Page
    {    

        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);

        }

        internal CartPage DeleteFromCart()
        {
            driver.FindElement(By.CssSelector("#cart a.content")).Click();
            List<IWebElement> tableItems = driver.FindElements(By.CssSelector("#order_confirmation-wrapper td.item")).ToList();
            int itemsCount = tableItems.Count;

            for (int i = itemsCount; i > 1; i--)
            {
                IWebElement el1 = driver.FindElement(By.CssSelector("table tr:nth-child(2) td.item"));
                string el1text = el1.GetAttribute("textContent");

                driver.FindElement(By.Name("remove_cart_item")).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("table tr:nth-child(2) td.item")));
                wait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.CssSelector("table tr:nth-child(2) td.item"), $"{el1text}"));
            }
            driver.FindElement(By.Name("remove_cart_item")).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#checkout-cart-wrapper a")));
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("#checkout-cart-wrapper a")).Click();

            return this;
        }


    }
}
