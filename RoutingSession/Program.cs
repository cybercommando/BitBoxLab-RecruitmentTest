using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RoutingSession.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingSession
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initiating...!!!");
            var webDriver = LaunchBrowser();
            //Code
            //=========================================================
            try
            {
                var connectionService = new ConnectionService(webDriver);
                System.Threading.Thread.Sleep(1000);

                connectionService.PerformLogin("user", "tartu7777");
                System.Threading.Thread.Sleep(1000);

                var data = connectionService.fetchStatusPage();

                //Status Page Processing
                foreach (var item in data)
                {
                    Console.WriteLine("================================================");
                    foreach (var i in item.FindElements(By.XPath(".//tr")))
                    {
                        if (i.GetAttribute("innerHTML").ToString().Contains("IP-Address"))
                        {
                            var tempData = i.FindElements(By.XPath(".//td//div"));
                            Console.WriteLine("{0}:\t{1}", tempData[0].GetAttribute("innerHTML"), tempData[1].GetAttribute("innerHTML"));

                            //Console.WriteLine(i.GetAttribute("innerHTML"));
                        }
                        else if (i.GetAttribute("innerHTML").ToString().Contains("MAC-Address"))
                        {
                            var tempData = i.FindElements(By.XPath(".//td//div"));
                            Console.WriteLine("{0}:\t{1}", tempData[0].GetAttribute("innerHTML"), tempData[1].GetAttribute("innerHTML"));

                        }
                        else if (i.GetAttribute("innerHTML").ToString().Contains("In Network"))
                        {
                            var tempData = i.FindElements(By.XPath(".//td//div"));
                            Console.WriteLine("{0} (Time in Seconds):\t{1}", tempData[0].GetAttribute("innerHTML"), tempData[1].GetAttribute("innerHTML"));
                        }
                        //Console.WriteLine(i.GetAttribute("innerHTML"));
                    }
                    Console.WriteLine("================================================");
                    //Console.WriteLine(item.GetAttribute("innerHTML"));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
            }
            finally
            {
                webDriver.Quit();
            }

            Console.WriteLine("\nDone...");
            //=========================================================

            Console.ReadKey();
        }

        static IWebDriver LaunchBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximised");
            options.AddArgument("--disable-notifications");

            var driver = new ChromeDriver(Environment.CurrentDirectory, options);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(5);
            return driver;
        }
    }
}
