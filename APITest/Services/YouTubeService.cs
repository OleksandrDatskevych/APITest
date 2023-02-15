using APITest.HttpClients;
using APITest.JsonModels.YouTubeResponse.Videos;

namespace APITest.Services
{
    public static class YouTubeService
    {
        private const string BaseUrl = "https://www.googleapis.com/youtube/v3/";
        private const string ApiKey = "YOUR_YOUTUBE_API_KEY";

        public static RestResponse<JsonModels.YouTubeResponse.ChannelInfo.Root>? GetUserInfo(string username)
        {
            var url = $"{BaseUrl}channels?key={ApiKey}&forUsername={username}&part=snippet,statistics";
            var response = MyHttpClient.GetRequest<JsonModels.YouTubeResponse.ChannelInfo.Root>(url, null);

            return response;
        }

        public static List<JsonModels.YouTubeResponse.Search.Item> GetListOfUserVideos(string username, int maxResults)
        {
            var url = $"{BaseUrl}search?key={ApiKey}&channelId={GetUserId(username)}&maxResults={maxResults}&part=snippet&order=viewCount";
            var response = MyHttpClient.GetRequest<JsonModels.YouTubeResponse.Search.Root>(url, null);
            var listOfVideos = response.ResponseModel.Items.Where(i => i.Id.Kind == "youtube#video").ToList();

            return listOfVideos;
        }

        public static Dictionary<string, int> GetDictionaryOfUserVideos(string username, int maxResults)
        {
            var listOfVideos = GetListOfUserVideos(username, maxResults);
            var videoIdList = listOfVideos.Select(video => video.Id.VideoId).ToList();
            var videoString = string.Join(",", videoIdList);
            var url = $"{BaseUrl}videos?key={ApiKey}&id={videoString}&part=snippet,statistics";
            var response = MyHttpClient.GetRequest<Root>(url, null);
            var dictionaryOfVideos = listOfVideos.ToDictionary(video => video.Snippet.Title, video => response.ResponseModel.Items
                                                                                            .Where(i => i.Snippet.Title == video.Snippet.Title)
                                                                                            .Select(i => int.Parse(i.Statistics.ViewCount))
                                                                                            .FirstOrDefault());

            return dictionaryOfVideos;
        }

        public static List<JsonModels.YouTubeResponse.Search.Item> GetSearchQueryList(string query, int maxResults)
        {
            var url = $"{BaseUrl}search?key={ApiKey}&maxResults={maxResults}&q={query}&part=snippet";
            var response = MyHttpClient.GetRequest<JsonModels.YouTubeResponse.Search.Root>(url, null);
            var listOfVideos = response.ResponseModel.Items;

            return listOfVideos;
        }

        private static string GetUserId(string username)
        {
            var url = $"{BaseUrl}channels?key={ApiKey}&forUsername={username}&part=id";
            var response = MyHttpClient.GetRequest<JsonModels.YouTubeResponse.ChannelInfo.Root>(url, null);
            var userId = response.ResponseModel.Items.FirstOrDefault().Id;

            return userId;
        }
    }
}
