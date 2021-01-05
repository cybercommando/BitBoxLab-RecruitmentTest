using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingSession.Services
{
    public class ConnectionService
    {
        private static string RouterUrl = "http://192.168.2.1";
        private readonly IWebDriver driver;

        public ConnectionService(IWebDriver _d)
        {
            this.driver = _d;
            this.driver.Url = RouterUrl;
        }

        
        public void PerformLogin(string Username,string Password)
        {
            driver.FindElement(By.Id("inputEmail")).Clear();
            driver.FindElement(By.Id("inputEmail")).SendKeys(Username);

            driver.FindElement(By.Id("inputPassword")).Clear();
            driver.FindElement(By.Id("inputPassword")).SendKeys(Password);

            ClickAndWaitForPageToLoad(driver, By.ClassName("btn"));
        }

        private void ClickAndWaitForPageToLoad(IWebDriver driver, By elementLocator, int timeout = 1000)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var elements = driver.FindElements(elementLocator);

                if (elements.Count == 0)
                {
                    throw new NoSuchElementException(
                        "Exception: No Elements " + elementLocator + " ClickAndWaitForPageToLoad");
                }
                var element = elements.FirstOrDefault(e => e.Displayed);
                element.Click();
                wait.Until(ExpectedConditions.StalenessOf(element));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    "Exception: Element with locator: '" + elementLocator + "' was not found");
                throw;
            }
        }

        public IEnumerable<IWebElement> fetchStatusPage()
        {
            driver.Navigate().GoToUrl(RouterUrl + "/#!/wireless-status");
            return driver.FindElements(By.ClassName("Panel"));
        }
    }
}
