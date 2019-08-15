using System;
using OpenQA.Selenium;
using SmartUni.UITests.PageObjects;
using SmartUni.UITests.SeleniumHelpers;
using Xunit;

namespace SmartUni.UITests
{
    public class Dashboard
    {
        private readonly string page_url;
        private readonly IWebDriver driver;
        
        public Dashboard()
        {
            page_url = "https://smartuni.azurewebsites.net/";
            driver = new DriverFactory().Create();
            driver.Navigate().GoToUrl(page_url);
        }

        [Fact]
        public void GetLogoTitle()
        {
            Assert.True(new HeaderBar(driver).GetLogoTitle());
        }
    }
}
