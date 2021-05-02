using DatabaseConnection.db_connections;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection
{
    public class ZodiacDbConnection : IDatabaseConnection
    {
        private static db_connections.DatabaseConnection _databaseConnection;

        public TimeInterval GetTimeIntervals(TimeIntervalType timeIntervalType)
        {
            return GetDatabaseConnection().GetTimeIntervals(timeIntervalType);
        }

        public Prediction GetPredictions(Zodiac zodiac, TimeInterval timeInterval)
        {
            return GetDatabaseConnection().GetPredictions(zodiac, timeInterval);
        }

        public Zodiac GetZodiac(ZodiacType zodiacType)
        {
            return GetDatabaseConnection().GetZodiac(zodiacType);
        }

        public void Save(TimeInterval timeInterval)
        {
            GetDatabaseConnection().Save(timeInterval);
        }

        public void Save(Prediction prediction)
        {
            GetDatabaseConnection().Save(prediction);
        }

        public void Save(Zodiac zodiac)
        {
            GetDatabaseConnection().Save(zodiac);
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