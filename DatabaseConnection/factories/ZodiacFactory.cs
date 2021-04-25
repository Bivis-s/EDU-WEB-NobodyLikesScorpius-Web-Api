using System.Data.SQLite;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.factories
{
    public class ZodiacFactory
    {
        private ZodiacFactory()
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
    }
}