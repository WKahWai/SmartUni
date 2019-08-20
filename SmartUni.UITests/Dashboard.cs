using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SmartUni.UITests.PageObjects;
using SmartUni.UITests.SeleniumHelpers;
using Xunit;

namespace SmartUni.UITests
{

    public class Dashboard : IDisposable
    {
        private readonly string page_url;
        private readonly IWebDriver driver;
        private Headerbar headerBar;
        private Sidebar sidebar;
        private IWebElement pageContent;
        private readonly By H1 = By.TagName("h1");
        private readonly By welcomeBox = By.ClassName("well");
        private readonly By logoBackground = By.ClassName("logo-background");
        public Dashboard()
        {
            page_url = "https://smartuni.azurewebsites.net/";
            driver = new DriverFactory().Create();
            driver.Navigate().GoToUrl(page_url);
            driver.Navigate().Refresh();

            headerBar = new Headerbar(driver);
            sidebar = new Sidebar(driver);
            pageContent = driver.FindElement(By.ClassName("page-content"));
        }

        public void Dispose()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }

        [Fact]
        public void HeaderBar_ShouldShowLogoTitle()
        {
            headerBar.FindLogoTitle();
        }

        [Fact]
        public void HeaderBar_ShouldShowLanguageOptions()
        {
            headerBar.FindLanguageOptions();
        }

        [Fact]
        public void SideBar_ShouldShowSidebar()
        {
            sidebar.FindSidebar();
        }

        [Fact]
        public void SideBar_ShouldShowNavigationItems()
        {
            sidebar.FindNavListItems();
        }

        [Fact]
        public void Body_ShouldShowWelcomeBox()
        {
            Assert.Equal("欢迎", pageContent.FindElement(H1).GetAttribute("innerText"));

            var welcomeBoxItem = pageContent.FindElement(welcomeBox);
            Assert.Equal("  从左侧面板中选择以开始", welcomeBoxItem.FindElement(By.TagName("h4")).Text);
            Assert.Equal("如果您遇到任何问题，请联系Kah Wai。", welcomeBoxItem.FindElement(By.TagName("span")).GetAttribute("innerText"));
        }

        [Fact]
        public void Body_ShouldShowBackgroundLogo()
        {
            Assert.Contains("logo.png", pageContent.FindElement(logoBackground).GetAttribute("src"));
        }
    }
}
