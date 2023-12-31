﻿using System.Text.Json.Serialization;

namespace CollabDo.Web.Configuration
{
    public class AuthConfiguration
    {
        [JsonRequired]
        public IEnumerable<string> Audiences { get;} = new List<string> { "backend" };

        [JsonRequired]
        public string ServerAddress { get; } = "http://192.168.0.110:8080";

        [JsonRequired]
        public string Realm { get; set; } = "CollabDo";

        [JsonRequired]
        public string ClientId { get; } = "backend";

        public string ClientSecret { get; } = "qd6s131le2HItgbB3XSLuavUpBh3tQRM";
    }
}
