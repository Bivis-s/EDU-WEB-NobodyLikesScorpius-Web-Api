using TryToWebApi.objects;

namespace DatabaseConnection.entities
{
    public class Zodiac
    {
        public int Id { get; }
        public string Name { get; set; }
        public ZodiacType Type { get; set; }

        public Zodiac(int id, string name, ZodiacType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public Zodiac(string name, ZodiacType type)
        {
            Name = name;
            Type = type;
        }
    }
}