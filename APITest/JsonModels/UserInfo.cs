using Newtonsoft.Json;

namespace APITest.JsonModels
{
    public class UserInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
    }
}
