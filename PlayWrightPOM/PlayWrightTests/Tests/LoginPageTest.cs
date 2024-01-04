using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlayWrightPOM.PlayWrightTests.Pages;
using PlayWrightPOM.TestDataClasses;
using PlayWrightPOM.Utilities;

namespace PlayWrightPOM.PlayWrightTests.Tests
{
    public class LoginPageTest : PageTest
    {
        Dictionary<string, string> Properties;
        string? currDir;
        private void ReadConfigSettings()
        {
            Properties = new(); //initializing
            currDir = Directory.GetParent(@"../../../")?.FullName;
            string configFileName = currDir + "/configsettings/config.properties";
            string[] lines = File.ReadAllLines(configFileName);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains('='))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    Properties[key] = value;
                }
            }
        }
        [SetUp]
        public async Task Setup()
        {
            ReadConfigSettings();
            Console.WriteLine("Opened Browser");
            await Page.GotoAsync(Properties["baseUrl"]);
            Console.WriteLine("Page loaded: " + Page.Url);
        }

        [Test]
        public async Task LoginTest()
        {
            NewLoginPage loginPage = new(Page);
            string excelFilePath = currDir + "/TestData/EAData.xlsx";
            string sheetName = "LoginData";

            List<EAData> excelDataList = EADataRead.ReadLoginCredentials(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? userName = excelData?.Username;
                string? password = excelData?.Password;

                await loginPage.ClickLoginLink();
                await loginPage.Login(userName, password);

                await Page.ScreenshotAsync(new()
                {
                    Path = currDir + "/Screenshots/Screenshot_" + DateTime.Now.ToString("yyyyMMdd_Hmmss") + ".png",
                    FullPage = true
                });

                Assert.That(await loginPage.CheckWelcomeMessage(), Is.True);
            }
        }
    }
}