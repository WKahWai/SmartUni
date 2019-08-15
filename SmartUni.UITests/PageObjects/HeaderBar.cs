using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SmartUni.UITests.PageObjects
{
    public class HeaderBar
    {
        private IWebDriver _driver;

        public HeaderBar(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool GetLogoTitle()
        {
            return _driver.FindElement(By.ClassName("navbar-brand")).Displayed;
        }
    }
}
