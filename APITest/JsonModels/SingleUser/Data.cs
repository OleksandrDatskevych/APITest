﻿using Newtonsoft.Json;

namespace APITest.JsonModels.SingleUser
{
    public class Data
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}