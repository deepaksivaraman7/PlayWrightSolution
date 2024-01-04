using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWrightNUnit
{
    internal class EATests : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Console.Out.WriteLineAsync("Opened Browser");
            await Page.GotoAsync("http://eaapp.somee.com/",
                new PageGotoOptions 
                { Timeout=5000,WaitUntil=WaitUntilState.DOMContentLoaded});

            await Console.Out.WriteLineAsync("Page loaded: " + Page.Url);
        }
        [Test]
        public async Task LoginTest()
        {
            string title = await Page.TitleAsync();
            await Console.Out.WriteLineAsync("Title: " + title);

            await Page.GetByText("Login").ClickAsync();
            await Console.Out.WriteLineAsync("Clicked login link");

            title = await Page.TitleAsync();
            await Console.Out.WriteLineAsync("Title: " + title);
            await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/Account/Login");
            await Console.Out.WriteLineAsync("Entered login page");

            //await Page.GetByLabel("UserName").FillAsync("admin");
            await Page.FillAsync(selector: "#UserName","admin");

            await Console.Out.WriteLineAsync("Entered username");

            //await Page.GetByLabel("Password").FillAsync("password");
            await Page.FillAsync(selector: "#Password", "password");
            await Console.Out.WriteLineAsync("Entered password");

            //await Page.Locator("//input[@value='Log in']").ClickAsync();
            //await Page.Locator(selector: "input", new PageLocatorOptions { HasTextString = "Log in" }).ClickAsync();
            //await Page.GetByText("Log in").ClickAsync();

            await Page.ClickAsync(selector: "text=Log in",new PageClickOptions
            {
                Timeout=1000
            });

            await Console.Out.WriteLineAsync("Clicked login button");

            title = await Page.TitleAsync();
            await Console.Out.WriteLineAsync("Title: " + title);

            //await Expect(Page).ToHaveURLAsync("http://eaapp.somee.com/");
            await Expect(Page.GetByTitle("Manage")).ToBeVisibleAsync();
            await Console.Out.WriteLineAsync("Logged in");
        }
    }
}
