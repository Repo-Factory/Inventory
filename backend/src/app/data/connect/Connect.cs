using Npgsql;

namespace DataBase
{
    class ConnectionHelper()
    {
        public static NpgsqlConnection Connect()
        {
            NpgsqlConnectionStringBuilder builder = new()
            {
                Host = "localhost",
                Username = "docker_postgres",
                Password = "docker_postgres",
                Database = "docker_postgres",
                Port = 5432
            };
            return new(builder.ConnectionString);
        }
    }
}

