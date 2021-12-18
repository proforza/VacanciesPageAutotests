# About
This autotest was created as a part of a test task.

Framework used: .NET5, nUnit, Selenium.

## Installation

Clone this repo locally, open a cmd/powershell window from the directory that contains VacanciesPageAutotests.csproj and finally run dotnet command (.NET5 SDK must be installed):

```bash
dotnet test VacanciesPageAutotests.csproj -c Release -s VacanciesPageAutotests.runsettings -r TestResults --verbosity normal --logger trx
```

Result .trx file will appear in the **/TestResults** folder. The most CI/CD tools (TeamCity, Jenkins, Azure DevOps etc.) support this type of file.

## Tests description

  1. Selecting a department and checking that it has been selected successfully
  2. Selecting a language (or multiple languages, divided by ',') and asserting that corresponding checkboxes are checked
  3. Counting the amount of vacancies on the page and checking that they are equal to expected value

## License
[MIT](https://choosealicense.com/licenses/mit/)
