using System.Data.SQLite;
using DatabaseConnection.entities;
using DatabaseConnection.factories;
using TryToWebApi.objects;

namespace DatabaseConnection.db_connections
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly SQLiteConnection _connection;

        public DatabaseConnection(SQLiteConnection connection)
        {
            _connection = connection.OpenAndReturn();
        }

        public TimeInterval GetTimeIntervals(TimeIntervalType timeIntervalType)
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
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"insert into {TimeInterval.GetTableName()}({TimeInterval.GetNameColumnName()}, {TimeInterval.GetTypeColumnName()}) " +
                "values (:name, :type);";
            command.Parameters.AddWithValue("name", timeInterval.Name);
            command.Parameters.AddWithValue("type", (int) timeInterval.Type);
            command.ExecuteNonQuery();
        }

        public void Save(Prediction prediction)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"insert into {Prediction.GetTableName()}({Prediction.GetZodiacColumnName()}, {Prediction.GetTimeIntervalColumnName()}, {Prediction.GetTextColumnName()}) " +
                "values (:zodiac_id, :interval_id, :text);";
            command.Parameters.AddWithValue("zodiac_id", prediction.Zodiac.Id);
            command.Parameters.AddWithValue("interval_id", prediction.TimeInterval.Id);
            command.Parameters.AddWithValue("text", prediction.Text);
            command.ExecuteNonQuery();
        }

        public void Save(Zodiac zodiac)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"insert into {Zodiac.GetTableName()}({Zodiac.GetNameColumnName()}, {Zodiac.GetTypeColumnName()}) " +
                "values (:name, :type);";
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

        private bool IsTableEmpty(string tableName, string whereParameterName, int whereParameterValue)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from {tableName} where {whereParameterName} = :parameter;";
            command.Parameters.AddWithValue("parameter", whereParameterValue);
            return command.ExecuteReader().Read();
        }

        private bool IsZodiacAlreadySaved(Zodiac zodiac)
        {
            return IsTableEmpty(Zodiac.GetTableName(), Zodiac.GetTypeColumnName(), (int) zodiac.Type);
        }

        private bool IsTimeIntervalAlreadySaved(TimeInterval timeInterval)
        {
            return IsTableEmpty(TimeInterval.GetTableName(), TimeInterval.GetTypeColumnName(),
                (int) timeInterval.Type);
        }

        //TODO add isSaved for prediction
    }
}