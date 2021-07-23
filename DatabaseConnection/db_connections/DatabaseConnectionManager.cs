using System;
using System.Data.SQLite;
using System.IO;

namespace DatabaseConnection.db_connections
{
    public static class DatabaseConnectionManager
    {
        private static string GetConnectionString()
        {
            return $"Data Source={Path.GetFullPath("../DatabaseConnection/zodiacs.sqlite")};";
        }

        public static SQLiteConnection GetSqlConnection()
        {
            return new(GetConnectionString(), true);
        }
    }
}