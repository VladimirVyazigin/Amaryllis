using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BookingComTests.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected IWebElement FindElement(By selector)
        {
            return Driver.FindElement(selector);
        }

        protected void ClickElement(By selector)
        {
            FindElement(selector).Click();
        }

        protected string GetElementText(By selector)
        {
            return FindElement(selector).Text;
        }

        protected void SetElementText(By selector, string text)
        {
            FindElement(selector).SendKeys(text);
        }

        protected void WaitUntilElementAppears(By selector, int seconds = 3)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementIsVisible(selector));
        }
    }
}