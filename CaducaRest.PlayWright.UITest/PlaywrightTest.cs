using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CaducaRest.PlayWright.UITest
{
    
    public class PlaywrightTest
    {
        [Test]
        public async Task TestGoogle()
        {
            using var playwright = await Playwright.CreateAsync();
            
            BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions { Headless = false };
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            await using var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://www.google.com/?gl=us&hl=en");
            var searchButtonText = await page.GetAttributeAsync("input[name='btnK']","value");
            Assert.AreEqual("Google Search", searchButtonText);
            await page.ScreenshotAsync(new PageScreenshotOptions { Path = "google.png" });

        }
    }
}
