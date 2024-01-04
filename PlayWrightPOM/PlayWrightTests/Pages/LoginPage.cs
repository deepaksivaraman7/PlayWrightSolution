using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightPOM.PlayWrightTests.Pages
{
    internal class LoginPage
    {
        private IPage _page;

        private ILocator _linkLogin;
        private ILocator _inpUserName;
        private ILocator _inpPassword;
        private ILocator _btnLogin;
        private ILocator _linkWelMess;

        public LoginPage(IPage page)
        {
            _page = page;
            _linkLogin = page.GetByText("Login");
            _inpUserName = page.Locator(selector: "#UserName");
            _inpPassword = page.Locator(selector: "#Password");
            _btnLogin = page.Locator(selector: "text=Log in");
            _linkWelMess = page.Locator(selector:"text='Hello admin!'");
        }
        public async Task ClickLoginLink()
        {
            await _linkLogin.ClickAsync();
        }
        public async Task Login(string userName, string password)
        {
            await _inpUserName.FillAsync(userName);
            await _inpPassword.FillAsync(password);
            await _btnLogin.ClickAsync();
        }
        public async Task<bool> CheckWelcomeMessage()
        {
           return await _linkWelMess.IsVisibleAsync();
        }
    }
}
