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
                connectionService.PerformLogin("user", "tartu7777");

                //Console.WriteLine(connectionService.fetchStatusPage());
                System.Threading.Thread.Sleep(2000);
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
            return driver;
        }
    }
}
