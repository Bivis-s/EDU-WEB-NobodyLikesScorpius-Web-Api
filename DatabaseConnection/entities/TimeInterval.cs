using System;
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TimeInterval) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, (int) Type);
        }

        private bool Equals(TimeInterval other)
        {
            return Name == other.Name && Type == other.Type;
        }
    }
}