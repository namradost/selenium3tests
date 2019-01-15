using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class TestsForStikers
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
        public void ProductsStikers()
        {
            driver.Url = "http://localhost/litecart/en/";
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#logotype-wrapper")));
            List<IWebElement> productItemsForAssert = driver.FindElement(By.CssSelector("div#page")).FindElements(By.CssSelector(".image-wrapper")).ToList();
            int productsItemsCount = productItemsForAssert.Count;

            for (int i = 0; i <= productsItemsCount - 1; i++)
            {
                productItemsForAssert = driver.FindElement(By.CssSelector("div#page")).FindElements(By.CssSelector(".image-wrapper")).ToList();
                List<IWebElement> stikers = productItemsForAssert[i].FindElements(By.CssSelector("div.sticker")).ToList();               
                int stikersCount = stikers.Count;
                if (stikersCount == 1)
                {
                    //True: Item has 1 sticker
                    driver.FindElement(By.Name("email")).SendKeys("+"); 
                    //не знаю, куда выводить результаты, поэтому считала стикеры для уточек в поле Email
                }
                else
                    //False: Incorrect result
                     driver.FindElement(By.Name("email")).SendKeys("-");
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
