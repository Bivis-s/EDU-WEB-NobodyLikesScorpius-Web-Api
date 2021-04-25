using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DatabaseConnection.entities;
using DatabaseConnection.factories;
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

        public Zodiac GetZodiac(ZodiacType zodiacType) //TODO make enum_number in db table UNIQUE
        {
            var connection = DatabaseConnectionManager.GetSqlConnection().OpenAndReturn();
            var command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandText = "select id, name, enum_number from zodiacs where enum_number = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) zodiacType);
            var zodiac = ZodiacFactory.CreateZodiac(command.ExecuteReader());
            command.Dispose();
            connection.Dispose();
            return zodiac;
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