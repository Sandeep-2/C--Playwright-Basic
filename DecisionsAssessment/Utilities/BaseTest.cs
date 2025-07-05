using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DecisionsAssessment.Utilities
{
    public class BaseTest
    {
        protected IPlaywright playwright;
        protected IBrowser browser;
        protected IBrowserContext context;
        protected IPage page;

        [SetUp]
        public async Task Setup()
        {
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false // Set to true to run in headless mode
            });
            var context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
