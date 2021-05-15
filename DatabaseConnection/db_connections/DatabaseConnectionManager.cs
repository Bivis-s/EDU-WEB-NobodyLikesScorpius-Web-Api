﻿using System.Data.SQLite;
using System.IO;

namespace DatabaseConnection.db_connections
{
    public class DatabaseConnectionManager
    {
        private DatabaseConnectionManager()
        {
        }

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