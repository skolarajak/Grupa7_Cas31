using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Cas31.PageObjects;
using Cas31.Libraries;

namespace Cas31
{
    class Tests
    {
        private IWebDriver driver;

        [Test]
        [Category("Shop")]
        public void TestLogin()
        {
            string testName = "TestLogin()";
            Logger.info(testName, "Starting test.");

            HomePage home = new HomePage(this.driver);
            home.GoToPage();

            LoginPage login = home.ClickOnLinkLogin();
            login.EnterUsername("abcd");
            login.EnterPassword("abcd");
            Logger.info(testName, "Attempting to log in.");
            home = login.ClikOnButtonLogin();

            Assert.AreEqual(true, home.IsUserLoggedIn());
            Logger.test(testName, $"home.IsUserLoggedIn (expected true) = {home.IsUserLoggedIn()}");
        }

        [Test]
        [Category("Shop")]
        public void TestRegister()
        {
            string testName = "TestRegister()";
            Logger.info(testName, "Starting test.");

            CSV csv = new CSV(@"C:\Kurs\user.csv");
            string[] data = csv.GetLine(0);

            HomePage home = new HomePage(this.driver);
            home.GoToPage();

            RegisterPage register = home.ClickOnLinkRegister();

            register.EnterFirstName(data[2]);
            Logger.info(testName, $"EnterFirstName({data[2]})");

            register.EnterLastName(data[3]);
            Logger.info(testName, $"EnterLastName({data[3]})");

            register.EnterEmail(data[4]);
            Logger.info(testName, $"EnterEmail({data[4]})");

            register.EnterUsername(data[0]);
            Logger.info(testName, $"EnterUsername({data[0]})");

            register.EnterPassword(data[1]);
            Logger.info(testName, $"EnterPassword({data[1]})");

            register.EnterPasswordAgain(data[1]);
            Logger.info(testName, $"EnterPasswordAgain({data[1]})");
            
            Logger.info(testName, "Attempting to register new user.");
            home = register.ClickOnButtonRegister();

            Assert.AreEqual(true, home.IsAlertSuccessVisible());
            Logger.test(
                testName,
                $"home.IsAlertSuccessVisible (expected true) = {home.IsAlertSuccessVisible()}"
            );
        }

        [Test]
        [Category("Shop")]
        public void TestLoginAndOrder()
        {
            HomePage home = new HomePage(this.driver);
            home.GoToPage();

            LoginPage login = home.ClickOnLinkLogin();
            login.EnterUsername("abcd");
            login.EnterPassword("abcd");

            home = login.ClikOnButtonLogin();
            Assert.AreEqual(true, home.IsUserLoggedIn());

            if (home.IsCartEmpty() == false)
            {
                // Checkout any outstanding orders, before continuing with the test
                CartPage emptyCart = new CartPage(this.driver);
                CheckoutPage checkout = emptyCart.ClickOnButtonCheckout();
                home = checkout.ClickOnButtonBack();
            }

            string package = "pro";
            string quantity = "5";
            string shipping = "free";

            CartPage cart = home.SelectPackage(package, quantity);
            Assert.AreEqual(true, cart.IsDisplayed());
            
            Assert.AreEqual(true, cart.VerifyItemNameAndQuantity(package, quantity));
            Assert.AreEqual(false, cart.VerifyItemNameAndQuantity(package, "6"));
            Assert.AreEqual(false, cart.VerifyItemNameAndQuantity("enterprise", quantity));
            Assert.AreEqual(false, cart.VerifyItemNameAndQuantity("starter", "1"));
            Assert.AreEqual(true, cart.VerifyShipping(shipping));
            Assert.AreEqual(false, cart.VerifyShipping("$20"));
        }

        [Test]
        [Category("Shop - Bulk")]
        public void TestRegisterBulk()
        {
            string testName = "TestRegisterBulk()";
            Logger.info(testName, "Starting test.");

            CSV csv = new CSV(@"C:\Kurs\user.csv");

            int rows = csv.RowCount;
            Logger.info(testName, $"Data row count {rows}.");
            for (int i = 0; i < rows; i++)
            {

                string[] data = csv.GetLine(i);

                HomePage home = new HomePage(this.driver);
                home.GoToPage();

                RegisterPage register = home.ClickOnLinkRegister();

                Logger.info(testName, $"Attempting to register user with data from row {i}.");

                register.EnterFirstName(data[2]);
                Logger.info(testName, $"EnterFirstName({data[2]})");

                register.EnterLastName(data[3]);
                Logger.info(testName, $"EnterLastName({data[3]})");

                register.EnterEmail(data[4]);
                Logger.info(testName, $"EnterEmail({data[4]})");

                register.EnterUsername(data[0]);
                Logger.info(testName, $"EnterUsername({data[0]})");

                register.EnterPassword(data[1]);
                Logger.info(testName, $"EnterPassword({data[1]})");

                register.EnterPasswordAgain(data[1]);
                Logger.info(testName, $"EnterPasswordAgain({data[1]})");

                Logger.info(testName, "Attempting to register new user.");
                home = register.ClickOnButtonRegister();

                Assert.AreEqual(true, home.IsAlertSuccessVisible());
                Logger.test(
                    testName,
                    $"home.IsAlertSuccessVisible (expected true) = {home.IsAlertSuccessVisible()}"
                );
                Logger.separator('=');
            }
            Logger.info(testName, "Finished test.");
        }

        [SetUp]
        public void SetUp()
        {
            this.driver = new ChromeDriver();
            this.driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
            this.driver.Dispose();
        }

        public void Sleep(int miliseconds = 500)
        {
            System.Threading.Thread.Sleep(miliseconds);
        }
    }
}
