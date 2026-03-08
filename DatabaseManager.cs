using CodingTracker.Models;
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
                            StartTime TEXT,
                            EndTime TEXT,
                            Duration TEXT)";

                connection.Execute(sql); //Dapper execute
            }
        }

        internal int Post(CodingSession session)
        {
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                var sql = "INSERT INTO coding_tracker (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

                var rowsAffected = connection.Execute(sql,session); //Dapper Execute
                return rowsAffected;
            }
        }

        internal List<CodingSession> Get()
        { 
            using (var connection = new SqliteConnection(GetConnectionString()))
            {
                var sql = "SELECT * FROM coding_tracker";

                var sessions = connection.Query<CodingSession>(sql);

                return sessions.ToList();
            }
        }
    }
}
