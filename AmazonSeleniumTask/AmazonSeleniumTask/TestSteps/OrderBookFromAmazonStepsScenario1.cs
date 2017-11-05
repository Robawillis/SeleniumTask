using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace AmazonSeleniumTask
{
    public class ServiceContext
    {
        public IWebDriver chromeDriver { get; set; }
    }

    [Binding]
    public class OrderBookFromAmazonStepsScenario1
    {
        public IWebDriver chDriver;
        public WebDriverWait waitPage;
        public String executeDirectory;

        
        [BeforeFeature]
        public static void SetupEnvironment()
        {
            IWebDriver chDriver = new ChromeDriver();
            chDriver.Manage().Window.Maximize();
            FeatureContext.Current["chromeDriver"] = chDriver;
            String executeTime = DateTime.Now.ToString("yyyyMMdd");

            String executeDirectory = TestContext.CurrentContext.WorkDirectory + "\\screenshots\\";
            FeatureContext.Current["executeDirectory"] = executeDirectory;
            if (Directory.Exists(executeDirectory) == false)
            {
                Directory.CreateDirectory(executeDirectory);
            }

        }
        [Given(@"I enter ""(.*)"" in Chrome's address bar")]
        public void GivenIEnterInChromeSAddressBar(string p0)
        {
            chDriver = FeatureContext.Current["chromeDriver"] as IWebDriver;
            chDriver.Navigate().GoToUrl(p0);

        }

        [When(@"I instruct it to go to the address")]
        public void WhenIInstructItToGoToTheAddress()
        {
            waitPage = new WebDriverWait(chDriver, TimeSpan.FromSeconds(1));
        }
        
        [Then(@"I expect Amazon's home page to display")]
        public void ThenIExpectAmazonSHomePageToDisplay()
        {
            waitPage.Until(chDriver => chDriver.Title.StartsWith("amazon.co.uk", StringComparison.OrdinalIgnoreCase));

            ITakesScreenshot screenshotChDriver = chDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotChDriver.GetScreenshot();
            executeDirectory = FeatureContext.Current["executeDirectory"] as String;

            screenshot.SaveAsFile(executeDirectory + ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", string.Empty) + ".jpg", ScreenshotImageFormat.Jpeg);

            IWebElement navBar = chDriver.FindElement(By.Id("nav-search"));

            Assert.IsNotNull(navBar);

            FeatureContext.Current["navsearch"] = navBar;
        }

       
    }
}
