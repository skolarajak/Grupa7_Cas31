﻿using OpenQA.Selenium;

namespace Cas31.PageObjects
{
    class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public IWebElement buttonCheckout
        {
            get
            {
                return this.FindElement(By.Name("checkout"));
            }
        }

        public IWebElement buttonContinueShopping
        {
            get
            {
                return this.FindElement(By.LinkText("Continue shopping"));
            }
        }

        public bool IsDisplayed()
        {
            IWebElement labelCart = this.FindElement(
                By.XPath("//h1[contains(., 'Quality Assurance (QA) course - Order')]")
            );
            return labelCart.Displayed;
        }

        public bool VerifyItemNameAndQuantity(string package, string quantity)
        {
            package = package.ToUpper();

            IWebElement itemQty = this.FindElement(
                By.XPath($"//table/tbody/tr[contains(., \"{package}\")]/td[2]")
            );
            if (itemQty == null)
            {
                return false;
            }

            return itemQty.Text == quantity;
        }

        public bool VerifyShipping(string shipping)
        {
            shipping = shipping.ToUpper();

            IWebElement itemShipping = this.FindElement(
                By.XPath("//table/tbody/tr[contains(., 'Shipping')]/td[3]")
            );

            if (itemShipping == null)
            {
                return false;
            }

            return itemShipping.Text == shipping;
        }

        public string GetCartTotal()
        {
            IWebElement cartTotal = this.FindElement(
                By.XPath("//table/tbody/tr/td[contains(., 'Total:')]")
            );
            return cartTotal.Text;
        }

        public void GetItemData(string package, out string quantity, out string pricePerItem, out string priceTotal)
        {
            package = package.ToUpper();

            IWebElement itemQty = this.FindElement(
                By.XPath($"//table/tbody/tr[contains(., \"{package}\")]/td[2]")
            );
            IWebElement itemPPI = this.FindElement(
                By.XPath($"//table/tbody/tr[contains(., \"{package}\")]/td[3]")
            );
            IWebElement itemTotal = this.FindElement(
                By.XPath($"//table/tbody/tr[contains(., \"{package}\")]/td[4]")
            );

            quantity = itemQty.Text;
            pricePerItem = itemPPI.Text;
            priceTotal = itemTotal.Text;
        }

        public CheckoutPage ClickOnButtonCheckout()
        {
            this.buttonCheckout.Click();
            this.ExplicitWait(500);
            return new CheckoutPage(this.driver);
        }

        public HomePage ClickOnButtonContinueShopping()
        {
            this.buttonContinueShopping.Click();
            this.ExplicitWait(500);
            return new HomePage(this.driver);
        }
    }
}