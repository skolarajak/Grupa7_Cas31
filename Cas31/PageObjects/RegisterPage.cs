using OpenQA.Selenium;

namespace Cas31.PageObjects
{
    class RegisterPage : BasePage
    {
        public RegisterPage(IWebDriver driver) : base(driver) { }

        private IWebElement inputFirstName
        {
            get
            {
                return this.FindElement(By.Name("ime"));
            }
        }

        private IWebElement inputLastName
        {
            get
            {
                return this.FindElement(By.Name("prezime"));
            }
        }

        private IWebElement inputEmail
        {
            get
            {
                return this.FindElement(By.Name("email"));
            }
        }

        private IWebElement inputUsername
        {
            get
            {
                return this.FindElement(By.Name("korisnicko"));
            }
        }

        private IWebElement inputPassword
        {
            get
            {
                return this.FindElement(By.Name("lozinka"));
            }
        }

        private IWebElement inputPasswordRepeat
        {
            get
            {
                return this.FindElement(By.Name("lozinkaOpet"));
            }
        }

        private IWebElement buttonRegister
        {
            get
            {
                return this.FindElement(By.Name("register"));
            }
        }

        public void EnterFirstName(string firstName)
        {
            this.inputFirstName.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            this.inputLastName.SendKeys(lastName);
        }

        public void EnterEmail(string email)
        {
            this.inputEmail.SendKeys(email);
        }

        public void EnterUsername(string username)
        {
            this.inputUsername.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            this.inputPassword.SendKeys(password);
        }

        public void EnterPasswordAgain(string password)
        {
            this.inputPasswordRepeat.SendKeys(password);
        }

        public HomePage ClickOnButtonRegister()
        {
            this.buttonRegister.Click();
            this.waitElementToBeVisible(
                By.XPath("//div[contains(@class, 'success') and contains(., 'Uspeh')]")
            );
            this.ExplicitWait(500);
            return new HomePage(this.driver);
        }

    }
}
