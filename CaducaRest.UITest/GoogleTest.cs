using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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