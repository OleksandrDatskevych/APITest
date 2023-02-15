using Newtonsoft.Json;

namespace APITest.JsonModels.YouTubeResponse.Search
{
    public class PageInfo
    {
        [JsonProperty("totalResults")]
        public int? TotalResults { get; set; }

        [JsonProperty("resultsPerPage")]
        public int? ResultsPerPage { get; set; }
    }
}
