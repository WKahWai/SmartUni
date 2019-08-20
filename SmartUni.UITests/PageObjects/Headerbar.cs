using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SmartUni.UITests.PageObjects;
using Xunit;

namespace SmartUni.UITests.PageObjects
{
    public class Headerbar
    {
        private readonly IWebDriver _driver;

        public static By logoTitle = By.ClassName("navbar-brand");
        public static By languageOption = By.CssSelector("ul[class='nav ace-nav']");

        public Headerbar(IWebDriver driver)
        {
            _driver = driver;
        }

        public void FindLogoTitle()
        {
            _driver.FindElement(logoTitle);
        }

        public void FindLanguageOptions()
        {
            var languageOptions = _driver.FindElement(languageOption).FindElements(By.TagName("a"));
            Assert.True(languageOptions.Count().Equals(2));
            Assert.Equal("English", languageOptions[0].GetAttribute("innerText"));
            Assert.Equal("简中", languageOptions[1].GetAttribute("innerText"));
        }
    }
}
