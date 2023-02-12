using Newtonsoft.Json;

namespace APITest.JsonModels.ListUsers
{
    public class Support
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
