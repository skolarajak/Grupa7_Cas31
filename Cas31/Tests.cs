using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Cas31.PageObjects;

namespace Cas31
{
    class Tests
    {
        private IWebDriver driver;

        [Test]
        [Category("Shop")]
        public void TestLogin()
        {
            HomePage home = new HomePage(this.driver);
            home.GoToPage();

            LoginPage login = home.ClickOnLinkLogin();
            login.EnterUsername("abcd");
            login.EnterPassword("abcd");

            home = login.ClikOnButtonLogin();

            Assert.AreEqual(true, home.IsUserLoggedIn());
        }

        [Test]
        [Category("Shop")]
        public void TestRegister()
        {
            HomePage home = new HomePage(this.driver);
            home.GoToPage();

            RegisterPage register = home.ClickOnLinkRegister();
            register.EnterFirstName("abcd");
            register.EnterLastName("abcd");
            register.EnterEmail("abcd@abcd.efg");
            register.EnterUsername("abcd");
            register.EnterPassword("abcd");
            register.EnterPasswordAgain("abcd");

            home = register.ClickOnButtonRegister();

            Assert.AreEqual(true, home.IsAlertSuccessVisible());
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
