using System;
using System.Collections.Generic;
using DatabaseConnection.entities;
using TryToWebApi.objects;

namespace DatabaseConnection.db_connections
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public List<TimeInterval> GetTimeIntervals()
        {
            throw new NotImplementedException();
        }

        public List<Prediction> GetPredictions(Zodiac zodiac, TimeInterval timeInterval)
        {
            throw new NotImplementedException();
        }

        public Zodiac GetZodiac(ZodiacType type)
        {
            throw new NotImplementedException();
        }

        public void SaveTimeInterval(TimeInterval timeInterval)
        {
            throw new NotImplementedException();
        }

        public void SavePrediction(Prediction prediction)
        {
            throw new NotImplementedException();
        }

        public void SaveZodiac(Zodiac zodiac)
        {
            throw new NotImplementedException();
        }

        private List<Zodiac> GetZodiacs()
        {
            throw new NotImplementedException();
        }
    }
}