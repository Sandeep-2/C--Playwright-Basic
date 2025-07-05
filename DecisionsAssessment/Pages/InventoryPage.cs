using DecisionsAssessment.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace DecisionsAssessment.Pages
{
    public class InventoryPage
    {
        private readonly IPage page;
        private CsharpActions csharpActions;

        public InventoryPage(IPage page) { 
            this.page = page;
            csharpActions = new CsharpActions(page);
        }

        public async Task AddItemToCart(string itemName)
        {
            await page.ClickAsync($"//*[text()='{itemName}']/ancestor::div[@class='inventory_item_description']//button");
        }

        public async Task RemoveItemFromCart(string itemName)
        {
            await page.ClickAsync($"//*[text()='{itemName}']/ancestor::div[@class='inventory_item_description']//button");
        }

        public async Task<int> GetCartItemCount()
        {
            var text = await csharpActions.GetTextForLocator("span.shopping_cart_badge");
            return int.Parse(text);
        }

        public async Task OpenCart()
        {         
            await csharpActions.ClickOnElementAsync("shopping_cart_badge");
        }
    }
}
