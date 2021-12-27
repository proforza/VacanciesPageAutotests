using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VacanciesPageAutotests
{
    public class BaseTest
    {
        public IWebDriver webDriver;

        [SetUp]
        public void BaseTestSetUp()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void BaseTestTearDown()
        {
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Quit();
        }
    }
}
