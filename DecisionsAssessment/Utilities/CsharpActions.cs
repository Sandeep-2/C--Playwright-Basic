using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionsAssessment.Utilities
{
    public class CsharpActions
    {
        private readonly IPage page;

        public CsharpActions(IPage page) { this.page = page; }

        public async Task SendTextToElementAsync(string locator, string inputValue)
        {
            await page.Locator($"#{locator}").FillAsync(inputValue);
            TestContext.WriteLine($"Entered '{inputValue}' into field '{locator}'.");
            
        }

        public async Task ClickOnElementAsync(string dataTestId)
        {
            await page.ClickAsync($"//*[@data-test='{dataTestId}'] | //*[@class='{dataTestId}'] | //*[@id='{dataTestId}']");

            TestContext.WriteLine($"Clicked element on '{dataTestId}'.");
        }

        public async Task<string> GetTextForLocator(string locator)
        {
            return await page.Locator(locator).InnerTextAsync();
        }
    }
}
