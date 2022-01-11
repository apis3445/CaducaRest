using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CaducaRest.PlayWright.UITest
{
    
    public class PlaywrightTest
    {
        [Test]
        public async Task TraceAsync()
        {
            using var playwright = await Playwright.CreateAsync();
            
            BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions { Headless = false };
            await using var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            await using var context = await browser.NewContextAsync();

            // Start tracing before creating / navigating a page.
            await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true
            });

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://playwright.dev");
            await page.ClickAsync("text=Get started");
            Assert.AreEqual("Getting started", await page.TextContentAsync("h1"));
            // Stop tracing and export it into a zip archive.
            await context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = "trace.zip"
            });
        }
    }
}
