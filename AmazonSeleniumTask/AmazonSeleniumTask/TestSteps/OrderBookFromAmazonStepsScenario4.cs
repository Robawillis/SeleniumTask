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

namespace AmazonSeleniumTask.TestSteps
{
    [Binding]
    public class OrderBookFromAmazonStepsScenario4
    {
        public IWebDriver chDriver;
        public IWebElement bookDetails;
        public IWebElement bookType;
       
        [Given(@"the books details are correct")]
        public void GivenTheBooksDetailsAreCorrect()
        {
            chDriver = FeatureContext.Current["chromeDriver"] as IWebDriver;
            bookDetails = FeatureContext.Current["bookDetails"] as IWebElement;

            IWebElement bookTitle = bookDetails.FindElement(By.Id("productTitle"));

            Assert.AreEqual(bookTitle.Text, "A Game of Thrones (A Song of Ice and Fire, Book 1)");

            IWebElement bookOptions = bookDetails.FindElement(By.Id("tmmSwatches"));

            IWebElement bookOptionsList = bookOptions.FindElement(By.TagName("ul"));
            IList<IWebElement> listItems = bookOptionsList.FindElements(By.TagName("li"));
            foreach (IWebElement item in listItems)
            {
                String itemType = item.Text;
                if (itemType.Contains("Paperback"))
                {
                    bookType = item;
                    break;
                }
            }
            // IWebElement bookTypePrice = bookType.FindElement(By.ClassName("a-size-base a-color-price a-color-price"));
            IWebElement bookTypePrice = bookType.FindElement(By.XPath("//*[@id='a-autoid-4-announce']/span[2]/span"));
            Assert.AreEqual(bookTypePrice.Text, "£4.00");

        }
        
        [Given(@"I have the paperback option selected")]
        public void GivenIHaveThePaperbackOptionSelected()
        {
            string classType = bookType.GetAttribute("class");
            if (!classType.Contains("selected"))
            {
                Assert.Fail();
            }
            

        }
        
        [When(@"I click on the ""(.*)"" option")]
        public void WhenIClickOnTheOption(string p0)
        {
            chDriver.FindElement(By.Id("add-to-cart-button")).Click();
        }
        
        [Then(@"I expect to see the ""(.*)"" notification page")]
        public void ThenIExpectToSeeTheNotificationPage(string p0)
        {
            IWebElement checkoutDetails = chDriver.FindElement(By.Id("huc-v2-order-row-inner"));

            Assert.IsNotNull(checkoutDetails);

            ITakesScreenshot screenshotChDriver = chDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotChDriver.GetScreenshot();
            String executeDirectory = FeatureContext.Current["executeDirectory"] as String;

            screenshot.SaveAsFile(executeDirectory + ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", string.Empty) + ".jpg", ScreenshotImageFormat.Jpeg);

            FeatureContext.Current["checkoutDetails"] = checkoutDetails; 
        }
    }
}
