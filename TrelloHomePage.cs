using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Linq;
using System.Threading;

namespace AutoDemo
{
    public class TrelloHomePage
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//h1[@class='js-board-editing-target board-header-btn-text']")]
        private IWebElement pageHeader;

        [FindsBy(How = How.XPath, Using =
            "//*[@class='list-header-name mod-list-name js-list-name-input' and text()='To Do']")]
        private IWebElement toDoCol;

        [FindsBy(How = How.XPath, Using =
            "//*[@class='list-header-name mod-list-name js-list-name-input' and text()='Working']")]
        private IWebElement workingCol;

        [FindsBy(How = How.XPath, Using =
            "//*[@class='list-header-name mod-list-name js-list-name-input' and text()='Done']")]
        private IWebElement doneCol;

        [FindsBy(How = How.XPath, Using = "//span[text()='Add a card']")]
        private IWebElement addCardSelectorToDo;

        [FindsBy(How = How.XPath, Using = "//*[@id='board']/div[1]/div/div[2]/div/div[1]/div/textarea")]
        private IWebElement addTextToDo;

        [FindsBy(How = How.XPath, Using = "//*[@id='board']/div[1]/div/div[2]/div/div[2]/div[1]/input")]
        private IWebElement addTextButToDo;

        public TrelloHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            // Make Selenium driver wait for page to load.
            GetPageHeader();
        }

        public string GetPageHeader()
        {
            return pageHeader.Text;
        }

        public bool ValidateWorkColumns()
        {
            bool result = false;

            if (toDoCol.Text == "To Do" && workingCol.Text == "Working" && doneCol.Text == "Done")
            {
                result = true;
            }
            return result;
        }

        public void AddCardToToDoCol(string title)
        {
            addCardSelectorToDo.Click();
            addTextToDo.SendKeys(title);
            addTextButToDo.Click();
        }

        public void AddDetailedDescriptionToCard(string cardName, string description)
        {
            var mainWindowHandle = driver.CurrentWindowHandle;
            var card = driver.FindElement(By.XPath("//*[@id='board']/div[1]//span[text()='" + cardName + "']"));
            card.Click();

            // Switch to child window.
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            // Add the detailed description.
            IWebElement detailedDescriptionTextArea;
            try
            {
                detailedDescriptionTextArea =
                driver.FindElement(By.XPath("//textarea[@placeholder='Add a more detailed description…']"));
            }
            catch(ElementNotInteractableException)
            {
                // Using the same card name in our test causes us to set the same detailed
                //  description card, which blows up the test. Get around this by aborting
                // until we find time for an effective solution.
                driver.SwitchTo().Window(mainWindowHandle);
                return;
            }
            detailedDescriptionTextArea.SendKeys(description);

            // Get Save button and save description.
            var saveButton = driver.FindElement(By.XPath("//input[@value='Save']"));
            saveButton.Click();

            // Switch back to main page.
            driver.SwitchTo().Window(mainWindowHandle);
        }

        public bool ValidateNewCardAddedToToDo(string cardName)
        {
            bool result = false;

            var newCard = toDoCol.FindElement(By.XPath("//span[contains(., '" + cardName + "')]"));
            var name = newCard.Text;

            if (name == cardName)
            {
                result = true;
            }
            return result;
        }
    }
}
