using OpenQA.Selenium;

namespace Cas31.PageObjects
{
    class HomePage : BasePage
    {
        //private IWebDriver driver;

        public HomePage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public IWebElement labelQAShop
        {
            get
            {
                return this.FindElement(By.XPath("//h1[text()='Quality Assurance (QA) Shop']"));
            }
        }

        private IWebElement linkLogout
        {
            get
            {
                return this.FindElement(By.PartialLinkText("Logout"));
            }
        }

        public IWebElement linkLogin
        {
            get
            {
                return this.FindElement(By.LinkText("Login"));
            }
        }

        public IWebElement linkRegister
        {
            get
            {
                return this.FindElement(By.LinkText("Register"));
            }
        }

        public IWebElement alertSuccess
        {
            get
            {
                return this.FindElement(
                    By.XPath("//div[contains(@class, 'success') and contains(., 'Uspeh')]")
                );
            }
        }

        public void GoToPage()
        {
            this.GoToURL("http://shop.qa.rs");
        }

        public bool IsUserLoggedIn()
        {
            if (this.linkLogout != null)
            {
                return this.linkLogout.Displayed;
            } else
            {
                return false;
            }
        }

        public bool IsAlertSuccessVisible()
        {
            return this.alertSuccess.Displayed;
        }

        public LoginPage ClickOnLinkLogin()
        {
            linkLogin.Click();
            this.waitElementToBeVisible(By.XPath("//h2[text()='Prijava']"));
            this.ExplicitWait(500);
            return new LoginPage(this.driver);
        }

        public RegisterPage ClickOnLinkRegister()
        {
            linkRegister.Click();
            this.waitElementToBeVisible(By.Name("register"));
            this.ExplicitWait(500);
            return new RegisterPage(this.driver);
        }

    }
}
