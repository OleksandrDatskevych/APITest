﻿using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.ChannelInfo
{
    public class PageInfo
    {
        [JsonProperty("totalResults")]
        public int? TotalResults { get; set; }

        [JsonProperty("resultsPerPage")]
        public int? ResultsPerPage { get; set; }
    }
}