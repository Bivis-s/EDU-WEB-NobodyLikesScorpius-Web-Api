using System.Collections.Generic;
using System.Data.SQLite;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.factories
{
    public static class Factory
    {
        public static Zodiac CreateZodiac(SQLiteDataReader dataReader)
        {
            return CreateZodiacList(dataReader)[0];
        }

        public static List<Zodiac> CreateZodiacList(SQLiteDataReader dataReader)
        {
            var zodiacs = new List<Zodiac>();
            while (dataReader.Read())
            {
                var id = dataReader.GetInt32(0);
                var name = dataReader.GetString(1);
                var zodiacType = (ZodiacType) dataReader.GetInt32(2);
                zodiacs.Add(new Zodiac(id, name, zodiacType));
            }

            return zodiacs;
        }

        public static TimeInterval CreateTimeInterval(SQLiteDataReader dataReader)
        {
            return CreateTimeIntervalList(dataReader)[0];
        }

        public static List<TimeInterval> CreateTimeIntervalList(SQLiteDataReader dataReader)
        {
            var timeIntervals = new List<TimeInterval>();
            while (dataReader.Read())
            {
                var id = dataReader.GetInt32(0);
                var name = dataReader.GetString(1);
                var type = (TimeIntervalType) dataReader.GetInt32(2);
                timeIntervals.Add(new TimeInterval(id, name, type));
            }

            return timeIntervals;
        }

        public static Prediction CreatePrediction(SQLiteDataReader dataReader, Zodiac zodiac, TimeInterval timeInterval)
        {
            dataReader.Read();
            var text = dataReader.GetString(2);
            return new Prediction(zodiac, timeInterval, text);
        }

        public static List<Haircut> CreateHaircutList(SQLiteDataReader dataReader,
            db_connections.DatabaseConnection databaseConnection)
        {
            var timeIntervals = new List<Haircut>();
            while (dataReader.Read())
            {
                var id = dataReader.GetInt32(0);
                var zodiacType = dataReader.GetInt32(1);
                var moonDay = dataReader.GetString(2);
                var moonPhase = dataReader.GetString(3);
                var prediction = dataReader.GetString(4);
                var isPositive = dataReader.GetBoolean(5);
                timeIntervals.Add(new Haircut(id, databaseConnection.GetZodiac((ZodiacType) zodiacType), moonDay,
                    moonPhase,
                    prediction, isPositive));
            }

            return timeIntervals;
        }

        public static List<Compatibility> CreateCompatibilityList(SQLiteDataReader dataReader,
            db_connections.DatabaseConnection databaseConnection)
        {
            var compatibilities = new List<Compatibility>();
            while (dataReader.Read()) compatibilities.Add(PrivateCreateCompatibility(dataReader, databaseConnection));

            return compatibilities;
        }

        public static Compatibility CreateCompatibility(SQLiteDataReader dataReader,
            db_connections.DatabaseConnection databaseConnection)
        {
            dataReader.Read();
            return PrivateCreateCompatibility(dataReader, databaseConnection);
        }

        private static Compatibility PrivateCreateCompatibility(SQLiteDataReader dataReader,
            db_connections.DatabaseConnection databaseConnection)
        {
            var id = dataReader.GetInt32(0);
            var zodiacId1 = dataReader.GetInt32(1);
            var zodiacId2 = dataReader.GetInt32(2);
            var compatibilityValue = dataReader.GetInt32(3);
            var textValue = dataReader.GetString(4);
            return new Compatibility(
                id,
                databaseConnection.GetZodiac((ZodiacType) zodiacId1),
                databaseConnection.GetZodiac((ZodiacType) zodiacId2),
                compatibilityValue,
                textValue
            );
        }
    }
}