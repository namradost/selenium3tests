using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class AddNewProduct
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
        public void AddProduct()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            GoToAdding();
            AddGeneral();
            AddInformation();
            AddPrices();
            ClickOnSave();
        }
        private void AddGeneral()
        {
            //general tab
            string productName = "Perfect Fish";
            string codeN = "999";
            string quantityN = "1";
            string relativePath = "./csharp-example/fish.jpg";
            string absolutePath = Path.GetFullPath(relativePath);

            IWebElement status = driver.FindElement(By.CssSelector("div#tab-general input[value='1'][type='radio']"));
            status.Click();

            IWebElement name = driver.FindElement(By.Name("name[en]"));
            name.Click();
            name.Clear();
            name.SendKeys(productName);
           
            IWebElement code = driver.FindElement(By.Name("code"));
            code.Click();
            code.Clear();
            code.SendKeys(codeN);

            IWebElement categ = driver.FindElement(By.CssSelector("div#tab-general input[value='0'][type='checkbox']"));
            string box = categ.GetAttribute("checked");
            if (box == null)
            {
                categ.Click();
            }

            IWebElement gender = driver.FindElement(By.CssSelector("div#tab-general input[value='1-3'][type='checkbox']"));
            string box2 = gender.GetAttribute("checked");
            if (box2 == null)
            {
                gender.Click();
            }

            IWebElement quantity = driver.FindElement(By.Name("quantity"));
            quantity.Click();
            quantity.Clear();
            quantity.SendKeys(quantityN);

            IWebElement uploadImages = driver.FindElement(By.CssSelector("div#tab-general input[name='new_images[]']"));
            uploadImages.SendKeys(absolutePath);

            IWebElement validFrom = driver.FindElement(By.Name("date_valid_from"));
            validFrom.SendKeys("25.01.2019");

            IWebElement validTo = driver.FindElement(By.Name("date_valid_to"));
            validTo.SendKeys("25.01.2029");
        }
        private void AddInformation()
        {
            //information tab
            string words = "fish perfect";
            string shortDesc = "My funny fish";
            string descr = "It is very beautiful funny little fish";
            string title = "Fish";
            string metaDesc = "fish";
            string manuf = "ACME Corp.";

            driver.FindElement(By.CssSelector("div.tabs a[href='#tab-information']")).Click();

            IWebElement state = driver.FindElement(By.Name("manufacturer_id"));
            Actions actionss = new Actions(driver);
            actionss.MoveToElement(state)
            .Click()
            .SendKeys(manuf)
            .SendKeys(Keys.Enter)
            .Perform();

            IWebElement keywords = driver.FindElement(By.Name("keywords"));
            keywords.Click();
            keywords.Clear();
            keywords.SendKeys(words);

            IWebElement shortDescr = driver.FindElement(By.Name("short_description[en]"));
            shortDescr.Click();
            shortDescr.Clear();
            shortDescr.SendKeys(shortDesc);

            IWebElement description = driver.FindElement(By.CssSelector("div#tab-information div.trumbowyg-editor"));
            description.Click();
            description.SendKeys(descr);
           
            IWebElement htitle = driver.FindElement(By.Name("head_title[en]"));
            htitle.Click();
            htitle.Clear();
            htitle.SendKeys(title);

            IWebElement metaDescr = driver.FindElement(By.Name("meta_description[en]"));
            metaDescr.SendKeys(metaDesc);
        }
        private void AddPrices()
        {
            string price1 = "20";
            string price2 = "25";
            string curr = "US Dollars";

            //prices tab
            driver.FindElement(By.CssSelector("div.tabs a[href='#tab-prices']")).Click();

            IWebElement pprice = driver.FindElement(By.Name("purchase_price"));
            pprice.Click();
            pprice.Clear();
            pprice.SendKeys(price1);

            IWebElement pricev = driver.FindElement(By.Name("purchase_price_currency_code"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(pricev)
            .Click()
            .SendKeys(curr)
            .SendKeys(Keys.Enter)
            .Perform();

            IWebElement uprice2 = driver.FindElement(By.CssSelector("div#tab-prices input[data-type='currency']"));
            uprice2.Click();
            uprice2.Clear();
            uprice2.SendKeys(price2);
        }

        private void ClickOnSave()
        {
            //save
            driver.FindElement(By.CssSelector("span.button-set button[type='submit'][value='Save']")).Click();
            wait.Until(ExpectedConditions.TitleIs("Catalog | My Store"));
        }

        private void GoToAdding()
        {
            //click on Catalog
            List<IWebElement> menuItemsToClick = driver.FindElement(By.CssSelector("div#box-apps-menu-wrapper"))
                    .FindElements(By.CssSelector(".name")).ToList();
            for (int i = 1; i < 2; i++)
            {
                var menuToClick = driver.FindElement(By.CssSelector($"#box-apps-menu > li:nth-child({i + 1})"));
                menuToClick.Click();
            }
            Thread.Sleep(1000);

            //click on Add New Product
            List<IWebElement> buttonsToClick = driver.FindElement(By.CssSelector("td#content"))
                .FindElements(By.CssSelector("a.button")).ToList();
            for (int i = 1; i < 2; i++)
            {
                var buttonToClick = driver.FindElement(By.CssSelector($"a.button:nth-child({i + 1})"));
                buttonToClick.Click();
            }
            Thread.Sleep(1000);

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;       
        }
    }
}
