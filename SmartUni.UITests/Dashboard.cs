using System;
using OpenQA.Selenium;
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
        public void Search_For_DotNet_Core()
        {
            Assert.True(driver.FindElement(By.ClassName("navbar-brand")).Displayed);
        }
    }
}
