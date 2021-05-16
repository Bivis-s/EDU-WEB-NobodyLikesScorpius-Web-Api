namespace TryToWebApi.objects
{
    public class UpdatePredictionRequest
    {
        public string Zodiac { get; set; }
        public string TimeInterval { get; set; }
        public string Text { get; set; }
        public string SessionToken { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Zodiac)}: {Zodiac}, {nameof(TimeInterval)}: {TimeInterval}, {nameof(Text)}: {Text}, {nameof(SessionToken)}: {SessionToken}";
        }
    }
}