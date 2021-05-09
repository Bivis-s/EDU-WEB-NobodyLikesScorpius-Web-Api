using DatabaseConnection.db_connections;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection
{
    public class ZodiacDbConnection : IDatabaseConnection
    {
        private static db_connections.DatabaseConnection _databaseConnection;

        public TimeInterval GetTimeInterval(TimeIntervalType timeIntervalType)
        {
            return GetDatabaseConnection().GetTimeInterval(timeIntervalType);
        }

        public Prediction GetPrediction(Zodiac zodiac, TimeInterval timeInterval)
        {
            return GetDatabaseConnection().GetPrediction(zodiac, timeInterval);
        }

        public Zodiac GetZodiac(ZodiacType zodiacType)
        {
            return GetDatabaseConnection().GetZodiac(zodiacType);
        }

        public void SaveOrUpdate(TimeInterval timeInterval)
        {
            GetDatabaseConnection().SaveOrUpdate(timeInterval);
        }

        public void SaveOrUpdate(Prediction prediction)
        {
            GetDatabaseConnection().SaveOrUpdate(prediction);
        }

        public void SaveOrUpdate(Zodiac zodiac)
        {
            GetDatabaseConnection().SaveOrUpdate(zodiac);
        }

        public void ClearAll()
        {
            GetDatabaseConnection().ClearAll();
        }

        private db_connections.DatabaseConnection GetDatabaseConnection()
        {
            return _databaseConnection ??=
                new db_connections.DatabaseConnection(DatabaseConnectionManager.GetSqlConnection());
        }
    }
}