using System.Net;
using NUnit.Framework;
using APITest.JsonModels;
using APITest.HttpClients;

namespace APITest
{
    [TestFixture]
    public class OtherRequestsTests
    {
        private const string JsonMediaType = "application/json";

        [Test]
        public void PutRequest()
        {
            var url = "https://reqres.in/api/users/2";
            var userToUpdate = new UserInfo
            {
                Job = "Accountant",
                Name = "Bill"
            };
            var response = MyHttpClient.PutRequest<UserInfo, UpdateResponse>(url, userToUpdate, null);
            TestContext.WriteLine($"{response.ResponseModel.Name} {response.ResponseModel.Job} {response.ResponseModel.UpdatedAt}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(userToUpdate.Name, response.ResponseModel.Name);
        }

        [Test]
        public void PatchRequest()
        {
            var url = "https://reqres.in/api/users/2";
            var userToUpdate = new UserInfo
            {
                Job = "JobName",
                Name = "Joe"
            };
            var response = MyHttpClient.PatchRequest<UserInfo, UpdateResponse>(url, userToUpdate, null);
            TestContext.WriteLine($"{response.ResponseModel.Name} {response.ResponseModel.Job} {response.ResponseModel.UpdatedAt}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(userToUpdate.Name, response.ResponseModel.Name);
        }

        [Test]
        public void DeleteRequest()
        {
            var url = "https://reqres.in/api/users/2";
            var response = MyHttpClient.DeleteRequest(url, null, null);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
