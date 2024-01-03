using Microsoft.Playwright;

//creating instance for playwright
//playwright startup
using var playwright = await Playwright.CreateAsync();

//launch browser
await using var browser = await playwright.Chromium.LaunchAsync();

//page instance
var context = await browser.NewContextAsync();
var page = await context.NewPageAsync();

Console.WriteLine("Opened browser");
await page.GotoAsync("https://www.google.com");
Console.WriteLine("Page loaded: "+ page.Url);

string title = await page.TitleAsync();
Console.WriteLine("Title: " + title);

await page.GetByTitle("Search").FillAsync("hp laptop");
Console.WriteLine("Typed");

await page.GetByRole(AriaRole.Button).GetByText("Google Search").ClickAsync();
Console.WriteLine("Clicked");
title = await page.TitleAsync();
Console.WriteLine("Title: " + title);



