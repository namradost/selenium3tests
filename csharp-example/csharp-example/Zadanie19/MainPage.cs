using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example
{
    public class MainPage: Page
    {    

        public MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);

        }

        internal MainPage Open()
        {            
            driver.Url = "http://litecart.stqa.ru/en/";
            return this;

        }

        internal MainPage WaitLogotype()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div#logotype-wrapper")));
            return this;

        }
    }
}
