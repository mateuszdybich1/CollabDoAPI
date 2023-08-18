namespace CollabDo.Infrastructure.Configuration
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; } = "User ID=admin;Password=admin;Host=localhost;Port=5432;Database=backend;Keepalive=600";
    }
}
