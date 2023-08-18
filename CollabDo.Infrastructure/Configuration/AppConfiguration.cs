using System.Text.Json.Serialization;

namespace CollabDo.Infrastructure.Configuration
{
    public class AppConfiguration 
    {
        [JsonRequired]
        public DatabaseConfig PostgresDataBase { get; } = new DatabaseConfig();

        [JsonRequired]
        public AuthConfiguration AuthTokenValidation { get; } = new AuthConfiguration();
    }
}
