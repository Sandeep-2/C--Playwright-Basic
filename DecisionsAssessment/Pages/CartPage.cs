using DecisionsAssessment.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace DecisionsAssessment.Pages
{
    public class CartPage
    {
        private readonly IPage page;
        private CsharpActions csharpActions;
        public CartPage(IPage page) { 
            this.page = page; 
            csharpActions = new CsharpActions(page); 
        }

        public async Task<string> GetItemName()
        {
            return await csharpActions.GetTextForLocator(".inventory_item_name");
        }

        public async Task ProceedToCheckout()
        {
            await csharpActions.ClickOnElementAsync("checkout");
        }
    }
}
