using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace VacanciesPageAutotests.Pages
{
    public class VacanciesPage
    {
        private IWebDriver webDriver;
        // common page elements
        private IWebElement CookiePopupCloseButton => webDriver.FindElement(By.Id("cookiescript_close"));
        // I guess that this one will be better, because we are not using a magic number, and less likely that placeholder will be changed
        private IWebElement LanguageDropDown => webDriver.FindElement(By.XPath("//button[contains(@class,'dropdown-toggle')][. = 'Все языки']"));
        public VacanciesPage(IWebDriver driver)
        {
            webDriver = driver;
        }

        // we can also make these methods to return page object to be able to use it like this:
        // vacanciesPage.CloseCookiePopup
        //              .SelectDepartment()
        //              .SelectLanguage();  
        //
        // but in this case it's unneccessary

        // public VacanciesPage CloseCookiePopup()
        public void CloseCookiePopup()
        {
            CookiePopupCloseButton.Click();

            //return this;
        }

        public void SelectDepartment(string department)
        {
            GetDepartmentDropDown("Все отделы").Click(); // open dropdown
            GetDepartmentDropDownOption(department).Click(); // select department
        }

        public void SelectLanguages(string[] languages)
        {
            LanguageDropDown.Click(); // open dropdown

            foreach (var lang in languages)
                GetLanguageDropDownOption(lang).Click(); // select language
        }
        public int GetVacanciesCount => webDriver.FindElements(By.XPath("//a[@data-vacancy]")).Count;

        private IWebElement GetDepartmentDropDown(string department) => webDriver.FindElement(By.XPath($"//button[contains(@class,'dropdown-toggle')][. = '{department}']"));
        private IWebElement GetDepartmentDropDownOption(string department) => webDriver.FindElement(By.XPath($"//a[. = '{department}']"));
        private IWebElement GetLanguageDropDownOption(string lang) => webDriver.FindElement(By.XPath($"//label[. = '{lang}']/preceding-sibling::input")); // assume that there are no any other labels with such names
        public bool IsDepartmentSelected(string department) => GetDepartmentDropDown(department).Text == department;
        // never used
        // alternative version of this method using explicit wait. But as I said before - I think that explicit waits are like overkill here
        public bool IsDepartmentSelectedWithExplicitWait(string department) 
        {
            WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(10));
            return wait.Until(driver => driver.FindElement(By.XPath($"//button[contains(@class,'dropdown-toggle')][. = '{department}']")).Displayed);
        }
        public bool IsLanguageSelected(string lang) => GetLanguageDropDownOption(lang).Selected;

    }
}
