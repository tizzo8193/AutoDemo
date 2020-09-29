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
    // Page object model to interact with Trello landing page.
    public class TrelloLandingPage
    {
        private IWebDriver driver;
        private string landingPageUrl =
            "https://trello.com/invite/b/xWAjppYF/873a1701c16b38d144556c2e3e8bbb01/sdet-test-board-tom-izzo";

        [FindsBy(How = How.XPath, Using = "//*[@class='button js-login']")]
        [CacheLookup] // if we want to cache the web object for performance.
        private IWebElement landingLoginButton;

        public TrelloLandingPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Logs into landing page.
        public void Login()
        {
            driver.Navigate().GoToUrl(landingPageUrl);
            landingLoginButton.Click();
        }
    }
}
