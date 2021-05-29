namespace TryToWebApi.objects
{
    public class UpdateCompatibilityRequest
    {
        public int ZodiacType1 { get; set; }

        public int ZodiacType2 { get; set; }

        public int CompatibilityValue { get; set; }

        public string Text { get; set; }

        public string SessionToken { get; set; }

        public override string ToString()
        {
            return $"{nameof(ZodiacType1)}: {ZodiacType1}, {nameof(ZodiacType2)}: {ZodiacType2}, " +
                   $"{nameof(CompatibilityValue)}: {CompatibilityValue}, {nameof(Text)}: {Text}, " +
                   $"{nameof(SessionToken)}: {SessionToken}";
        }
    }
}