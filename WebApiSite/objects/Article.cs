namespace TryToWebApi.objects
{
    public class Article
    {
        public Article()
        {
        }

        public Article(string title, string content, ZodiacType zodiacType)
        {
            Title = title;
            Content = content;
            ZodiacType = zodiacType;
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public ZodiacType ZodiacType { get; set; }
    }
}