using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Videos
{
    public class Statistics
    {
        [JsonProperty("viewCount")]
        public string ViewCount { get; set; }

        [JsonProperty("likeCount")]
        public string LikeCount { get; set; }

        [JsonProperty("favoriteCount")]
        public string FavoriteCount { get; set; }

        [JsonProperty("commentCount")]
        public string CommentCount { get; set; }
    }
}
