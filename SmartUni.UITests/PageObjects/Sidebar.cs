using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SmartUni.UITests.PageObjects;
using Xunit;

namespace SmartUni.UITests.PageObjects
{
    public class Sidebar
    {
        private readonly string page_url;
        private readonly IWebDriver _driver;
        By sidebar = By.Id("sidebar");
        By navList = By.CssSelector("ul[class='nav nav-list']");

        public Sidebar(IWebDriver driver)
        {
            _driver = driver;
            page_url = "https://smartuni.azurewebsites.net/";
        }

        public void FindSidebar()
        {
            _driver.FindElement(sidebar);
        }

        public void FindNavListItems()
        {
            String[] navTitle = { "主页", "教师", "班级", "科目", "学生", "考试" };
            String[][] navItems = {
                new String[] { "新增教师", "管理教师", "管理设置" },
                new String[] { "新增班级", "管理班级", "管理设置" },
                new String[] { "新增科目", "管理科目" },
                new String[] { "新增学生", "管理学生", "管理设置" },
                new String[] { "新增考试", "管理考试" }
            };
            String[][] navUrls = {
                new String[] { "/Tutors/Create", "/Tutors", "/Tutors/Settings" },
                new String[] { "/Classes/Create", "/Classes", "/Classes/Settings" },
                new String[] { "/Subjects/Create", "/Subjects" },
                new String[] { "/Students/Create", "/Students", "/Students/Settings" },
                new String[] { "/Exams/Create", "/Exams" }
            };

            var navTabs = _driver.FindElement(navList).FindElements(By.Name("navTab"));
            Assert.True(navTabs.Count().Equals(6));

            var navLink0 = navTabs[0].FindElement(By.TagName("a"));
            Assert.Equal(page_url, navLink0.GetAttribute("href"));
            Assert.Equal(navTitle[0], navLink0.FindElement(By.ClassName("menu-text")).GetAttribute("innerText"));

            for ( int index = 1; index < 5; index++ )
            {
                var navTabMain = navTabs[index].FindElement(By.TagName("a"));
                var navTabTitle = navTabMain.FindElement(By.ClassName("menu-text"));
                Assert.Equal(navTitle[index], navTabTitle.GetAttribute("innerText"));

                var navListItems = navTabs[index].FindElement(By.Name("navList")).FindElements(By.TagName("li"));
                Assert.Equal(navListItems.Count(), navItems[index-1].Length);

                for ( int itemIndex = 0; itemIndex < navListItems.Count(); itemIndex++ )
                {
                    Assert.Equal(
                            navItems[index-1][itemIndex],
                            navListItems[itemIndex]
                                .FindElement(By.ClassName("menu-text"))
                                .GetAttribute("innerText")
                    );
                    Assert.Contains(
                        navUrls[index-1][itemIndex],
                        navListItems[itemIndex]
                            .FindElement(By.TagName("a"))
                            .GetAttribute("href")
                   );
                }
            }
        }
    }
}
