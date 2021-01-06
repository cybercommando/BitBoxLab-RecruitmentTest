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

                foreach (var item in data)
                {
                    Console.WriteLine(item.Text);
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
