using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Videos
{
    public class Thumbnails
    {
        [JsonProperty("default")]
        public Default Default { get; set; }

        [JsonProperty("medium")]
        public Medium Medium { get; set; }

        [JsonProperty("high")]
        public High High { get; set; }

        [JsonProperty("standard")]
        public Standard Standard { get; set; }

        [JsonProperty("maxres")]
        public Maxres Maxres { get; set; }
    }
}
