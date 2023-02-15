using System.Diagnostics;
using System.Net;
using APITest.HttpClients;
using APITest.JsonModels.ListUsers;
using NUnit.Framework;

namespace APITest
{
    [TestFixture]
    public class GetRequestTests
    {
        [Test]
        public void StatusCodeOk()
        {
            var url = "https://reqres.in/api/users?page=2";
            var response = MyHttpClient.GetRequest(url, null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void ResponseContent()
        {
            var url = "http://reqres.in/api/users/2";
            var response = MyHttpClient.GetRequest<JsonModels.SingleUser.Root>(url, null);
            Assert.AreEqual("Janet", response.ResponseModel.Data.FirstName);
        }

        [Test]
        public void StatusCodeNotFound()
        {
            var url = "https://reqres.in/api/unknown/505";
            var response = MyHttpClient.GetRequest(url, null);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void ListOfUsers()
        {
            var userId = 4;
            var url = "https://reqres.in/api/users?page=1";
            var url2 = $"https://reqres.in/api/users/{userId}";
            var response = MyHttpClient.GetRequest<Root>(url, null);
            Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
            var userDataFromList = response.ResponseModel.Data.FirstOrDefault(i => i.Id == userId);
            Assert.IsNotNull(userDataFromList);
            var response2 = MyHttpClient.GetRequest<JsonModels.SingleUser.Root>(url2, null);
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
            var userDataFromSingle = response2.ResponseModel.Data;
            Assert.AreEqual(userDataFromSingle.Id, userDataFromList.Id);
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
            var response = MyHttpClient.GetRequest(url, null);
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
            var response = MyHttpClient.GetRequest(url, null);
            stopwatch.Stop();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds > 5000);
            TestContext.WriteLine("Response time: " + stopwatch.ElapsedMilliseconds + " ms");
        }

        [Test]
        public void ListResource()
        {
            var resourseId = 2;
            var url = "https://reqres.in/api/unknown";
            var response = MyHttpClient.GetRequest<JsonModels.ListResource.Root>(url, null);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var resource = response.ResponseModel.Data.FirstOrDefault(i => i.Id == resourseId);
            Assert.IsNotNull(resource);
            Assert.AreEqual("fuchsia rose", resource.Name);
        }
    }
}
