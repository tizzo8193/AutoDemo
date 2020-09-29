using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

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
            userNameTextBox.SendKeys(userName);
            passwordTextBox.SendKeys(password);

            loginButton.Click();
        }
    }
}
