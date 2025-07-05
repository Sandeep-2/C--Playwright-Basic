using NUnit.Framework;
using System.Threading.Tasks;
using DecisionsAssessment.Pages;
using DecisionsAssessment.Utilities;

namespace DecisionsAssessment.Tests
{
    public class TestE2E : BaseTest
    {
        [Test]
        public async Task Assessment()
        {
            var loginPage = new LoginPage(page);
            var inventoryPage = new InventoryPage(page);
            var cartPage = new CartPage(page);
            var checkoutPage = new CheckoutPage(page);

            await loginPage.NavigateAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");
            string[] items = { "Sauce Labs Backpack", "Sauce Labs Fleece Jacket" };

            foreach (string item in items)
            {
                await inventoryPage.AddItemToCart(item);
                await page.WaitForTimeoutAsync(500);
            }

            Assert.AreEqual(items.Length, await inventoryPage.GetCartItemCount(), "Cart item count mismatch");

            await inventoryPage.RemoveItemFromCart(items[0]);

            Assert.AreEqual(items.Length - 1, await inventoryPage.GetCartItemCount(), "Cart count after removal mismatch");

            await inventoryPage.OpenCart();

            Assert.AreEqual(items[1], await cartPage.GetItemName(), "Cart item does not match");
            TestContext.WriteLine("Done till here");

            await cartPage.ProceedToCheckout();

            await checkoutPage.FillCustomerInfo("Decisions", "Assessment", "000001");
            await checkoutPage.ClickContinue();

            Assert.AreEqual(items[1], await cartPage.GetItemName(), "Item mismatch at checkout");

            await checkoutPage.ClickFinish();

            Assert.AreEqual("Thank you for your order!", await checkoutPage.GetOrderConfirmation(), "Order confirmation failed");
        }
        

    } 
        

    }
