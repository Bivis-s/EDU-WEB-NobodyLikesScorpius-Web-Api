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
    }
}