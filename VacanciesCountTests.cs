using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VacanciesPageAutotests
{
    [TestFixture]
    public class VacanciesCountTests
    {
        private TestContext TestContext { get; set; }
        private IWebDriver webDriver;
        private string Department { get; set; }
        private string Language { get; set; }
        private int ExpectedVacanciesCount { get; set; }

        [SetUp]
        public void VacanciesCountTestsSetup()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            webDriver.Navigate().GoToUrl(TestContext.Parameters["BaseUrl"]);
            webDriver.Manage().Window.Maximize();

            // Parametrization of input values by using .runsettings config file
            Department = TestContext.Parameters["Department"];
            Language = TestContext.Parameters["Language"];
            // Assume that params will be correct, no additional validations or trycatch block here
            ExpectedVacanciesCount = int.Parse(TestContext.Parameters["ExpectedVacanciesCount"]);
        }

        // Basically this test could be divided by 3 tests, to have only single assertion per test
        // But I guess that it has no sense in this case, as far as we want to test vacancies count, but not the behaviour of UI elements   
        [Test]
        public void SelectDepartment_Then_SelectLanguage_And_CountVacancies()
        {
            #region selecting department
            webDriver.FindElement(By.XPath("//div[@class='form-group'][2]")).Click();
            webDriver.FindElement(By.XPath($"//a[. = '{Department}']")).Click();
            var selectedDepartment = webDriver.FindElement(By.XPath("//button[contains(@class, 'selected')][1]")).Text; // maybe not the best way to locate this element

            Assert.That(selectedDepartment == Department);
            #endregion

            #region selecting languages
            webDriver.FindElement(By.XPath("//div[@class='form-group'][3]")).Click();
            // We can specify multiple languages divided by ','
            foreach (var lang in Language.Split(","))
            {
                var langLabel = webDriver.FindElement(By.XPath($"//label[. = '{lang}']"));
                var langID = langLabel.GetAttribute("for");
                langLabel.Click();
                var langIsSelected = webDriver.FindElement(By.Id(langID)).Selected;
                
                Assert.That(langIsSelected == true);
            }
            #endregion

            #region count vacancies
            var allVacancies = webDriver.FindElements(By.XPath("//a[@data-vacancy]"));

            Assert.That(allVacancies.Count == ExpectedVacanciesCount);
            #endregion
        }

        [TearDown]
        public void VacanciesCountTestsTearDown()
        {
            webDriver.Quit();
        }
    }
}
