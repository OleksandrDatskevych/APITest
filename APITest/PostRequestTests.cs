using APITest.JsonModels;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;
using APITest.HttpClients;

namespace APITest
{
    [TestFixture]
    public class PostRequestTests
    {
        [Test]
        public void UserCreate()
        {
            var url = "https://reqres.in/api/users";
            var userToCreate = new UserInfo
            {
                Job = "Unknown",
                Name = "John"
            };
            var response = MyHttpClient.PostRequest<UserInfo, CreateResponse>(url, userToCreate, null);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.IsNotNull(response.ResponseModel.Id);
            Assert.IsNotNull(response.ResponseModel.CreatedAt);
            TestContext.WriteLine($"{response.ResponseModel.Id} {response.ResponseModel.Name} {response.ResponseModel.Job} " +
                                  $"{response.ResponseModel.CreatedAt}");
            var responseTime = response.ResponseHeaders.Date - response.ResponseModel.CreatedAt;
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
            var response = MyHttpClient.PostRequest<UserCredentials, RegisterResponse>(url, userToRegister, null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.ResponseModel.Id);
            Assert.IsNotNull(response.ResponseModel.Token);
            TestContext.WriteLine($"{response.ResponseModel.Id} {response.ResponseModel.Token}");
        }

        [Test]
        public void RegisterUserNegative()
        {
            var url = "https://reqres.in/api/register";
            var userToRegister = new UserCredentials
            {
                Email = "sydney@fife"
            };
            var response = MyHttpClient.PostRequest<UserCredentials, RegisterResponse>(url, userToRegister, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsNotNull(response.ResponseModel.Error);
            TestContext.WriteLine(response.ResponseModel.Error);
        }

        [Test]
        public void RegisterUserNegative2()
        {
            var url = "https://reqres.in/api/register";
            var userToRegister = new UserCredentials
            {
                Email = "eve.holt@reqres.in"
            };
            var response = MyHttpClient.PostRequest<UserCredentials, RegisterResponse>(url, userToRegister, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsNotNull(response.ResponseModel.Error);
            TestContext.WriteLine(response.ResponseModel.Error);
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
            var responseResult = MyHttpClient.PostRequest<UserCredentials, LoginResponse>(url, userToLogin, null);
            Assert.AreEqual(HttpStatusCode.OK, responseResult.StatusCode);
            Assert.IsNotNull(responseResult.ResponseModel.Token);
            TestContext.WriteLine($"Token: {responseResult.ResponseModel.Token}");
        }

        [Test]
        public void LoginUserNegative()
        {
            var url = "https://reqres.in/api/login";
            var userToLogin = new UserCredentials
            {
                Email = "peter@kl"
            };
            var responseResult = MyHttpClient.PostRequest<UserCredentials, LoginResponse>(url, userToLogin, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, responseResult.StatusCode);
            Assert.IsNotNull(responseResult.ResponseModel.Error);
            TestContext.WriteLine($"Error: {responseResult.ResponseModel.Error}");
        }
    }
}
