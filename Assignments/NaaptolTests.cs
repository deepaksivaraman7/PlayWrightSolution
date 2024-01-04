using Microsoft.Playwright.NUnit;

namespace Assignments
{
    public class NaaptolTests:PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Console.Out.WriteLineAsync("Opened Browser");
            await Page.GotoAsync("https://www.naaptol.com/");
            await Console.Out.WriteLineAsync("Page loaded: " + Page.Url);
        }

        [Test]
        public async Task SearchTest()
        {
            string title = await Page.TitleAsync();
            await Console.Out.WriteLineAsync("Title: " + title);

            await Page.GetByPlaceholder("Find your favourite brand, product, model and many more").Last.FillAsync("eyewear");
            await Console.Out.WriteLineAsync("Entered search text");

            await Page.ClickAsync(selector: ".search");
            await Console.Out.WriteLineAsync("Clicked search button");
            title = await Page.TitleAsync();
            await Console.Out.WriteLineAsync("Title: " + title);
        }
    }
}