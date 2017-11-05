using System;
using TechTalk.SpecFlow;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace AmazonSeleniumTask
{
    [Binding]
    public class OrderBookFromAmazonStepsScenario3
    {

        public IWebDriver chDriver;
        public IWebElement resultsList;
        public IWebElement targetBook = null;

        [Given(@"I Have the amazon results page")]
        public void GivenIHaveTheAmazonResultsPage()
        {
            chDriver = FeatureContext.Current["chromeDriver"] as IWebDriver;
            resultsList = FeatureContext.Current["bookResults"] as IWebElement;
        }
        
        [Given(@"the book on test is present in the results list")]
        public void GivenTheBookOnTestIsPresentInTheResultsList()
        {
            IWebElement bookList = resultsList.FindElement(By.Id("s-results-list-atf"));
            IList<IWebElement> listItems = bookList.FindElements(By.TagName("li"));
           
            foreach (IWebElement option in listItems)
            {
                String bookTitle = option.GetAttribute("textContent").ToString();
                if (bookTitle.Contains("A Game of Thrones (A Song of Ice and Fire, Book 1)"))
                {
                    targetBook = option;
                    break;
                 }
            }
           Assert.IsNotNull(targetBook.FindElement(By.PartialLinkText("Paperback")));
            IWebElement bookPrice = targetBook.FindElement(By.XPath("//*[@id='result_0']/div/div/div/div[2]/div[2]/div[1]/div[2]/a/span[2]"));
            Assert.IsNotNull(bookPrice);
            Assert.AreEqual(bookPrice.Text, "£4.00");
        }
        
        [When(@"I click on the item in the results list")]
        public void WhenIClickOnTheItemInTheResultsList()
        {
            targetBook.FindElement(By.PartialLinkText("Paperback")).Click();
        }
        
        [Then(@"I expect to See the books details")]
        public void ThenIExpectToSeeTheBooksDetails()
        {
            IWebElement bookDetails = chDriver.FindElement(By.Id("dp-container"));

            Assert.IsNotNull(bookDetails);

            ITakesScreenshot screenshotChDriver = chDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotChDriver.GetScreenshot();
            String executeDirectory = FeatureContext.Current["executeDirectory"] as String;

            screenshot.SaveAsFile(executeDirectory + ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", string.Empty) + ".jpg", ScreenshotImageFormat.Jpeg);

            FeatureContext.Current["bookDetails"] = bookDetails;
        }
    }
}
