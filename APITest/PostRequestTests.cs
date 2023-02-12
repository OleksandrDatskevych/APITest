using APITest.JsonModels;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace APITest
{
    [TestFixture, Parallelizable(ParallelScope.Children)]
    public class PostRequestTests
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
        }

        [Test]
        public void UserCreate()
        {
            var url = "https://reqres.in/api/users";
            var userToCreate = new UserInfo
            {
                Job = "Unknown",
                Name = "John"
            };
            var userJson = JsonConvert.SerializeObject(userToCreate);
            var content = new StringContent(userJson, Encoding.UTF8, JsonMediaType);
            var response = _httpClient.PostAsync(url, content);
            var responseContent = JsonConvert.DeserializeObject<CreateResponse>(response.Result.Content.ReadAsStringAsync().Result);
            Assert.AreEqual(HttpStatusCode.Created, response.Result.StatusCode);
            TestContext.WriteLine($"{responseContent.Id} {responseContent.Name} {responseContent.Job} {responseContent.CreatedAt}");
            var responseTime = response.Result.Headers.Date - responseContent.CreatedAt;
            Assert.True(responseTime < TimeSpan.FromSeconds(1));
            TestContext.WriteLine(responseTime);
        }

        [Test]
        public void RegisterUserPositive()
        {
            var url = "https://reqres.in/api/register";
            var userToRegister = new UserCredentials
            {
                Email = "eve.holt@reqres.in",
                Password = "pass"
            };
            var registerJson = JsonConvert.SerializeObject(userToRegister);
            var content = new StringContent(registerJson, Encoding.UTF8, JsonMediaType);
            var response = _httpClient.PostAsync(url, content).Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseContent = JsonConvert.DeserializeObject<RegisterResponse>(response.Content.ReadAsStringAsync().Result);
            TestContext.WriteLine($"{responseContent.Id} {responseContent.Token}");
        }

        [Test]
        public void RegisterUserNegative()
        {
            var url = "https://reqres.in/api/register";
            var userToRegister = new UserCredentials
            {
                Email = "sydney@fife"
            };
            var registerJson = JsonConvert.SerializeObject(userToRegister);
            var content = new StringContent(registerJson, Encoding.UTF8, JsonMediaType);
            var response = _httpClient.PostAsync(url, content).Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void PutRequest()
        {
            var url = "https://reqres.in/api/users/2";
            var userToUpdate = new UserInfo
            {
                Job = "Accountant",
                Name = "Bill"
            };
            var userJson = JsonConvert.SerializeObject(userToUpdate);
            var content = new StringContent(userJson, Encoding.UTF8, JsonMediaType);
            var response = _httpClient.PutAsync(url, content);
            var responseContent = JsonConvert.DeserializeObject<UpdateResponse>(response.Result.Content.ReadAsStringAsync().Result);
            TestContext.WriteLine($"{responseContent.Name} {responseContent.Job} {responseContent.UpdatedAt}");
            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void LoginUserPositive()
        {
            var url = "https://reqres.in/api/login";
            var userToLogin = new UserCredentials
            {
                Email = "eve.holt@reqres.in",
                Password = "pass"
            };
            var userJson = JsonConvert.SerializeObject(userToLogin);
            var content = new StringContent(userJson, Encoding.UTF8, JsonMediaType);
            var responseResult = _httpClient.PostAsync(url, content).Result;
            Assert.AreEqual(HttpStatusCode.OK, responseResult.StatusCode);
        }

        [Test]
        public void LoginUserNegative()
        {
            var url = "https://reqres.in/api/login";
            var userToLogin = new UserCredentials
            {
                Email = "peter@kl"
            };
            var userJson = JsonConvert.SerializeObject(userToLogin);
            var content = new StringContent(userJson, Encoding.UTF8, JsonMediaType);
            var responseResult = _httpClient.PostAsync(url, content).Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, responseResult.StatusCode);
        }
    }
}
