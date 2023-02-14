﻿using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Videos
{
    public class Item
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("snippet")]
        public Snippet Snippet { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }
    }
}