using System.Collections.Generic;
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

        public Prediction GetPrediction(ZodiacType zodiacType, TimeIntervalType timeIntervalType)
        {
            return GetDatabaseConnection().GetPrediction(zodiacType, timeIntervalType);
        }

        public Zodiac GetZodiac(ZodiacType zodiacType)
        {
            return GetDatabaseConnection().GetZodiac(zodiacType);
        }

        public Zodiac GetZodiac(int id)
        {
            return GetDatabaseConnection().GetZodiac(id);
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

        public void SaveOrUpdate(Haircut haircut)
        {
            GetDatabaseConnection().SaveOrUpdate(haircut);
        }

        public void ClearAll()
        {
            GetDatabaseConnection().ClearAll();
        }

        public bool IsSuchAdminPresent(Admin admin)
        {
            return GetDatabaseConnection().IsSuchAdminPresent(admin);
        }

        public List<Zodiac> GetZodiacs()
        {
            return GetDatabaseConnection().GetZodiacs();
        }

        public List<TimeInterval> GetTimeIntervals()
        {
            return GetDatabaseConnection().GetTimeIntervals();
        }

        public List<Haircut> GetHaircuts()
        {
            return GetDatabaseConnection().GetHaircuts();
        }

        public List<Compatibility> GetCompatibilities()
        {
            return GetDatabaseConnection().GetCompatibilities();
        }

        public void SaveOrUpdate(Compatibility compatibility)
        {
            GetDatabaseConnection().SaveOrUpdate(compatibility);
        }

        private db_connections.DatabaseConnection GetDatabaseConnection()
        {
            return _databaseConnection ??=
                new db_connections.DatabaseConnection(DatabaseConnectionManager.GetSqlConnection());
        }
    }
}