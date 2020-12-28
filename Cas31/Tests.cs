using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Cas31.PageObjects;
using Cas31.Libraries;
using System.Text.RegularExpressions;

namespace Cas31
{
    class Tests
    {
        private IWebDriver driver;
        private Functions func;

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

        [Test]
        [Category("Shop")]
        public void TestAddMultipleItems()
        {
            string testName = "TestAddMultipleItems()";
            Logger.separator('=');
            Logger.info(testName, "Starting test.");

            CSV csv = new CSV(@"C:\Kurs\user.csv");
            string[] data = csv.GetLine(0);

            HomePage home = new HomePage(this.driver);
            home.GoToPage();

            LoginPage login = home.ClickOnLinkLogin();
            Logger.info(testName, $"Attempting to log in as user '{data[0]}'.");
            login.EnterUsername(data[0]);
            login.EnterPassword(data[1]);
            home = login.ClikOnButtonLogin();

            Assert.AreEqual(true, home.IsUserLoggedIn());
            Logger.info(testName, "User is logged in.");

            CheckoutPage checkout;
            if (home.IsCartEmpty() == false)
            {
                Logger.info(testName, "Cart was not empty, clearing the cart.");
                // Checkout any outstanding orders, before continuing with the test
                CartPage emptyCart = new CartPage(this.driver);
                checkout = emptyCart.ClickOnButtonCheckout();
                home = checkout.ClickOnButtonBack();
            }

            string[] packages = { "starter", "small", "pro", "enterprise" };
            CartPage cart;
            int cartTotal = 0;

            foreach(string package in packages)
            {
                string quantity = this.func.RandomNumber(1, 10);

                Logger.info(
                    testName,
                    string.Format("Adding item \"{0}\" {1}x", package, quantity)
                );
                cart = home.SelectPackage(package, quantity);
                Assert.AreEqual(true, cart.IsDisplayed());
                Assert.AreEqual(true, cart.VerifyItemNameAndQuantity(package, quantity));

                string q, ppi, total;
                cart.GetItemData(package, out q, out ppi, out total);
                cartTotal += Convert.ToInt32(this.func.ExtractNumbers(total));
                Logger.info(
                    testName,
                    string.Format("Added item \"{0}\" {1}x {2} = {3}", package, q, ppi, total)
                );

                home = cart.ClickOnButtonContinueShopping();
            }
            cart = home.ClickOnLinkViewCart();
            
            string pageCartTotal = this.func.ExtractNumbers(cart.GetCartTotal());

            Logger.info(testName, $"Calculated cart total = {cartTotal}");
            Logger.info(testName, $"Page cart total = {pageCartTotal}");

            Assert.AreEqual(cartTotal.ToString(), pageCartTotal);

            checkout = cart.ClickOnButtonCheckout();

            //string orderNumber = this.func.ExtractNumbers(checkout.labelOrderNo.Text);
            //string orderAmount = this.func.ExtractNumbers(checkout.labelAmount.Text);
            string orderNumber = Regex.Match(checkout.labelOrderNo.Text, @"\d+").Value;
            string orderAmount = Regex.Match(checkout.labelAmount.Text, @"\d+").Value;

            Logger.info(testName, $"Order #{orderNumber} billed ${orderAmount}");

            home = checkout.ClickOnButtonBack();
            HistoryPage history = home.ClickOnLinkOrderHistory();

            bool verified = history.VerifyOrderPriceAndStatus(orderNumber, orderAmount + ".00");

            Logger.info(testName, $"Status ordered = {verified.ToString()}");
            Assert.AreEqual(true, verified);
        }

        [SetUp]
        public void SetUp()
        {
            this.driver = new ChromeDriver();
            this.driver.Manage().Window.Maximize();
            this.func = new Functions();
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
