using System;
using DatabaseConnection.attributes;

namespace DatabaseConnection.entities
{
    [TableName("haircuts")]
    public class Haircut
    {
        public Haircut(int id, Zodiac zodiac, string moonDay, string moonPhase, string prediction, bool isPositive)
        {
            Id = id;
            Zodiac = zodiac;
            MoonDay = moonDay;
            MoonPhase = moonPhase;
            Prediction = prediction;
            IsPositive = isPositive;
        }

        public Haircut(Zodiac zodiac, string moonDay, string moonPhase, string prediction, bool isPositive)
        {
            Zodiac = zodiac;
            MoonDay = moonDay;
            MoonPhase = moonPhase;
            Prediction = prediction;
            IsPositive = isPositive;
        }

        [SerializableName("id")] public int Id { get; }

        [SerializableName("zodiac_type")] public Zodiac Zodiac { get; set; }

        [SerializableName("moon_day")] public string MoonDay { get; set; }

        [SerializableName("moon_phase")] public string MoonPhase { get; set; }

        [SerializableName("prediction")] public string Prediction { get; set; }

        [SerializableName("is_positive")] public bool IsPositive { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Haircut) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Zodiac, MoonDay, MoonPhase, Prediction, IsPositive);
        }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(Zodiac)}: {Zodiac}, {nameof(MoonDay)}: {MoonDay}, {nameof(MoonPhase)}: {MoonPhase}, {nameof(Prediction)}: {Prediction}, {nameof(IsPositive)}: {IsPositive}";
        }

        private bool Equals(Haircut other)
        {
            return Equals(Zodiac, other.Zodiac) && MoonDay == other.MoonDay && MoonPhase == other.MoonPhase &&
                   Prediction == other.Prediction && IsPositive == other.IsPositive;
        }

        #region GetSerializableName

        public static string GetTableName()
        {
            return TableName.GetTableName(typeof(Haircut));
        }

        public static string GetIdColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Haircut), "Id");
        }

        public static string GetZodiacIdColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Haircut), "Zodiac");
        }

        public static string GetMoonDayColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Haircut), "MoonDay");
        }

        public static string GetMoonPhaseColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Haircut), "MoonPhase");
        }

        public static string GetPredictionColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Haircut), "Prediction");
        }

        public static string GetIsPositiveColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Haircut), "IsPositive");
        }

        #endregion
    }
}