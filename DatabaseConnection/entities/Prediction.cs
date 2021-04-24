namespace DatabaseConnection.entities
{
    public class Prediction
    {
        public Zodiac Zodiac { get; set; }

        public TimeInterval TimeInterval { get; set; }
        
        public string Text { get; set; }

        public Prediction(Zodiac zodiac, TimeInterval timeInterval, string text)
        {
            Zodiac = zodiac;
            TimeInterval = timeInterval;
            Text = text;
        }
    }
}