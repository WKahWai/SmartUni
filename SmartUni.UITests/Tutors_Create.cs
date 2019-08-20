using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using SmartUni.UITests.PageObjects;
using SmartUni.UITests.SeleniumHelpers;
using Xunit;

namespace SmartUni.UITests
{

    public class Tutors_Create : IDisposable
    {
        private readonly string base_url;
        private readonly string path;
        private readonly IWebDriver driver;
        private Headerbar headerBar;
        private Sidebar sidebar;
        private IWebElement pageContent;
        private readonly string[] fields = { "TutorName", "Email", "PhoneNo", "TutorStatusId", "TutorTypeId" };

        public Tutors_Create()
        {
            base_url = "https://smartuni.azurewebsites.net/";
            path = "Tutors/Create";
            driver = new DriverFactory().Create();
            driver.Navigate().GoToUrl(base_url + path);
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
        public void Body_ShouldRenderHeading()
        {
            Assert.Equal("新增", pageContent.FindElement(By.TagName("h2")).GetAttribute("innerText"));
            Assert.Equal("教师", pageContent.FindElement(By.TagName("h4")).GetAttribute("innerText"));
        }

        [Fact]
        public void Body_ShouldRenderForm()
        {
            String[] labels = { "教师名字", "电邮地址", "联络号码", "教师状态", "教师种类", "新增" };

            var elements = pageContent.FindElements(By.ClassName("form-group"));
            for (int index = 0; index < 5; index++)
            {
                Assert.Equal(labels[index], elements[index].FindElement(By.TagName("label")).GetAttribute("innerText"));
                if (index == 3 || index == 4)
                {
                    Assert.NotNull(elements[index].FindElement(By.TagName("select")));
                    Assert.Equal(fields[index], elements[index].FindElement(By.TagName("select")).GetAttribute("id"));
                } else
                {
                    Assert.NotNull(elements[index].FindElement(By.TagName("input")));
                    Assert.Equal(fields[index], elements[index].FindElement(By.TagName("input")).GetAttribute("id"));
                    Assert.NotNull(elements[index].FindElement(By.TagName("span")));
                }
            }

            Assert.Equal(labels[5], elements[5].FindElement(By.TagName("input")).GetAttribute("value"));
        }

        [Fact]
        public void Body_ShouldRenderBackToListButton()
        {
            var element = pageContent.FindElement(By.TagName("a"));
            Assert.Equal(base_url + "Tutors", element.GetAttribute("href"));
            Assert.Equal("回到列表", element.GetAttribute("innerText"));
        }

        [Fact]
        public void Body_ShouldRenderErrorMsgWhenSubmitWithoutData()
        {
            String[] errorMsg = { "教师名字 字段是必需的。", "电邮地址 字段是必需的。", "联络号码 字段是必需的。" };

            IWebElement btnSubmit = pageContent.FindElement(By.TagName("form")).FindElement(By.XPath("//*[@type='submit']"));
            btnSubmit.Click();

            for (int index = 0; index < 3; index++)
            {
                Assert.Equal(errorMsg[index], pageContent.FindElement(By.Id(fields[index] + "-error")).GetAttribute("innerText"));
            }
        }

        [Theory]
        [InlineData("Kah Wai", "kw@kw.com",11234567890)]
        public void Body_ShouldRenderErrorMsgWhenSubmitWithCorrectData(string tutorName, string email, Int64 phoneNo)
        {
            pageContent.FindElement(By.Id(fields[0])).SendKeys(tutorName);
            pageContent.FindElement(By.Id(fields[1])).SendKeys(email);
            pageContent.FindElement(By.Id(fields[2])).SendKeys(phoneNo.ToString());

            IWebElement btnSubmit = pageContent.FindElement(By.TagName("form")).FindElement(By.XPath("//*[@type='submit']"));
            btnSubmit.Click();
        }

        [Theory]
        [InlineData("Kah Wai", "kw.com", "www")]
        public void Body_ShouldRenderErrorMsgWhenSubmitWithIncorrectData(string tutorName, string email, string phoneNo)
        {
            String[] errorMsg = { "", "无效的电邮地址", "联系号码必须在10-11位数之内。" };

            pageContent.FindElement(By.Id(fields[0])).SendKeys(tutorName);
            pageContent.FindElement(By.Id(fields[1])).SendKeys(email);
            pageContent.FindElement(By.Id(fields[2])).SendKeys(phoneNo);

            IWebElement btnSubmit = pageContent.FindElement(By.TagName("form")).FindElement(By.XPath("//*[@type='submit']"));
            btnSubmit.Click();

            for (int index = 1; index < 3; index++)
            {
                Assert.Equal(errorMsg[index], pageContent.FindElement(By.Id(fields[index] + "-error")).GetAttribute("innerText"));
            }
        }
    }
}
