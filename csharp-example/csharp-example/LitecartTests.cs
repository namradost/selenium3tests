using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class Login
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
        public void FirstTest()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));
            driver.FindElement(By.LinkText("ACME Corp.")).Click();
            driver.FindElement(By.Name("order_id")).SendKeys("2");
            driver.FindElement(By.XPath("//*[@id=\"content\"]/div/form/button")).Click();
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;       
        }
    }
}
