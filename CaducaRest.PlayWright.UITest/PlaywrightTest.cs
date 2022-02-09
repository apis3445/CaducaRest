using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CaducaRest.PlayWright.UITest
{
    
    public class PlaywrightTest
    {
        IPage page;
        [Test]
        public async Task TestGoogle()
        {
            using var playwright = await Playwright.CreateAsync();
            
            BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions { Headless = false };
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            await using var context = await browser.NewContextAsync();

            page = await context.NewPageAsync();
            await page.GotoAsync("https://www.google.com/?gl=us&hl=en");
            var searchButtonText = await page.GetAttributeAsync("input[name='btnK']","value");
            Assert.AreEqual("Google Search", searchButtonText);
            await TakeScreenShootAsync("google.png");

        }

        public async Task TakeScreenShootAsync(string name)
        {
            var screenImage = Path.Combine(TestContext.CurrentContext.TestDirectory, name + "-" + Guid.NewGuid().ToString() + ".png");
            var imageBytes = await page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true });
            File.WriteAllBytes(screenImage, imageBytes);
            TestContext.AddTestAttachment(screenImage);
        }
    }
}
