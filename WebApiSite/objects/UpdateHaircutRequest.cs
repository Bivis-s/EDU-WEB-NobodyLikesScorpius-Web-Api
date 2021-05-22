namespace TryToWebApi.objects
{
    public class UpdateHaircutRequest
    {
        public int Id { get; set; }

        public int ZodiacId { get; set; }

        public string MoonDay { get; set; }

        public string MoonPhase { get; set; }

        public string Prediction { get; set; }

        public string IsPositive { get; set; }

        public string SessionToken { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(ZodiacId)}: {ZodiacId}, {nameof(MoonDay)}: {MoonDay}, {nameof(MoonPhase)}: {MoonPhase}, {nameof(Prediction)}: {Prediction}, {nameof(IsPositive)}: {IsPositive}, {nameof(SessionToken)}: {SessionToken}";
        }
    }
}