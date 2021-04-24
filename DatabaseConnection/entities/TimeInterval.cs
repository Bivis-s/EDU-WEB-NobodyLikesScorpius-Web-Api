using TryToWebApi.objects;

namespace DatabaseConnection.entities
{
    public class TimeInterval
    {
        public int Id { get; }
        
        public string Name { get; set; }
        
        public TimeIntervalType Type { get; set; }

        public TimeInterval(int id, string name, TimeIntervalType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public TimeInterval(string name, TimeIntervalType type)
        {
            Name = name;
            Type = type;
        }
    }
}