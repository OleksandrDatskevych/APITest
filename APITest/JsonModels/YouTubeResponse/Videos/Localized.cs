using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Videos
{
    public class Localized
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
