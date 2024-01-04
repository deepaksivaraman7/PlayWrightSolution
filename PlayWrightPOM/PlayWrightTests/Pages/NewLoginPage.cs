using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightPOM.PlayWrightTests.Pages
{
    internal class NewLoginPage
    {
        private IPage _page;

        private ILocator LinkLogin => _page.GetByText("Login");
        private ILocator InpUserName => _page.Locator(selector: "#UserName");
        private ILocator InpPassword => _page.Locator(selector: "#Password");
        private ILocator BtnLogin => _page.Locator(selector: "text=Log in");
        private ILocator LinkWelMess => _page.Locator(selector: "text='Hello admin!'");

        public NewLoginPage(IPage page) => _page = page;
        public async Task ClickLoginLink() => await LinkLogin.ClickAsync();
        public async Task Login(string userName, string password)
        {
            await InpUserName.FillAsync(userName);
            await InpPassword.FillAsync(password);
            await BtnLogin.ClickAsync();
        }
        public async Task<bool> CheckWelcomeMessage() => await LinkWelMess.IsVisibleAsync();
    }
}
