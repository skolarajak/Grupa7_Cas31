using OpenQA.Selenium;

namespace Cas31.PageObjects
{
    class HistoryPage : BasePage
    {
        public HistoryPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public bool IsDisplayed()
        {
            IWebElement labelHistory = this.FindElement(
                By.XPath("//h1[contains(., 'Order History')]")
            );
            return labelHistory.Displayed;
        }

        public bool VerifyOrderPriceAndStatus(string order, string price)
        {
            IWebElement itemPrice = this.FindElement(
                By.XPath($"//table/tbody/tr[contains(., '#{order}')]/td[3]")
            );
            if (itemPrice.Text != price)
            {
                return false;
            }

            IWebElement itemStatus = this.FindElement(
                By.XPath($"//table/tbody/tr[contains(., '#{order}')]/td[4]")
            );
            if (itemStatus.Text.ToUpper() != "ORDERED")
            {
                return false;
            }

            return true;
        }
    }
}