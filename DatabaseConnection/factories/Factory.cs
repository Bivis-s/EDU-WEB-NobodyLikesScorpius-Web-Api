using System.Data.SQLite;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.factories
{
    public class Factory
    {
        private Factory()
        {
        }

        public static Zodiac CreateZodiac(SQLiteDataReader dataReader)
        {
            dataReader.Read();
            var id = dataReader.GetInt32(0);
            var name = dataReader.GetString(1);
            var zodiacType = (ZodiacType) dataReader.GetInt32(2);
            return new Zodiac(id, name, zodiacType);
        }

        public static TimeInterval CreateTimeInterval(SQLiteDataReader dataReader)
        {
            dataReader.Read();
            var id = dataReader.GetInt32(0);
            var name = dataReader.GetString(1);
            var type = (TimeIntervalType) dataReader.GetInt32(2);
            return new TimeInterval(id, name, type);
        }

        public static Prediction CreatePrediction(SQLiteDataReader dataReader, Zodiac zodiac, TimeInterval timeInterval)
        {
            dataReader.Read();
            var text = dataReader.GetString(2);
            return new Prediction(zodiac, timeInterval, text);
        }
    }
}