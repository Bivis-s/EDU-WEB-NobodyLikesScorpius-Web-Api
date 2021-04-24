using DatabaseConnection.attributes;
using TryToWebApi.objects;

namespace DatabaseConnection.entities
{
    [TableName("time_intervals")]
    public class TimeInterval
    {
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

        [SerializableName("id")] public int Id { get; }

        [SerializableName("name")] public string Name { get; set; }

        [SerializableName("enum_number")] public TimeIntervalType Type { get; set; }
    }
}