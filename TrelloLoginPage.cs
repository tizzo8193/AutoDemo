using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;

namespace AutoDemo
{
    // Page object model to interact with Trello login page.
    public class TrelloLoginPage
    {
        private IWebDriver driver;
        private const string userName = "tizzo1370@gmail.com";
        private const string password = "TomTr@llo";

        [FindsBy(How = How.Id, Using = "user")]
        private IWebElement userNameTextBox;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordTextBox;

        [FindsBy(How = How.Id, Using = "login")]
        private IWebElement loginButton;

        public TrelloLoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Login to our Trello page.
        public void Login()
        {
            try
            {
                userNameTextBox.SendKeys(userName);
                passwordTextBox.SendKeys(password);

                loginButton.Click();
            }
            catch(Exception ex)
            {
                driver.Dispose();

                // Brief message about what went wrong.
                Assert.Fail($"Exception loging in: {ex.Message}");
            }
        }
    }
}
