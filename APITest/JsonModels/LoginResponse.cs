﻿using Newtonsoft.Json;

namespace APITest.JsonModels
{
    public class LoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
