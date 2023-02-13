using Newtonsoft.Json;

namespace APITest.JsonModels.ListResource
{
    public class Root
    {
        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("per_page")]
        public int? PerPage { get; set; }

        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("total_pages")]
        public int? TotalPages { get; set; }

        [JsonProperty("data")]
        public List<Data> Data { get; set; }

        [JsonProperty("support")]
        public Support Support { get; set; }
    }
}
