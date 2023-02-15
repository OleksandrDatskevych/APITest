﻿using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Videos
{
    public class High
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int? Width { get; set; }

        [JsonProperty("height")]
        public int? Height { get; set; }
    }
}
