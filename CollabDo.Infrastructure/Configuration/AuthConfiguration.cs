
using System.Text.Json.Serialization;

namespace CollabDo.Infrastructure.Configuration
{
    public class AuthConfiguration
    {
        [JsonRequired]
        public IEnumerable<string> Audiences { get;} = new List<string> { "backend" };

        [JsonRequired]
        public string ServerAddress { get; } = "http://localhost:8080";

        [JsonRequired]
        public string Realm { get; set; } = "APP";

        [JsonRequired]
        public string ClientId { get; } = "backend";

        public string ClientSecret { get; } = "qd6s131le2HItgbB3XSLuavUpBh3tQRM";
    }
}
