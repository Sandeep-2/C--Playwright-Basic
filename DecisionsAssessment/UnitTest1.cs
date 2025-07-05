using Microsoft.Playwright;
using NUnit.Framework;

namespace DecisionsAssessment
{
    public class Decision
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IPage page;

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

        [Test]
        public async Task End2End()
        {
            await page.GotoAsync("https://www.saucedemo.com/");
            await page.FillAsync("#user-name", "standard_user");
            await page.FillAsync("#password", "secret_sauce");
            await page.ClickAsync("#login-button");

            string[] items = { "Sauce Labs Backpack", "Sauce Labs Fleece Jacket" };

            foreach (string item in items)
            {
                await page.ClickAsync($"//*[text()='{item}']/ancestor::div[@class='inventory_item_description']//button");
                await page.WaitForTimeoutAsync(500);
            }

            var expectedCount = items.Length;
            var actualCount = int.Parse(await GetTextForLocator("span[class='shopping_cart_badge']"));

            Assert.AreEqual(expectedCount, actualCount, "Cart item count does not match expected");

            // Remove first item
            await page.ClickAsync($"//*[text()='{items[0]}']/ancestor::div[@class='inventory_item_description']//button");

            var updatedCount = int.Parse(await page.Locator("span[class='shopping_cart_badge']").InnerTextAsync());

            Assert.AreEqual(expectedCount - 1, updatedCount, "Cart item count does not match after removal");

            await page.ClickAsync("span[class='shopping_cart_badge']");

            var actualProduct = await GetTextForLocator(".inventory_item_name");

            Assert.AreEqual(items[1], actualProduct, "Cart item does not match expected");

            await page.ClickAsync("#checkout");

            await SendTextToElementAsync("first-name", "Decisions");
            await SendTextToElementAsync("last-name", "Assessment");
            await SendTextToElementAsync("postal-code", "000001");

            await ClickOnElementAsync("continue");

            var actualProductAtCheckout = await GetTextForLocator(".inventory_item_name");

            Assert.AreEqual(items[1], actualProductAtCheckout, "Checkout item does not match expected");

            await ClickOnElementAsync("finish");

            var checkoutComplete = await GetTextForLocator("h2");

            Assert.AreEqual("Thank you for your order!", checkoutComplete, "Your order was not placed successfully");
        }

        private async Task SendTextToElementAsync(string locator, string inputValue)
        {
            await page.Locator($"#{locator}").FillAsync(inputValue);
            TestContext.WriteLine($"Entered '{inputValue}' into field '{locator}'.");
        }

        private async Task ClickOnElementAsync(string dataTestId)
        {
            await page.ClickAsync($"//*[@data-test='{dataTestId}']");
            TestContext.WriteLine($"Clicked element with data-test='{dataTestId}'.");
        }

        private async Task<string> GetTextForLocator(string locator)
        {
            return await page.Locator(locator).InnerTextAsync();
        }
    }
}