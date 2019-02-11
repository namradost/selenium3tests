using System;
using NUnit.Framework;

namespace csharp_example
{
    [TestFixture]
    public class PageObgectTest: TestBase
    {
        [Test]

        public void mainTest()
        {
            app.mainPage.Open();
            app.mainPage.WaitLogotype();
            app.itemPage.AddProductInCart();
            app.cartPage.DeleteFromCart();
        }

        static void Main()
        {

        }

    }
}
