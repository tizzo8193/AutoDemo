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
        public void CreateNewCardInToDoTest()
        {
            IWebDriver driver = new ChromeDriver();
            // Be sloppy for demo and just wait a long time for elements to load.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(25);

            // Login.
            Login(driver);

            // Create home page model.
            var homePage = new TrelloHomePage(driver);

            // Are we logged in, and as the right person?
            Assert.IsTrue(driver.Url.Contains("sdet-test-board-tom-izzo"), "Failed to login.");

            // Validate we have (3) work columns.
            bool validated = homePage.ValidateWorkColumns();
            Assert.IsTrue(validated, "Not confirmed that (3) work columns rendered on home page.");

            //Add new card and detailed description to To Do column.
            homePage.AddCardToToDoCol("new card");
            homePage.AddDetailedDescriptionToCard("new card", "this is a new card");

            // Validate that new card is displayed on home page.
            var cardFound = homePage.ValidateNewCardAddedToToDo("new card");
            Assert.IsTrue(cardFound, "Did not find new card in To Do column on home page.");

            driver.Dispose();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Clean up after ourselves.

            // TODO: Loop through all active Windows processes and kill any orphaned 
            // Selenium chrome driver processes still hanging around.
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
