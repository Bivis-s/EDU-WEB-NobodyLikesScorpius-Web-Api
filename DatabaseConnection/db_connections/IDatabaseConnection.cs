using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.db_connections
{
    public interface IDatabaseConnection
    {
        TimeInterval GetTimeIntervals(TimeIntervalType timeIntervalType);

        Prediction GetPredictions(Zodiac zodiac, TimeInterval timeInterval);

        Zodiac GetZodiac(ZodiacType zodiacType);

        void Save(TimeInterval timeInterval);

        void Save(Prediction prediction);

        void Save(Zodiac zodiac);

        void ClearAll();
    }
}