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
                
                Console.Clear();

                while (true)
                {
                    Console.WriteLine("--------------------------------------------------------------------");
                    var data = connectionService.GetLiveDeviceList();

                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", "IP-Address", "MAC-Address", "Time (ms)", "Device Status");
                    foreach (var item in data)
                    {
                        Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", item.IpAddress, item.MacAddress, item.Time, item.status);
                    }
                    System.Threading.Thread.Sleep(60000);
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
