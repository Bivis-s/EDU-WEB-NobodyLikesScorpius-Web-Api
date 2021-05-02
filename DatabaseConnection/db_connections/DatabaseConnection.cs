using System;
using System.Data.SQLite;
using DatabaseConnection.attributes;
using DatabaseConnection.entities;
using DatabaseConnection.factories;
using TryToWebApi.objects;

namespace DatabaseConnection.db_connections
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private SQLiteConnection _connection;

        public DatabaseConnection(SQLiteConnection connection)
        {
            _connection = connection.OpenAndReturn();
        }

        public TimeInterval
            GetTimeIntervals(TimeIntervalType timeIntervalType) //TODO make enum_number in db table UNIQUE
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "select id, name, enum_number " +
                                  "from time_intervals " +
                                  "where enum_number = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) timeIntervalType);
            return Factory.CreateTimeInterval(command.ExecuteReader());
        }

        public Prediction GetPredictions(Zodiac zodiac, TimeInterval timeInterval)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "select text_value " +
                                  "from predictions " +
                                  "where zodiac_id = :zodiac_id and time_interval_id = :time_interval_id;";
            command.Parameters.AddWithValue("zodiac_id", zodiac.Id);
            command.Parameters.AddWithValue("time_interval_id", timeInterval.Id);
            return Factory.CreatePrediction(command.ExecuteReader(), zodiac, timeInterval);
        }

        public Zodiac GetZodiac(ZodiacType zodiacType) //TODO make enum_number in db table UNIQUE
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "select id, name, enum_number from zodiacs where enum_number = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) zodiacType);
            return Factory.CreateZodiac(command.ExecuteReader());
        }

        public void Save(TimeInterval timeInterval)
        {
            var tableName = TableName.GetTableName(timeInterval.GetType());
            var nameColumn = SerializableName.GetSerializableName(timeInterval.GetType(), "Name");
            var typeColumn = SerializableName.GetSerializableName(timeInterval.GetType(), "Type");

            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = $"insert into {tableName}({nameColumn}, {typeColumn}) values (:name, :type);";
            command.Parameters.AddWithValue("name", timeInterval.Name);
            command.Parameters.AddWithValue("type", (int) timeInterval.Type);
            command.ExecuteNonQuery();
        }

        public void Save(Prediction prediction)
        {
            var tableName = TableName.GetTableName(prediction.GetType());
            var zodiacIdColumn = SerializableName.GetSerializableName(prediction.GetType(), "Zodiac");
            var timeIntervalColumn = SerializableName.GetSerializableName(prediction.GetType(), "TimeInterval");
            var textColumn = SerializableName.GetSerializableName(prediction.GetType(), "Text");

            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"insert into {tableName}({zodiacIdColumn}, {timeIntervalColumn}, {textColumn}) values (:zodiac_id, :interval_id, :text);";
            command.Parameters.AddWithValue("zodiac_id", prediction.Zodiac.Id);
            command.Parameters.AddWithValue("interval_id", prediction.TimeInterval.Id);
            command.Parameters.AddWithValue("text", prediction.Text);
            command.ExecuteNonQuery();
        }

        public void Save(Zodiac zodiac)
        {
            var tableName = TableName.GetTableName(zodiac.GetType());
            var nameColumn = SerializableName.GetSerializableName(zodiac.GetType(), "Name");
            var typeColumn = SerializableName.GetSerializableName(zodiac.GetType(), "Type");

            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = $"insert into {tableName}({nameColumn}, {typeColumn}) values (:name, :type);";
            command.Parameters.AddWithValue("name", zodiac.Name);
            command.Parameters.AddWithValue("type", (int) zodiac.Type);
            command.ExecuteNonQuery();
        }

        public void ClearAll()
        {
            ExecuteNonQuery("delete from compatibilities; " +
                            "delete from haircuts; " +
                            "delete from predictions; " +
                            "delete from time_intervals; " +
                            "delete from zodiacs;");
        }

        private void ExecuteNonQuery(string commandText)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = commandText;
            command.ExecuteNonQuery();
        }
    }
}