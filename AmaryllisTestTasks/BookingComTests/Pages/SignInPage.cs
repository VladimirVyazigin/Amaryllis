using OpenQA.Selenium;

namespace BookingComTests.Pages
{
    public class SignInPage : BasePage
    {
        private static By EmailInput { get; }
        private static By PasswordInput { get; }
        private static By ContinueWithEmailButton { get; }
        private static By SignInButton { get; }

        static SignInPage()
        {
            EmailInput = By.Id("username");
            PasswordInput = By.Id("password");
            ContinueWithEmailButton = By.CssSelector("button[type=\"submit\"]");
            SignInButton = By.CssSelector("button[type=\"submit\"]");
        }

        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public void SignIn(string email, string password)
        {
            FindElement(EmailInput).SendKeys(email);
            FindElement(ContinueWithEmailButton).Click();
            WaitUntilElementAppears(PasswordInput);
            FindElement(PasswordInput).SendKeys(password);
            FindElement(SignInButton).Click();
        }
    }
}