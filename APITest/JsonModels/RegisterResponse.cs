using Newtonsoft.Json;

namespace APITest.JsonModels
{
    public class RegisterResponse
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
