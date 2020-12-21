using OpenQA.Selenium;

namespace Cas31.PageObjects
{
    class CheckoutPage : BasePage
    {
        public CheckoutPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public IWebElement buttonBack
        {
            get
            {
                return this.FindElement(By.LinkText("Go back to the site."));
            }
        }

        public HomePage ClickOnButtonBack()
        {
            this.buttonBack.Click();
            this.ExplicitWait(500);
            return new HomePage(this.driver);
        }
    }
}