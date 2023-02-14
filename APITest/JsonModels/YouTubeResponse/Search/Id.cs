using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Search
{
    public class Id
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }
    }
}
