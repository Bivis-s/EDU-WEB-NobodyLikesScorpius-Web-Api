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
    }
}