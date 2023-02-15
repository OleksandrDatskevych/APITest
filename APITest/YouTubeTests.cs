using System.Net;
using NUnit.Framework;
using APITest.Services;

namespace APITest
{
    [TestFixture]
    public class YouTubeTests
    {
        [Test]
        public void UserInfo()
        {
            var response = YouTubeService.GetUserInfo("aschornas");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.ResponseModel.Items);
            Assert.AreEqual("UA", response.ResponseModel.Items.FirstOrDefault().Snippet.Country);
            TestContext.WriteLine("User Id: " + response.ResponseModel.Items.FirstOrDefault().Id);
        }

        [Test]
        public void NegativeUserInfo()
        {
            var response = YouTubeService.GetUserInfo("notdesu");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNull(response.ResponseModel.Items);
        }

        [Test]
        public void UserVideos()
        {
            var maxResults = 25;
            var listOfVideos = YouTubeService.GetListOfUserVideos("aschornas", maxResults);
            Assert.IsNotNull(listOfVideos);
            var listOfTitles = listOfVideos.Select(i => i.Snippet.Title).ToList();
            Assert.Contains("A Hat in Time Any% speedrun PB 59:41.14", listOfTitles);
        }

        [Test]
        public void ViewCount()
        {
            var maxResults = 5;
            var dictionaryOfVideos = YouTubeService.GetDictionaryOfUserVideos("aschornas", maxResults);
            Assert.IsNotNull(dictionaryOfVideos);
            Assert.True(dictionaryOfVideos.Values.Any(i => i > 100000));
        }

        [Test]
        public void SearchQuery()
        {
            var maxResults = 10;
            var searchQuery = "lofi";
            var listOfVideos = YouTubeService.GetSearchQueryList(searchQuery, maxResults);
            Assert.IsNotNull(listOfVideos);
            Assert.True(listOfVideos.All(i => i.Snippet.Title.ToLower().Contains(searchQuery.ToLower())));
        }
    }
}
