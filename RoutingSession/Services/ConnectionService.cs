using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Protractor;
using RoutingSession.Model;
using RoutingSession.Repository;
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
        private DeviceRepository deviceRepository = new DeviceRepository();

        public ConnectionService(IWebDriver _d)
        {
            this.driver = _d;
            this.driver.Url = RouterUrl;
        }

        public void PerformLogin(string Username,string Password)
        {
            this.driver.FindElement(By.Id("inputEmail")).Clear();
            this.driver.FindElement(By.Id("inputEmail")).SendKeys(Username);

            this.driver.FindElement(By.Id("inputPassword")).Clear();
            this.driver.FindElement(By.Id("inputPassword")).SendKeys(Password);

            ClickAndWaitForPageToLoad(this.driver, By.ClassName("btn"));
        }

        private void ClickAndWaitForPageToLoad(IWebDriver driver, By elementLocator, int timeout = 10000)
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

        public IEnumerable<IWebElement> FetchStatusPage()
        {
            driver.Navigate().GoToUrl(RouterUrl + "/#!/wireless-status");
            System.Threading.Thread.Sleep(3000);
            return driver.FindElements(By.XPath(".//table"));
        }

        public IEnumerable<DeviceData> GetCurrentDeviceList()
        {
            var deviceDatas = new List<DeviceData>();

            var deviceList = FetchStatusPage();

            //Status Page Processing
            foreach (var device in deviceList)
            {
                var deviceData = new DeviceData();

                //Console.WriteLine("================================================");
                foreach (var deviceAttr in device.FindElements(By.XPath(".//tr")))
                {
                    if (deviceAttr.GetAttribute("innerHTML").ToString().Contains("IP-Address"))
                    {
                        var tempData = deviceAttr.FindElements(By.XPath(".//td//div"));
                        deviceData.IpAddress = tempData[1].GetAttribute("innerHTML");
                        //Console.WriteLine("{0}:\t{1}", tempData[0].GetAttribute("innerHTML"), tempData[1].GetAttribute("innerHTML"));
                    }
                    else if (deviceAttr.GetAttribute("innerHTML").ToString().Contains("MAC-Address"))
                    {
                        var tempData = deviceAttr.FindElements(By.XPath(".//td//div"));
                        deviceData.MacAddress = tempData[1].GetAttribute("innerHTML");
                        //Console.WriteLine("{0}:\t{1}", tempData[0].GetAttribute("innerHTML"), tempData[1].GetAttribute("innerHTML"));
                    }
                    else if (deviceAttr.GetAttribute("innerHTML").ToString().Contains("In Network"))
                    {
                        var tempData = deviceAttr.FindElements(By.XPath(".//td//div"));
                        deviceData.Time = tempData[1].GetAttribute("innerHTML");
                        //Console.WriteLine("{0} (Time in Seconds):\t{1}", tempData[0].GetAttribute("innerHTML"), tempData[1].GetAttribute("innerHTML"));
                    }
                }
                deviceData.status = "ONLINE";
                deviceDatas.Add(deviceData);
                //Console.WriteLine("================================================");
            }

            return deviceDatas;
        }

        public IEnumerable<DeviceData> GetLiveDeviceList()
        {
            var data = GetCurrentDeviceList();

            if (deviceRepository.SyncDeviceData(data))
            {
                return deviceRepository.GetAllDevice();
            }
            return null;
        }
    }
}
