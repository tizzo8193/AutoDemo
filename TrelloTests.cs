using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using System;

namespace AutoDemo
{
    [TestClass]
    public class TrelloTests
    {
        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
        }

        [TestMethod]
        public void CreateNewCardInToDo()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            Login(driver);

            driver.Dispose();
        }

        private void Login(IWebDriver driver)
        {
            var landingPage = new TrelloLandingPage(driver);
            landingPage.Login();

            var loginPage = new TrelloLoginPage(driver);
            loginPage.Login();
        }
    }
}
