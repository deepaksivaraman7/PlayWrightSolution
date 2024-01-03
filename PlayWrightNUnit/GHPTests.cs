using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlayWrightNUnit
{
    public class GHPTests : PageTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Manual instance
        //[Test]
        //public async Task Test1()
        //{
        //    //creating instance for playwright
        //    //playwright startup
        //    using var playwright = await Playwright.CreateAsync();

        //    //launch browser
        //    await using var browser = await playwright.Chromium.LaunchAsync(
        //        new BrowserTypeLaunchOptions
        //        {
        //            Headless=false
        //        }
        //        );

        //    //page instance
        //    var context = await browser.NewContextAsync();
        //    var page = await context.NewPageAsync();

        //    Console.WriteLine("Opened browser");
        //    await page.GotoAsync("https://www.google.com");
        //    Console.WriteLine("Page loaded: " + page.Url);

        //    string title = await page.TitleAsync();
        //    Console.WriteLine("Title: " + title);

        //    await page.GetByTitle("Search").FillAsync("hp laptop");
        //    Console.WriteLine("Typed");

        //    await page.GetByRole(AriaRole.Button).GetByText("Google Search").ClickAsync();
        //    Console.WriteLine("Clicked");
        //    title = await page.TitleAsync();
        //    Console.WriteLine("Title: " + title);
        //}

        //Playwright managed instance
        [Test, TestCase("hp laptop")]
        public async Task Test2(string searchText)
        {
            Console.WriteLine("Opened browser");
            await Page.GotoAsync("https://www.google.com");
            Console.WriteLine("Page loaded: " + Page.Url);

            string title = await Page.TitleAsync();
            Console.WriteLine("Title: " + title);

            await Page.GetByTitle("Search").FillAsync(searchText);
            Console.WriteLine("Typed");

            await Page.GetByRole(AriaRole.Button).GetByText("Google Search").ClickAsync();
            Console.WriteLine("Clicked");
            /* title = await Page.TitleAsync();
             Console.WriteLine("Title: " + title);
             Assert.That(title, Is.EqualTo(searchText + " - Google Search"));*/ //for c# nunit

            await Expect(Page).ToHaveTitleAsync(searchText + " - Google Search");
        }
    }
}