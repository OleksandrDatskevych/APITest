using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.ChannelInfo
{
    public class Localized
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
