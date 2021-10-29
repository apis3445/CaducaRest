using System;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V94.Performance;
namespace CaducaRest.UITest
{
    public class Tests : IDisposable
    {
        private  IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            if (Environment.GetEnvironmentVariable("ChromeWebDriver") != null)
                _driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"));
            else
                _driver = new ChromeDriver();
        }
  
        [Test]
        public void TestGoogle()
        {
            _driver.Navigate().GoToUrl("http://www.google.com?gl=us");
            Assert.AreEqual("Google Search", _driver.FindElement(By.Name("btnK")).GetAttribute("value"));
            TakeScreenShoot();
        }

        [Test]
        public async Task NetworkIntercerptAsync()
        {
            _driver.Navigate().GoToUrl("http://www.google.com?gl=us");
            IDevTools devTools = _driver as IDevTools;
            DevToolsSession session = devTools.GetDevToolsSession();
            await session.SendCommand(new EnableCommandSettings());
            var metricsResponse =
                await session.SendCommand<GetMetricsCommandSettings, GetMetricsCommandResponse>(
                    new GetMetricsCommandSettings());

            _driver.Navigate().GoToUrl("http://www.google.com");
            _driver.Quit();

            var metrics = metricsResponse.Metrics;
            foreach (Metric metric in metrics)
            {
                Console.WriteLine("{0} = {1}", metric.Name, metric.Value);
            }
        }

        public void TakeScreenShoot()
        {
            var screenImage = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory,  Guid.NewGuid().ToString() + ".png");       
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(screenImage, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(screenImage);
        }
        
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}