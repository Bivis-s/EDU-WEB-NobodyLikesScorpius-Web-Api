
namespace TryToWebApi.objects
{
    public class Article
    {
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public ZodiacType ZodiacType { get; set; }

        public Article()
        {
        }

        public Article(string title, string content, ZodiacType zodiacType)
        {
            Title = title;
            Content = content;
            ZodiacType = zodiacType;
        }
    }
}