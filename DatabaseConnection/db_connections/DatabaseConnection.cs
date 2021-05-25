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

        public bool IsSuchAdminPresent(Admin admin)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from {Admin.GetTableName()} " +
                                  $"where {Admin.GetNameColumnName()} = :name " +
                                  $"and {Admin.GetPasswordColumnName()} = :pass;";
            command.Parameters.AddWithValue("name", admin.Name);
            command.Parameters.AddWithValue("pass", admin.Password);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return command.ExecuteReader().Read();
        }

        private void ExecuteNonQuery(string commandText)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = commandText;
            Console.WriteLine("Execute SQL: " + command.CommandText);
            command.ExecuteNonQuery();
        }

        private bool IsTableEmpty(string tableName, string whereParameterName, int whereParameterValue)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText = $"select * from {tableName} where {whereParameterName} = :parameter;";
            command.Parameters.AddWithValue("parameter", whereParameterValue);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return command.ExecuteReader().Read();
        }

        #region Zodiacs

        public Zodiac GetZodiac(int id)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {Zodiac.GetIdColumnName()}, {Zodiac.GetNameColumnName()}, {Zodiac.GetTypeColumnName()} " +
                $"from {Zodiac.GetTableName()} " +
                $"where {Zodiac.GetIdColumnName()} = :id;";
            command.Parameters.AddWithValue("id", id);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return Factory.CreateZodiac(command.ExecuteReader());
        }

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
                Console.WriteLine("Execute SQL: " + command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        public Zodiac GetZodiac(ZodiacType zodiacType)
        {
            return Factory.CreateZodiac(GetZodiacDataReader(zodiacType));
        }

        public List<Zodiac> GetZodiacs()
        {
            return Factory.CreateZodiacList(GetZodiacsDataReader());
        }

        private SQLiteDataReader GetZodiacDataReader(ZodiacType zodiacType)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {Zodiac.GetIdColumnName()}, {Zodiac.GetNameColumnName()}, {Zodiac.GetTypeColumnName()} " +
                $"from {Zodiac.GetTableName()} " +
                $"where {Zodiac.GetTypeColumnName()} = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) zodiacType);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return command.ExecuteReader();
        }

        private SQLiteDataReader GetZodiacsDataReader()
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {Zodiac.GetIdColumnName()}, {Zodiac.GetNameColumnName()}, {Zodiac.GetTypeColumnName()} " +
                $"from {Zodiac.GetTableName()};";
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
                Console.WriteLine("Execute SQL: " + command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        public TimeInterval GetTimeInterval(TimeIntervalType timeIntervalType)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {TimeInterval.GetIdColumnName()}, {TimeInterval.GetNameColumnName()}, {TimeInterval.GetTypeColumnName()} " +
                $"from {TimeInterval.GetTableName()} " +
                $"where {TimeInterval.GetTypeColumnName()} = :enum_number;";
            command.Parameters.AddWithValue("enum_number", (int) timeIntervalType);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return Factory.CreateTimeInterval(command.ExecuteReader());
        }

        public List<TimeInterval> GetTimeIntervals()
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {TimeInterval.GetIdColumnName()}, {TimeInterval.GetNameColumnName()}, {TimeInterval.GetTypeColumnName()} " +
                $"from {TimeInterval.GetTableName()};";
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return Factory.CreateTimeIntervalList(command.ExecuteReader());
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
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
                Console.WriteLine("Execute SQL: " + command.CommandText);
                command.ExecuteNonQuery();
            }
        }

        public Prediction GetPrediction(ZodiacType zodiacType, TimeIntervalType timeIntervalType)
        {
            var zodiac = GetZodiac(zodiacType);
            var timeInterval = GetTimeInterval(timeIntervalType);
            return Factory.CreatePrediction(GetPredictionDataReader(zodiac, timeInterval), zodiac, timeInterval);
        }

        private SQLiteDataReader GetPredictionDataReader(Zodiac zodiac, TimeInterval timeInterval)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {Prediction.GetZodiacColumnName()}, {Prediction.GetTimeIntervalColumnName()}, {Prediction.GetTextColumnName()} from {Prediction.GetTableName()} " +
                $"where {Prediction.GetZodiacColumnName()} = :zodiac_id " +
                $"and {Prediction.GetTimeIntervalColumnName()} = :interval_id;";
            command.Parameters.AddWithValue("zodiac_id", zodiac.Id);
            command.Parameters.AddWithValue("interval_id", timeInterval.Id);
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
            Console.WriteLine("Execute SQL: " + command.CommandText);
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
            Console.WriteLine("Execute SQL: " + command.CommandText);
            command.ExecuteNonQuery();
        }

        #endregion

        #region Haircut

        public List<Haircut> GetHaircuts()
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {Haircut.GetIdColumnName()}, {Haircut.GetZodiacIdColumnName()}, {Haircut.GetMoonDayColumnName()}, " +
                $"{Haircut.GetMoonPhaseColumnName()}, {Haircut.GetPredictionColumnName()}, {Haircut.GetIsPositiveColumnName()} " +
                $"from {Haircut.GetTableName()};";
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return Factory.CreateHaircutList(command.ExecuteReader(), this);
        }

        public void SaveOrUpdate(Haircut haircut)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"update {Haircut.GetTableName()} " +
                $"set {Haircut.GetZodiacIdColumnName()} = :zodiacId, " +
                $"{Haircut.GetMoonDayColumnName()} = :moonDay, " +
                $"{Haircut.GetMoonPhaseColumnName()} = :moonPhase, " +
                $"{Haircut.GetPredictionColumnName()} = :prediction, " +
                $"{Haircut.GetIsPositiveColumnName()} = :isPositive " +
                $"where {Haircut.GetIdColumnName()} = :id;";
            command.Parameters.AddWithValue("zodiacId", (int) haircut.Zodiac.Type);
            command.Parameters.AddWithValue("moonDay", haircut.MoonDay);
            command.Parameters.AddWithValue("moonPhase", haircut.MoonPhase);
            command.Parameters.AddWithValue("prediction", haircut.Prediction);
            command.Parameters.AddWithValue("isPositive", haircut.IsPositive);
            command.Parameters.AddWithValue("id", haircut.Id);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            command.ExecuteNonQuery();
        }

        #endregion

        #region Compatibilities

        public List<Compatibility> GetCompatibilities()
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"select {Compatibility.GetZodiac1ColumnName()}, {Compatibility.GetZodiac2ColumnName()}, " +
                $"{Compatibility.GetCompatibilityValueColumnName()}, {Compatibility.GetTextValueColumnName()} " +
                $"from {Compatibility.GetTableName()};";
            Console.WriteLine("Execute SQL: " + command.CommandText);
            return Factory.CreateCompatibilityList(command.ExecuteReader(), this);
        }

        public void SaveOrUpdate(Compatibility compatibility)
        {
            using var command = _connection.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                $"update {Compatibility.GetTableName()} " +
                $"set {Compatibility.GetCompatibilityValueColumnName()} = :compatibilityValue, " +
                $"{Compatibility.GetTextValueColumnName()} = :textValue " +
                $"where {Compatibility.GetZodiac1ColumnName()} = zodiacId1: " +
                $"and  {Compatibility.GetZodiac1ColumnName()} = :zodiacId2;";
            command.Parameters.AddWithValue("compatibilityValue", compatibility.CompatibilityValue);
            command.Parameters.AddWithValue("textValue", compatibility.TextValue);
            command.Parameters.AddWithValue("zodiacId1", compatibility.Zodiac1.Id);
            command.Parameters.AddWithValue("zodiacId2", compatibility.Zodiac2.Id);
            Console.WriteLine("Execute SQL: " + command.CommandText);
            command.ExecuteNonQuery();
        }

        #endregion
    }
}