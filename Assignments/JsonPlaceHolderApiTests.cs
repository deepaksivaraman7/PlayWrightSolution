using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assignments
{
    internal class JsonPlaceHolderApiTests
    {
        IAPIRequestContext requestContext;
        [SetUp]
        public async Task Setup()
        {
            var playWright = await Playwright.CreateAsync();
            requestContext = await playWright.APIRequest.NewContextAsync(
                new APIRequestNewContextOptions
                {
                    BaseURL = "https://jsonplaceholder.typicode.com/",
                    IgnoreHTTPSErrors = true
                });
        }

        [Test]
        public async Task GetAllPosts()
        {
            var getResponse = await requestContext.GetAsync(url: "posts");
            await Console.Out.WriteLineAsync("Response: " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + getResponse.StatusText);

            Assert.That(getResponse.Status, Is.EqualTo(200));
            Assert.That(getResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());

        }

        [Test, TestCase(2)]
        public async Task GetSinglePost(int id)
        {
            var getResponse = await requestContext.GetAsync(url: "posts/" + id);
            await Console.Out.WriteLineAsync("Response: " + getResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + getResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + getResponse.StatusText);

            Assert.That(getResponse.Status, Is.EqualTo(200));
            Assert.That(getResponse, Is.Not.Null);

            JsonElement responseBody = (JsonElement)await getResponse.JsonAsync();
            await Console.Out.WriteLineAsync("Response body: " + responseBody.ToString());

        }

        [Test, TestCase("New title", "New body")]
        public async Task CreatePost(string newTitle, string newBody)
        {
            var postData = new
            {
                userId = 3,
                title = newTitle,
                body = newBody
            };
            var jsonPostData = JsonSerializer.Serialize(postData);
            var postResponse = await requestContext.PostAsync(url: "posts", new APIRequestContextOptions
            {
                //Data = jsonPostData
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

        [Test, TestCase(2,3, "Updated title", "Updated body")]
        public async Task PutPost(int id,int userID, string updatedTitle, string updatedBody)
        {
            var putData = new
            {
                userId = userID,
                title = updatedTitle,
                body = updatedBody
            };
            var jsonPutData = JsonSerializer.Serialize(putData);
            var putResponse = await requestContext.PutAsync(url: "posts/" + id, new APIRequestContextOptions
            {
                //Data = jsonPutData
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
        public async Task DeletePost(int id)
        {
            var deleteResponse = await requestContext.DeleteAsync(url: "posts/" + id);
            await Console.Out.WriteLineAsync("Response: " + deleteResponse.ToString());
            await Console.Out.WriteLineAsync("Code: " + deleteResponse.Status);
            await Console.Out.WriteLineAsync("Text: " + deleteResponse.StatusText);

            Assert.That(deleteResponse.Status, Is.EqualTo(200));
        }
    }
}
