using Newtonsoft.Json;

namespace APITest.JsonModels
{
    public class UserCredentials
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
