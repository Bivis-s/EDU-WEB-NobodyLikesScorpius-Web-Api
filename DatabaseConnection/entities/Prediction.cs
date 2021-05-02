using System;
using DatabaseConnection.attributes;

namespace DatabaseConnection.entities
{
    [TableName("predictions")]
    public class Prediction
    {
        public Prediction(Zodiac zodiac, TimeInterval timeInterval, string text)
        {
            Zodiac = zodiac;
            TimeInterval = timeInterval;
            Text = text;
        }

        [SerializableName("zodiac_id")] public Zodiac Zodiac { get; set; }

        [SerializableName("time_interval_id")] public TimeInterval TimeInterval { get; set; }

        [SerializableName("text_value")] public string Text { get; set; }

        public static string GetTableName()
        {
            return TableName.GetTableName(typeof(Prediction));
        }

        public static string GetZodiacColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Prediction), "Zodiac");
        }

        public static string GetTimeIntervalColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Prediction), "TimeInterval");
        }

        public static string GetTextColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Prediction), "Text");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Prediction) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TimeInterval, Text);
        }

        private bool Equals(Prediction other)
        {
            return Equals(TimeInterval, other.TimeInterval) && Text == other.Text;
        }
    }
}