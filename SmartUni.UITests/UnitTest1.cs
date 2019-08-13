using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SmartUni.UITests.SeleniumHelpers;
using Xunit;

namespace SmartUni.UITests
{
    public static class UnitTest1
    {
        [Fact]
        public static void Search_For_DotNet_Core()
        {
            string url = "https://smartuni.azurewebsites.net/"; // Google doesn't seem to work properly in IE at the moment...

            using (var driver = new DriverFactory().Create())
            {
                driver.Navigate().GoToUrl(url);
                Assert.True(driver.FindElement(By.ClassName("navbar-brand")).Displayed);
            }
        }
    }
}
