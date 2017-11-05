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
    public class OrderBookFromAmazonStepsScenario5
    {
        public IWebDriver chDriver;
        public IWebElement checkoutDetails;
        public IWebElement bookType;

        [Given(@"I am on the basket notification page")]
        public void GivenIAmOnTheBasketNotificationPage()
        {
            chDriver = FeatureContext.Current["chromeDriver"] as IWebDriver;
            checkoutDetails = FeatureContext.Current["checkoutDetails"] as IWebElement;

            IWebElement checkoutMessage = checkoutDetails.FindElement(By.Id("huc-v2-order-row-confirm-text"));

            Assert.AreEqual(checkoutMessage.Text, "Added to Basket");

            //    IWebElement ordered_item = checkoutDetails.FindElement(By.TagName("img"));

            //  Assert.IsNotNull(ordered_item);

            IList<IWebElement> checkoutDetailsText = checkoutDetails.FindElements(By.TagName("span"));
            Boolean itemNotAdded = true;
            foreach (IWebElement textContents in checkoutDetailsText)
            {
                String contents = textContents.Text;
                if (contents.Contains("(1 item)"))
                {
                    itemNotAdded = false;
                   
                }
            }
            if (itemNotAdded)
            {
                Assert.Fail();
            }

        }
        
        [When(@"I click on the ""(.*)"" button")]
        public void WhenIClickOnTheButton(string p0)
        {
            chDriver.FindElement(By.XPath("//*[@id='hlb-view-cart-announce']")).Click();
        }

        [Then(@"I expect to see the Shopping basket page with the item present, quantity and subtotal correct")]
        public void ThenIExpectToSeeTheShoppingBasketPageWithTheItemPresentQuantityAndSubtotalCorrect()
        {
            IWebElement activeCart = chDriver.FindElement(By.Id("activeCartViewForm"));
            Assert.NotNull(activeCart);

            IWebElement cartItemDetail = activeCart.FindElement(By.TagName("ul"));
            IList<IWebElement> itemDetails = cartItemDetail.FindElements(By.TagName("li"));
            Boolean titleFound = false;
            Boolean typeFound = false;
            foreach (IWebElement detail in itemDetails)
            {
                String detailContents = detail.Text;
                if (detailContents.Contains("A Game of Thrones (A Song of Ice and Fire, Book 1) "))
                {
                    titleFound = true;
                } else if (detailContents.Contains("Paperback"))
                {
                    typeFound = true;
                }

                if (titleFound && typeFound)
                {
                    break;
                }
            }

            Assert.True(typeFound);
            Assert.True(titleFound);

            IWebElement itemPrice = activeCart.FindElement(By.XPath("//*[@id='activeCartViewForm']/div[2]/div/div[4]/div/div[2]/p/span"));
            Assert.AreEqual(itemPrice.Text, "£4.00");

            IWebElement itemQuantity = activeCart.FindElement(By.TagName("select"));
            IList<IWebElement> quantityOptions = itemQuantity.FindElements(By.TagName("option"));
            foreach (IWebElement option in quantityOptions) {
                if (option.Selected)
                {
                    string optionText = option.Text;
                    Assert.AreEqual(optionText.Trim(), "1");
                }
            }
            IWebElement subTotal = chDriver.FindElement(By.Id("sc-subtotal-amount-activecart"));
            Assert.AreEqual(subTotal.Text, itemPrice.Text);

            ITakesScreenshot screenshotChDriver = chDriver as ITakesScreenshot;
            Screenshot screenshot = screenshotChDriver.GetScreenshot();
            String executeDirectory = FeatureContext.Current["executeDirectory"] as String;

            screenshot.SaveAsFile(executeDirectory + ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", string.Empty) + ".jpg", ScreenshotImageFormat.Jpeg);

        }

        [AfterFeature]
        public static void After()
        {

            IWebDriver chDriver = FeatureContext.Current["chromeDriver"] as IWebDriver;
            chDriver.Close();
            chDriver.Quit();


            /*    foreach (var process in Process.GetProcessesByName("chromedriver"))
                     {
                    process.Kill();
                } */

        }
    }
}
