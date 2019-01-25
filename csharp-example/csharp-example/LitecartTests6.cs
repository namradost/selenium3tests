using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class Registration
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
        public void NewCustomerTest()
        {
            //login data
            Random rand = new Random();
            int num = rand.Next(50);
            string customerEmail = num + "test@tut.by";
            string customerPassw = "999999999";
            //test
            driver.Url = "http://localhost/litecart/en/";
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#logotype-wrapper")));
            NewCustomerRegistration(customerEmail, customerPassw);
            logout();
            login(customerEmail, customerPassw);
            logout();
        }

        private void NewCustomerRegistration(string customerEmail, string customerPassw)
        {
            //registration data
            string customerFN = "Natasha";
            string customerLN = "Rostova";
            string customerAddr = "Street 123";
            string customerZIP = "12345";
            string customerCity = "Chikago";
            string customerCountry = "United States";
            string customerState = "Illinois";
            string customerPhone = "+12121234567";

            driver.Url = "http://localhost/litecart/en/";
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#logotype-wrapper")));

            IWebElement registration = driver.FindElement(By.CssSelector("div#box-account-login a[href*=create_account]"));
            registration.Click();

            IWebElement fn = driver.FindElement(By.Name("firstname"));
            fn.Click();
            fn.Clear();
            fn.SendKeys(customerFN);

            IWebElement lastName = driver.FindElement(By.Name("lastname"));
            lastName.Click();
            lastName.Clear();
            lastName.SendKeys(customerLN);

            IWebElement address1 = driver.FindElement(By.Name("address1"));
            address1.Click();
            address1.Clear();
            address1.SendKeys(customerAddr);

            IWebElement city = driver.FindElement(By.Name("city"));
            city.Click();
            city.Clear();
            city.SendKeys(customerCity);

            IWebElement country = driver.FindElement(By.CssSelector("span.selection"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(country)
            .Click()
            .SendKeys(customerCountry)
            .SendKeys(Keys.Enter)
            .Perform();

            IWebElement postcode = driver.FindElement(By.Name("postcode"));
            postcode.Click();
            postcode.Clear();
            postcode.SendKeys(customerZIP);

            IWebElement email = driver.FindElement(By.Name("email"));
            email.Click();
            email.Clear();
            email.SendKeys(customerEmail);

            IWebElement phone = driver.FindElement(By.Name("phone"));
            phone.Click();
            phone.Clear();
            phone.SendKeys(customerPhone);

            IWebElement password = driver.FindElement(By.Name("password"));
            password.Click();
            password.Clear();
            password.SendKeys(customerPassw);

            IWebElement cpassword = driver.FindElement(By.Name("confirmed_password"));
            cpassword.Click();
            cpassword.Clear();
            cpassword.SendKeys(customerPassw);

            driver.FindElement(By.Name("create_account")).Click();

            IWebElement state = driver.FindElement(By.Name("zone_code"));
            Actions actionss = new Actions(driver);
            actionss.MoveToElement(state)
            .Click()
            .SendKeys(customerState)
            .SendKeys(Keys.Enter)
            .Perform();

            IWebElement password2 = driver.FindElement(By.Name("password"));
            password2.Click();
            password2.Clear();
            password2.SendKeys(customerPassw);

            IWebElement cpassword2 = driver.FindElement(By.Name("confirmed_password"));
            cpassword2.Click();
            cpassword2.Clear();
            cpassword2.SendKeys(customerPassw);

            driver.FindElement(By.Name("create_account")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#main div#notices-wrapper")));
        }

        private void login(string customerEmail, string customerPassw)
        {
            IWebElement email = driver.FindElement(By.Name("email"));
            email.Click();
            email.Clear();
            email.SendKeys(customerEmail);
            IWebElement passw = driver.FindElement(By.Name("password"));
            passw.Click();
            passw.Clear();
            passw.SendKeys(customerPassw);
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#main div#notices-wrapper")));
        }

        private void logout()
        {
            driver.FindElement(By.CssSelector("div#box-account div.content li:last-child a")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div#main div#notices-wrapper")));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;       
        }

    }
}
