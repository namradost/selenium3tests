using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;


namespace csharp_example
{
    [TestFixture]
    public class SortingContriesZones
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
        public void Sorting()
        {
            driver.Url = "http://localhost/litecart/admin/login.php";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));

            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            wait.Until(ExpectedConditions.TitleContains("Countries | My Store"));

            //Sorting for Contries
            List<string> sortList = new List<string>();
            IList <IWebElement> countriesList = driver.FindElements(By.CssSelector("#content table tr td:nth-child(5) a"));
            foreach (IWebElement we in countriesList)
            {
                sortList.Add(we.Text);
            }
            List<string> sortedList = new List<string>();
            foreach (string s in sortList)
            {
                sortedList.Add(s);
            }
            sortedList.Sort();
            Assert.True(sortedList.SequenceEqual(sortList));

            //Sortig for Zones into Contries
            List<IWebElement> countrieslist2 = driver.FindElements(By.CssSelector("table.dataTable tr.row")).ToList();
            int countryCount = countrieslist2.Count;

            for (int i = 0; i <= countryCount - 1; i++)
            {
                IWebElement zone = driver.FindElement(By.CssSelector($"table tbody tr:nth-child({i + 1}) td:nth-child(6)"));
                string zonecount = zone.GetAttribute("textContent");

                if (zonecount != "0")
                {
                    driver.FindElement(By.CssSelector($"table tr:nth-child({i + 1}) td:nth-child(5) a")).Click();

                    List<string> sortList2 = new List<string>();
                    IList<IWebElement> zonesList = driver.FindElements(By.CssSelector("#table-zones td:nth-child(3)"));     

                    foreach (IWebElement we in zonesList)
                    {

                        sortList2.Add(we.Text);
                        sortList2.Remove("");
                    }
                    List<string> sortedList2 = new List<string>();
                    foreach (string s in sortList2)
                    {
                        sortedList2.Add(s);
                    }
                    sortedList2.Sort();
                    Assert.True(sortedList2.SequenceEqual(sortList2));
                    driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
                }
            }

            //Sorting for GeoZones
            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            List<IWebElement> zoneslist2 = driver.FindElements(By.CssSelector("#content td:nth-child(3)")).ToList();
            int zonesCount2 = zoneslist2.Count;

            for (int i = 0; i <= zonesCount2 - 1; i++)
            {

                driver.FindElement(By.CssSelector($"#content table tr:nth-child({i + 2}) td:nth-child(3) a")).Click();
                List<string> sortList3 = new List<string>();
                IList<IWebElement> zonesList3 = driver.FindElements(By.CssSelector("#table-zones td:nth-child(3) select option[selected = 'selected']"));

                    foreach (IWebElement we in zonesList3)
                    {
                        sortList3.Add(we.Text);
                    }
                    List<string> sortedList3 = new List<string>();
                    foreach (string s in sortList3)
                    {
                        sortedList3.Add(s);
                    }
                    sortedList3.Sort();
                    Assert.True(sortedList3.SequenceEqual(sortList3));
                    driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";
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
