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
    public class OrderBookFromAmazonStepsScenario2
    {

        public IWebDriver chDriver;
        public WebDriverWait waitPage;
        public String executeDirectory;
        public IWebElement navBar;

        [Given(@"I enter ""(.*)"" in Amazon's Search bar")]
        public void GivenIEnterInAmazonSSearchBar(string p0)
        {
            chDriver = FeatureContext.Current["chromeDriver"] as IWebDriver;
            navBar = FeatureContext.Current["navsearch"] as IWebElement;

            IWebElement searchTextBox = navBar.FindElement(By.Id("twotabsearchtextbox"));
            searchTextBox.SendKeys(p0);
        }
        
        [Given(@"I select the ""(.*)"" option in the Amazon Search bar category")]
        public void GivenISelectTheOptionInTheAmazonSearchBarCatoegory(string p0)
        {
            IWebElement searchCategory = navBar.FindElement(By.Id("searchDropdownBox"));
            IList<IWebElement> categoryOptions = searchCategory.FindElements(By.TagName("option"));

            foreach (IWebElement option in categoryOptions)
            {
              //  String value = option.GetAttribute("text").ToString();
                if ((option.GetAttribute("text").ToString() == "Books")){
                    option.Click();
                }
            }
        }
        
        [When(@"I click on the search button")]
        public void WhenIClickOnTheSearchButton()
        {
            //  IWebElement searchButton = chDriver.FindElement(By.Id("nav-search-submit-text"));
            //   searchButton.FindElement(By.CssSelector("nav-input")).Click();
            navBar.FindElement(By.ClassName("nav-input")).Click();

        }
        
        [Then(@"I expect to see Amazon's search results page with relevant search results")]
        public void ThenIExpectToSeeAmazonSSearchResultsPageWithRelevantSearchResults()
        {
            IWebElement resultsList = chDriver.FindElement(By.Id("resultsCol"));

            Assert.IsNotNull(resultsList);

            ITakesScreenshot screenshotChDriver = chDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotChDriver.GetScreenshot();
            executeDirectory = FeatureContext.Current["executeDirectory"] as String;

            screenshot.SaveAsFile(executeDirectory + ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", string.Empty) + ".jpg", ScreenshotImageFormat.Jpeg);

            FeatureContext.Current["bookResults"] = resultsList;
        }
    }
}
