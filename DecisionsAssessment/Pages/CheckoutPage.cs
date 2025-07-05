using DecisionsAssessment.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace DecisionsAssessment.Pages
{
    public class CheckoutPage
    {
        private readonly IPage page;
        private CsharpActions csharpActions;

        public CheckoutPage(IPage page) { 
            this.page = page;
            csharpActions = new CsharpActions(page);
        }

        public async Task FillCustomerInfo(string firstName, string lastName, string postalCode)
        {
            await csharpActions.SendTextToElementAsync("first-name", firstName);
            await csharpActions.SendTextToElementAsync("last-name", lastName);
            await csharpActions.SendTextToElementAsync("postal-code", postalCode);
        }

        public async Task ClickContinue()
        {
            await csharpActions.ClickOnElementAsync("continue");
        }

        public async Task ClickFinish()
        {
            await csharpActions.ClickOnElementAsync("finish");
        }

        public async Task<string> GetOrderConfirmation()
        {
            return await page.Locator("h2").InnerTextAsync();
        }
    }
}
