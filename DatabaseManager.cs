using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTracker
{
    internal class DatabaseManager
    {
        //Appsetting.json config connection
        public string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            return connectionString;

        }
        
        internal void CreateTable()
        {
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                var sql = @"CREATE TABLE IF NOT EXISTS coding_tracker(
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            StartDate TEXT,
                            EndDate TEXT,
                            Duration TEXT)";

                connection.Execute(sql); //Dapper execute
            }
        }
    }
}
