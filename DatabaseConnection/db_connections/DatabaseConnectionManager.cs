using System.Data.SQLite;

namespace DatabaseConnection.db_connections
{
    public class DatabaseConnectionManager
    {
        private DatabaseConnectionManager()
        {
        }

        private static string GetConnectionString()
        {
            // return $"Data Source={Path.GetFullPath("DatabaseConnection/zodiacs.sqlite")};"; TODO optimize
            return
                @"Data Source=C:\Users\Anton\RiderProjects\TryToWebApi\TryToWebApi\DatabaseConnection\zodiacs.sqlite";
        }

        public static SQLiteConnection GetSqlConnection()
        {
            return new(GetConnectionString(), true);
        }
    }
}