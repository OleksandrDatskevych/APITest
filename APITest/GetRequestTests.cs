using System.Diagnostics;
using System.Net;
using APITest.JsonModels.ListUsers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace APITest
{
    [TestFixture, Parallelizable(ParallelScope.Children)]
    public class GetRequestTests
    {
        private HttpClient _httpClient;
        private const string JsonMediaType = "application/json";

        [OneTimeSetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public void StatusCodeOK()
        {
            var url = "https://reqres.in/api/users?page=2";
            var response = _httpClient.GetAsync(url).Result;
            Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
        }

        [Test]
        public void ResponseContent()
        {
            var url = "http://reqres.in/api/users/2";
            var response = _httpClient.GetAsync(url).Result;
            var responseContent = JsonConvert.DeserializeObject<JsonModels.SingleUser.Root>(response.Content.ReadAsStringAsync().Result);
            Assert.AreEqual("Janet",responseContent.Data.FirstName);
        }

        [Test]
        public void StatusCodeNotFound()
        {
            var url = "https://reqres.in/api/unknown/23";
            var response = _httpClient.GetAsync(url).Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void ListOfUsers()
        {
            var userId = 8;
            var url = "https://reqres.in/api/users?page=2";
            var url2 = $"https://reqres.in/api/users/{userId}";
            var response = _httpClient.GetAsync(url).Result;
            Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
            var responseContent = JsonConvert.DeserializeObject<Root>(response.Content.ReadAsStringAsync().Result);
            var userDataFromList = responseContent.Data.Where(i => i.Id == userId).FirstOrDefault();
            Assert.IsNotNull(userDataFromList);
            var response2 = _httpClient.GetAsync(url2).Result;
            Assert.AreEqual(HttpStatusCode.OK,response2.StatusCode);
            var response2Content = JsonConvert.DeserializeObject<JsonModels.SingleUser.Root>(response2.Content.ReadAsStringAsync().Result);
            var userDataFromSingle = response2Content.Data;
            Assert.AreEqual(userDataFromSingle.Id,userDataFromList.Id);
            Assert.AreEqual(userDataFromSingle.FirstName, userDataFromList.FirstName);
            Assert.AreEqual(userDataFromSingle.LastName, userDataFromList.LastName);
            Assert.AreEqual(userDataFromSingle.Email, userDataFromList.Email);
            Assert.AreEqual(userDataFromSingle.Avatar,userDataFromList.Avatar);
        }

        [Test]
        public void ResponseTime()
        {
            var url = "https://reqres.in/api/users?page=2";
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = _httpClient.GetAsync(url).Result;
            stopwatch.Stop();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < 500);
            TestContext.WriteLine("Response time: " + stopwatch.ElapsedMilliseconds + " ms");
        }

        [Test]
        public void DelayedResponseTime()
        {
            var url = "https://reqres.in/api/users?delay=5";
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = _httpClient.GetAsync(url).Result;
            stopwatch.Stop();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds > 5000);
            TestContext.WriteLine("Response time: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}
