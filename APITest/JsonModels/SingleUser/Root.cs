using Newtonsoft.Json;

namespace APITest.JsonModels.SingleUser
{
    public class Root
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("support")]
        public Support Support { get; set; }
    }
}
