using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.db_connections
{
    public interface IDatabaseConnection
    {
        TimeInterval GetTimeInterval(TimeIntervalType timeIntervalType);

        Prediction GetPrediction(Zodiac zodiac, TimeInterval timeInterval);

        Zodiac GetZodiac(ZodiacType zodiacType);

        void SaveOrUpdate(TimeInterval timeInterval);

        void SaveOrUpdate(Prediction prediction);

        void SaveOrUpdate(Zodiac zodiac);

        void ClearAll();
    }
}