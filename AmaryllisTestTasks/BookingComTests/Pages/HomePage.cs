using System;
using OpenQA.Selenium;

namespace BookingComTests.Pages
{
    public class HomePage : BasePage
    {
        private static By LanguageSwitchButton { get; }
        private static By EsLanguageButton { get; }
        private static By FlightsTab { get; }
        private static By StaysTab { get; }
        private static By DestinationInput { get; }
        private static By DatesPickerDiv { get; }
        private static By GuestsPickerDiv { get; }
        private static By SearchResultsButton { get; }
        private static By IncreaseChildrenAmountButton { get; }
        private static By SignInButton { get; }
        private static By AvatarBlockDiv { get; }
        private static By ManageAccountMenuItem { get; }

        static HomePage()
        {
            LanguageSwitchButton = By.CssSelector("button[data-modal-id=\"language-selection\"]");
            EsLanguageButton = By.CssSelector("a[data-lang=\"es\"]");
            FlightsTab = By.CssSelector("a[data-ga-track*=\"flights\"]");
            StaysTab = By.CssSelector("a[data-ga-track*=\"accommodation\"] .bui-tab__text");
            DestinationInput = By.CssSelector("input[data-component=\"search/destination/input-placeholder\"]");
            DatesPickerDiv = By.CssSelector("div.xp__dates.xp__group");
            GuestsPickerDiv = By.CssSelector("div.xp__input-group.xp__guests");
            SearchResultsButton = By.ClassName("sb-searchbox__button");
            IncreaseChildrenAmountButton = By.CssSelector("div.sb-group-children button.bui-stepper__add-button");
            SignInButton = By.CssSelector(".bui-group__item:last-child");
            AvatarBlockDiv = By.ClassName("bui-avatar-block");
            ManageAccountMenuItem = By.CssSelector(".bui-dropdown-menu__item:first-child");
        }

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void SetDestinationInFilter(string destination)
        {
            SetElementText(DestinationInput, destination);
        }

        public void SetCheckInAndCheckOutDatesInFilter(DateTime checkInDate, DateTime checkOutDate)
        {
            ClickElement(DatesPickerDiv);
            var checkInDateTd = By.CssSelector($"td.bui-calendar__date[data-date=\"{checkInDate:yyyy-MM-dd}\"]");
            var checkOutDateTd = By.CssSelector($"td.bui-calendar__date[data-date=\"{checkOutDate:yyyy-MM-dd}\"]");
            ClickElement(checkInDateTd);
            ClickElement(checkOutDateTd);
        }

        public void IncreaseChildrenAmountInFilter(int count)
        {
            ClickElement(GuestsPickerDiv);
            for (var i = 0; i < count; i++) ClickElement(IncreaseChildrenAmountButton);
        }

        public void SearchResults()
        {
            ClickElement(SearchResultsButton);
        }

        public void SwitchToEsLanguage()
        {
            ClickElement(LanguageSwitchButton);
            ClickElement(EsLanguageButton);
        }

        public string GetStaysTabText()
        {
            return GetElementText(StaysTab);
        }

        public void GoToFlightsPage()
        {
            ClickElement(FlightsTab);
        }

        public void GoToSignInPage()
        {
            ClickElement(SignInButton);
        }

        public void GoToAccountManagementPage()
        {
            WaitUntilElementAppears(AvatarBlockDiv);
            ClickElement(AvatarBlockDiv);
            ClickElement(ManageAccountMenuItem);
        }
    }
}