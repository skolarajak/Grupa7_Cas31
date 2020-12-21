using System;
using OpenQA.Selenium;

namespace Cas31.PageObjects
{
    class LoginPage : BasePage
    {

        public LoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement inputUsername
        {
            get
            {
                return this.FindElement(By.Name("username"));
            }
        }

        public IWebElement inputPassword
        {
            get
            {
                return this.FindElement(By.Name("password"));
            }
        }

        public IWebElement buttonLogin
        {
            get
            {
                return this.FindElement(By.Name("login"));
            }
        }

        public void EnterUsername(string username)
        {
            this.inputUsername.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            this.inputPassword.SendKeys(password);
        }

        public HomePage ClikOnButtonLogin()
        {
            this.buttonLogin.Click();
            try
            {
                this.waitElementToBeVisible(By.XPath("//h2[contains(text(), 'Welcome back,')]"));
            } catch (WebDriverTimeoutException)
            {
            } catch (Exception ex)
            {
                throw ex;
            }
            this.ExplicitWait(500);
            return new HomePage(this.driver);
        }
    }
}
