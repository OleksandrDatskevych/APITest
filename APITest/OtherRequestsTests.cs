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

        [Test]
        public void GoRestCRUD()
        {
            var urlPost = "https://gorest.co.in/public/v2/users";
            var headers = new Dictionary<string, string> { { "Authorization", "Bearer 7d221c0ab80983191373add5ec7474e9fd6eae017f8a0998ebb76893ad159f52" } };
            var userToCreate = new GoRestUser
            {
                Id = 100,
                Name = "John Doe",
                Email = "testerino@testss.com",
                Gender = "male",
                Status = "active"
            };
            var postRequest = MyHttpClient.PostRequest<GoRestUser, GoRestUser>(urlPost, userToCreate, headers);
            Assert.AreEqual(HttpStatusCode.Created, postRequest.StatusCode);
            TestContext.WriteLine($"Id: {postRequest.ResponseModel.Id}, Name: {postRequest.ResponseModel.Name}");
            var userId = postRequest.ResponseModel.Id;
            var urlRUD = $"{urlPost}/{userId}";
            var getRequest = MyHttpClient.GetRequest<GoRestUser>(urlRUD, headers);
            Assert.AreEqual(HttpStatusCode.OK, getRequest.StatusCode);
            Assert.AreEqual(userToCreate.Name, getRequest.ResponseModel.Name);
            Assert.AreEqual(userToCreate.Email, getRequest.ResponseModel.Email);
            Assert.AreEqual(userToCreate.Gender, getRequest.ResponseModel.Gender);
            Assert.AreEqual(userToCreate.Status, getRequest.ResponseModel.Status);
            var userUpdate = new GoRestUser
            {
                Id = 100,
                Name = "Mary Sue",
                Email = "testerino123@test.com",
                Gender = "female",
                Status = "active"
            };
            var putRequest = MyHttpClient.PutRequest<GoRestUser, GoRestUser>(urlRUD, userUpdate, headers);
            Assert.AreEqual(HttpStatusCode.OK, putRequest.StatusCode);
            Assert.AreEqual(userUpdate.Email, putRequest.ResponseModel.Email);
            var deleteRequest = MyHttpClient.DeleteRequest(urlRUD, null, headers);
            Assert.AreEqual(HttpStatusCode.NoContent, deleteRequest.StatusCode);
            var getRequestNotFound = MyHttpClient.GetRequest<GoRestUser>(urlRUD, headers);
            Assert.AreEqual(HttpStatusCode.NotFound, getRequestNotFound.StatusCode);
        }
    }
}
