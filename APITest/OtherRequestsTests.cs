using System.Net;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
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
            var userJson = JsonConvert.SerializeObject(userToUpdate);
            var content = new StringContent(userJson, Encoding.UTF8, JsonMediaType);
            var response = MyHttpClient.PutRequest<UpdateResponse>(url, content, null);
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
            var userJson = JsonConvert.SerializeObject(userToUpdate);
            var content = new StringContent(userJson, Encoding.UTF8, JsonMediaType);
            var response = MyHttpClient.PatchRequest<UpdateResponse>(url, content, null);
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
