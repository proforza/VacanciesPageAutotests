using NUnit.Framework;
using VacanciesPageAutotests.Pages;

namespace VacanciesPageAutotests
{
    [TestFixture]
    public class VacanciesCountTests : BaseTest
    {
        private string Department { get; set; }
        private string Language { get; set; }
        private int ExpectedVacanciesCount { get; set; }

        [SetUp]
        public void VacanciesCountTestsSetup()
        {
            webDriver.Navigate().GoToUrl(TestContext.Parameters["VacanciesPageTests.Url"]);
            // Parametrization of input values by using .runsettings config file
            Department = TestContext.Parameters["VacanciesPageTests.Department"];
            Language = TestContext.Parameters["VacanciesPageTests.Language"];
            // Assume that params will be correct, no additional validations or trycatch block here
            ExpectedVacanciesCount = int.Parse(TestContext.Parameters["VacanciesPageTests.ExpectedVacanciesCount"]);
        }

        // Basically this test could be divided by 3 tests, to have only single assertion per test
        // But I guess that it has no sense in this case, as far as we want to test vacancies count, but not the behaviour of UI elements   
        [Test]
        public void SelectDepartment_Then_SelectLanguage_And_CountVacancies()
        {
            VacanciesPage vacanciesPage = new(webDriver);

            #region selecting department
            vacanciesPage.SelectDepartment(Department);

            Assert.That(vacanciesPage.IsDepartmentSelected(Department));
            #endregion

            #region selecting languages
            // We can specify multiple languages divided by ','
            var languages = Language.Split(",");

            vacanciesPage.SelectLanguages(languages);

            foreach (var lang in languages)
                Assert.That(vacanciesPage.IsLanguageSelected(lang));
            #endregion

            #region count vacancies
            Assert.That(vacanciesPage.GetVacanciesCount == ExpectedVacanciesCount);
            #endregion
        }
        //[Test]
        //public void Click_On_ResetAllButton()
        //{
        //    webDriver.FindElement(By.XPath("//div[@class='form-group'][last()]//button")).Click();
        //}
    }
}
