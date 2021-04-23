using System;
using BookingComTests.Pages;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BookingComTests.Tests
{
    public class BookingComTest
    {
        private const string SiteUrl = "https://www.booking.com/";
        private IWebDriver _driver;
        private HomePage _homePage;
        private SignInPage _signInPage;

        private static string Email { get; }
        private static string Password { get; }

        static BookingComTest()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            Email = config["Settings:BookingEmail"];
            Password = config["Settings:BookingPassword"];
        }

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArguments("--lang=ru-RU");
            _driver = new ChromeDriver(options);
            _driver.Navigate().GoToUrl(SiteUrl);
            _homePage = new HomePage(_driver);
            _signInPage = new SignInPage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }

        [Test]
        public void ChangeLanguage()
        {
            _homePage.SwitchToEsLanguage();
            Assert.AreEqual("Alojamiento", _homePage.GetStaysTabText());
        }

        [Test]
        public void GoToFlightsPage()
        {
            _homePage.GoToFlightsPage();
            Assert.True(_driver.Url.StartsWith("https://booking.kayak.com"));
        }

        [Test]
        public void CheckFilter()
        {
            _homePage.SetDestinationInFilter("Москва");
            _homePage.SetCheckInAndCheckOutDatesInFilter(DateTime.Today.AddDays(7), DateTime.Today.AddDays(9));
            _homePage.IncreaseChildrenAmountInFilter(1);
            _homePage.SearchResults();
            Assert.True(_driver.Url.StartsWith("https://www.booking.com/searchresults"));
        }

        [Test]
        public void SignIn()
        {
            _homePage.GoToSignInPage();
            _signInPage.SignIn(Email, Password);
            _homePage.GoToAccountManagementPage();
            Assert.True(_driver.Url.StartsWith("https://account.booking.com/mysettings"));
        }
    }
}