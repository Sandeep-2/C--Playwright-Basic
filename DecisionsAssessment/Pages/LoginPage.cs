using DecisionsAssessment.Utilities;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace DecisionsAssessment.Pages
{
    public class LoginPage
    {
        private readonly IPage page;
        private CsharpActions csharpActions;

        public LoginPage(IPage page) {
            this.page = page; 
            csharpActions = new CsharpActions(page); 
        }

        public async Task NavigateAsync() =>
            await page.GotoAsync("https://www.saucedemo.com/");

        public async Task LoginAsync(string username, string password)
        {
            await csharpActions.SendTextToElementAsync("user-name", username);
            await csharpActions.SendTextToElementAsync("password", password);
            await csharpActions.ClickOnElementAsync("login-button");
        }
    }
}
