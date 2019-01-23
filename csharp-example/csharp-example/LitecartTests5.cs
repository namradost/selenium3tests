using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class TestsForProducts
    {
        private IWebDriver driver;
        private IWebDriver firefoxdriver;
        private IWebDriver iedriver;
        private IWebDriver edgedriver;
        private WebDriverWait wait;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver = new InternetExplorerDriver();
            driver = new FirefoxDriver();
            driver = new EdgeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void ProductsPrices() 
        {
            driver.Url = "http://localhost/litecart/en/";
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#logotype-wrapper")));

            //for Product Name
            IWebElement firstProduct = driver.FindElement(By.XPath(".//*[@id='box-campaigns']//ul/li[1]"));
            IWebElement firstProductName = firstProduct.FindElement(By.CssSelector("div.name"));
            string productName = firstProductName.GetAttribute("textContent");

            //for Product Old Grey Price
            IWebElement productPriceOld = firstProduct.FindElement(By.CssSelector("s.regular-price"));
            string productPriceOldText = productPriceOld.GetAttribute("textContent");
            string productPriceOldCl = productPriceOld.GetAttribute("class");
            string productPriceOldCol = productPriceOld.GetCssValue("color");
            string productPriceOldDec = productPriceOld.GetCssValue("text-decoration-line");
            string productPriceOldS = productPriceOld.GetAttribute("tagName");
            string productPriceOldSA = "S";
            string productPriceOldSz = productPriceOld.GetCssValue("font-size");

            //for Product Auction Red Price
            IWebElement productPriceNew = firstProduct.FindElement(By.CssSelector("strong.campaign-price"));
            string productPriceNewText = productPriceNew.GetAttribute("textContent");
            string productPriceNewCl = productPriceNew.GetAttribute("class");
            string productPriceNewCol = productPriceNew.GetCssValue("color");
            string productPriceNewDec = productPriceNew.GetAttribute("tagName");
            string productPriceNewSA = "STRONG";
            string productPriceNewSz = productPriceNew.GetCssValue("font-size");

            //Click on Product Name -> Page with details
            firstProductName.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#box-product h1")));

            //Duplicate options into
            IWebElement boxForProduct = driver.FindElement(By.CssSelector("div#box-product"));
            IWebElement firstProductNameInto = driver.FindElement(By.CssSelector("div#box-product h1"));
            string firstProductNameIn = firstProductNameInto.GetAttribute("textContent");

            //for Product Old Grey Price into
            IWebElement productPriceOldIn = 
                boxForProduct.FindElement(By.CssSelector("s.regular-price"));
            string productPriceOldTextIn = productPriceOldIn.GetAttribute("textContent");
            string productPriceOldClIn = productPriceOldIn.GetAttribute("class");
            string productPriceOldColIn = productPriceOldIn.GetCssValue("color");
            string productPriceOldDecIn = productPriceOldIn.GetCssValue("text-decoration-line");
            string productPriceOldSIn = productPriceOldIn.GetAttribute("tagName");
            string productPriceOldSAIn = "S";
            string productPriceOldSzIn = productPriceOldIn.GetCssValue("font-size");

            //for Product Auction Red Price into
            IWebElement productPriceNewIn = 
                boxForProduct.FindElement(By.CssSelector("strong.campaign-price"));
            string productPriceNewTextIn = productPriceNewIn.GetAttribute("textContent");
            string productPriceNewClIn = productPriceNewIn.GetAttribute("class");
            string productPriceNewColIn = productPriceNewIn.GetCssValue("color");
            string productPriceNewDecIn = productPriceNewIn.GetAttribute("tagName");
            string productPriceNewSAIn = "STRONG";
            string productPriceNewSzIn = productPriceNewIn.GetCssValue("font-size");

            //Color
            string colorAss = "0";

            //Assertions
            Assert.AreEqual(productName, firstProductNameIn);

            //Old price
            Assert.AreEqual(productPriceOldText, productPriceOldTextIn);
            Assert.AreEqual(productPriceOldCl, productPriceOldClIn);
            Assert.AreEqual(productPriceOldDec, productPriceOldDecIn);
            Assert.AreEqual(productPriceOldS, productPriceOldSA);
            Assert.AreEqual(productPriceOldSIn, productPriceOldSAIn);
            Assert.NotNull(productPriceOldCol);
            Assert.NotNull(productPriceOldColIn);
            Assert.AreEqual(productPriceOldCol.Substring(5, 3),
                productPriceOldCol.Substring(10, 3), productPriceOldCol.Substring(15, 3));
            Assert.AreEqual(productPriceOldColIn.Substring(5, 3),
                productPriceOldColIn.Substring(10, 3), productPriceOldColIn.Substring(15, 3));

            //New price
            Assert.AreEqual(productPriceNewText, productPriceNewTextIn);
            Assert.AreEqual(productPriceNewCl, productPriceNewClIn);
            Assert.AreEqual(productPriceNewCol, productPriceNewColIn);
            Assert.AreEqual(productPriceNewDec, productPriceNewSA);
            Assert.AreEqual(productPriceNewDecIn, productPriceNewSAIn);
            Assert.NotNull(productPriceNewCol);
            Assert.NotNull(productPriceNewColIn);
            Assert.AreEqual(productPriceNewCol.Substring(10, 1), 
                productPriceNewCol.Substring(13, 1), colorAss);
            Assert.AreEqual(productPriceNewColIn.Substring(10, 1), 
                productPriceNewColIn.Substring(13, 1), colorAss);

            //Size of Prices
            Assert.Greater(productPriceNewSz, productPriceOldSz);
            Assert.Greater(productPriceNewSzIn, productPriceOldSzIn);
        }


        [TearDown]
    public void stop()
    {
        driver.Quit();
        driver = null;
    }

    }
}
