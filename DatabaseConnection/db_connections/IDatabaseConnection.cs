using System.Collections.Generic;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.db_connections
{
    public interface IDatabaseConnection
    {
        TimeInterval GetTimeInterval(TimeIntervalType timeIntervalType);

        Prediction GetPrediction(ZodiacType zodiacType, TimeIntervalType timeIntervalType);

        Zodiac GetZodiac(ZodiacType zodiacType);

        Zodiac GetZodiac(int id);

        void SaveOrUpdate(TimeInterval timeInterval);

        void SaveOrUpdate(Prediction prediction);

        void SaveOrUpdate(Zodiac zodiac);

        void SaveOrUpdate(Haircut haircut);

        void ClearAll();

        bool IsSuchAdminPresent(Admin admin);

        List<Zodiac> GetZodiacs();

        List<TimeInterval> GetTimeIntervals();

        List<Haircut> GetHaircuts();

        List<Compatibility> GetCompatibilities();

        void SaveOrUpdate(Compatibility compatibility);
    }
}