using Microsoft.Playwright;
using System.Text.Json;

namespace PlayWrightAPI
{
    public class ReqResApiTests
    {
        IAPIRequestContext requestContext;
        [SetUp]
        public async Task Setup()
        {
            var playWright = await Playwright.CreateAsync();
            requestContext = await playWright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://reqres.in/api/",
                    IgnoreHTTPSErrors = true
                });
        }

        [Test, TestCase(2)]
        public async Task GetAllUsers(int pageNo)
        {
            var getResponse = await requestContext.GetAsync(url: "users?page=" + pageNo);
            await Console.Out.WriteLineAsync("Response: " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + getResponse.StatusText);

            Assert.That(getResponse.Status, Is.EqualTo(200));
            Assert.That(getResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());

        }

        [Test, TestCase(2)]
        public async Task GetSingleUser(int id)
        {
            var getResponse = await requestContext.GetAsync(url: "users/" + id);
            await Console.Out.WriteLineAsync("Response: " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + getResponse.StatusText);

            Assert.That(getResponse.Status, Is.EqualTo(200));
            Assert.That(getResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());

        }

        [Test, TestCase(23)]
        public async Task GetSingleUserNotFound(int id)
        {
            var getResponse = await requestContext.GetAsync(url: "users/" + id);
            await Console.Out.WriteLineAsync("Response: " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + getResponse.StatusText);

            Assert.That(getResponse.Status, Is.EqualTo(404));
            Assert.That(getResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());
            Assert.That(responseBody.ToString(), Is.EqualTo("{}"));
        }

        [Test, TestCase("John", "Engineer")]
        public async Task PostUser(string newName, string newJob)
        {
            var postData = new
            {
                name = newName,
                job = newJob
            };
            var jsonPostData = JsonSerializer.Serialize(postData);
            var postResponse = await requestContext.PostAsync(url: "users", new APIRequestContextOptions
            {
                Data = jsonPostData
            });
            await Console.Out.WriteLineAsync("Response: " + postResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + postResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + postResponse.StatusText);

            Assert.That(postResponse.Status, Is.EqualTo(201));
            Assert.That(postResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await postResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());
            Assert.That(responseBody.ToString(), Is.Not.Empty);
        }

        [Test, TestCase(2, "John", "Engineer")]
        public async Task PutUser(int id, string updatedName, string updatedJob)
        {
            var putData = new
            {
                name = updatedName,
                job = updatedJob
            };
            var jsonPutData = JsonSerializer.Serialize(putData);
            var putResponse = await requestContext.PutAsync(url: "users/" + id, new APIRequestContextOptions
            {
                Data = jsonPutData
            });
            await Console.Out.WriteLineAsync("Response: " + putResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + putResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + putResponse.StatusText);

            Assert.That(putResponse.Status, Is.EqualTo(200));
            Assert.That(putResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await putResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());
            Assert.That(responseBody.ToString(), Is.Not.Empty);
        }

        [Test, TestCase(2)]
        public async Task DeleteUser(int id)
        {
            var deleteResponse = await requestContext.DeleteAsync(url: "users/" + id);
            await Console.Out.WriteLineAsync("Response: " + deleteResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + deleteResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + deleteResponse.StatusText);

            Assert.That(deleteResponse.Status, Is.EqualTo(204));
        }
    }
}