using System.Text.Json.Serialization;

namespace CollabDo.Web.Configuration
{
    public class AppConfiguration 
    {
        [JsonRequired]
        public DatabaseConfig PostgresDataBase { get; } = new DatabaseConfig();

        [JsonRequired]
        public AuthConfiguration AuthTokenValidation { get; } = new AuthConfiguration();
    }
}
