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

        #region Zodiacs

        public void SaveOrUpdate(Zodiac zodiac)
        {
            if (IsZodiacAlreadySaved(zodiac))
            {
                Update(zodiac);
            }
            else
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
        }

        public Zodiac GetZodiac(ZodiacType zodiacType)
        {
            return Factory.CreateZodiac(GetZodiacDataReader(zodiacType));
        }

        private SQLiteDataReader GetZodiacDataReader(ZodiacType zodiacType)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select id, name, enum_number from {Zodiac.GetTableName()} " +
                $"where {Zodiac.GetTypeColumnName()} = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) zodiacType);
            return command.ExecuteReader();
        }

        private bool IsZodiacAlreadySaved(Zodiac zodiac)
        {
            return IsTableEmpty(Zodiac.GetTableName(), Zodiac.GetTypeColumnName(), (int) zodiac.Type);
        }

        private void Delete(Zodiac zodiac)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"delete from {Zodiac.GetTableName()} " +
                $"where {Zodiac.GetTypeColumnName()} = :enum_number;";
            command.Parameters.AddWithValue("zodiac_id", (int) zodiac.Type);
            command.ExecuteNonQuery();
        }

        private void Update(Zodiac zodiac)
        {
            var id = zodiac.Id;
            if (id == 0) id = GetZodiac(zodiac.Type).Id;

            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"update {Zodiac.GetTableName()} " +
                $"set {Zodiac.GetNameColumnName()} = :new_name, {Zodiac.GetTypeColumnName()} = :new_enum_number " +
                "where id = :id;";
            command.Parameters.AddWithValue("new_name", zodiac.Name);
            command.Parameters.AddWithValue("new_enum_number", (int) zodiac.Type);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }

        #endregion

        #region TimeIntervals

        public void SaveOrUpdate(TimeInterval timeInterval)
        {
            if (IsTimeIntervalAlreadySaved(timeInterval))
            {
                Update(timeInterval);
            }
            else
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
        }

        public TimeInterval GetTimeInterval(TimeIntervalType timeIntervalType)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = "select id, name, enum_number " +
                                  "from time_intervals " +
                                  "where enum_number = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) timeIntervalType);
            return Factory.CreateTimeInterval(command.ExecuteReader());
        }

        private bool IsTimeIntervalAlreadySaved(TimeInterval timeInterval)
        {
            return IsTableEmpty(TimeInterval.GetTableName(), TimeInterval.GetTypeColumnName(),
                (int) timeInterval.Type);
        }

        private void Delete(TimeInterval timeInterval)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"delete from {TimeInterval.GetTableName()} " +
                $"where {TimeInterval.GetTypeColumnName()} = :enum_number;";
            command.Parameters.AddWithValue("zodiac_id", (int) timeInterval.Type);
            command.ExecuteNonQuery();
        }

        private void Update(TimeInterval timeInterval)
        {
            var id = timeInterval.Id;
            if (id == 0) id = GetTimeInterval(timeInterval.Type).Id;

            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"update {TimeInterval.GetTableName()} " +
                $"set {TimeInterval.GetNameColumnName()} = :new_name, {TimeInterval.GetTypeColumnName()} = :new_enum_number " +
                "where id = :id;";
            command.Parameters.AddWithValue("new_name", timeInterval.Name);
            command.Parameters.AddWithValue("new_enum_number", (int) timeInterval.Type);
            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();
        }

        #endregion

        #region Predictions

        public void SaveOrUpdate(Prediction prediction)
        {
            if (IsPredictionAlreadySaved(prediction))
            {
                Update(prediction);
            }
            else
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
        }

        public Prediction GetPrediction(Zodiac zodiac, TimeInterval timeInterval)
        {
            return Factory.CreatePrediction(GetPredictionDataReader(zodiac, timeInterval), zodiac, timeInterval);
        }

        private SQLiteDataReader GetPredictionDataReader(Zodiac zodiac, TimeInterval timeInterval)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select zodiac_id, time_interval_id, text_value from {Prediction.GetTableName()} " +
                $"where {Prediction.GetZodiacColumnName()} = :zodiac_id " +
                $"and {Prediction.GetTimeIntervalColumnName()} = :interval_id;";
            command.Parameters.AddWithValue("zodiac_id", zodiac.Id);
            command.Parameters.AddWithValue("interval_id", timeInterval.Id);
            return command.ExecuteReader();
        }

        private bool IsPredictionAlreadySaved(Prediction prediction)
        {
            return GetPredictionDataReader(prediction.Zodiac, prediction.TimeInterval).Read();
        }

        private void Delete(Prediction prediction)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"delete from {Prediction.GetTableName()} " +
                $"where {Prediction.GetZodiacColumnName()} = :zodiac_id " +
                $"and {Prediction.GetTimeIntervalColumnName()} = :interval_id;";
            command.Parameters.AddWithValue("zodiac_id", prediction.Zodiac.Id);
            command.Parameters.AddWithValue("interval_id", prediction.TimeInterval.Id);
            command.ExecuteNonQuery();
        }

        private void Update(Prediction prediction)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"update {Prediction.GetTableName()} " +
                $"set {Prediction.GetTextColumnName()} = :new_text " +
                $"where {Prediction.GetZodiacColumnName()} = :zodiac_id " +
                $"and {Prediction.GetTimeIntervalColumnName()} = :time_interval_id;";
            command.Parameters.AddWithValue("new_text", prediction.Text);
            command.Parameters.AddWithValue("zodiac_id", prediction.Zodiac.Id);
            command.Parameters.AddWithValue("time_interval_id", prediction.TimeInterval.Id);

            command.ExecuteNonQuery();
        }

        #endregion
    }
}