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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Prediction) obj);
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