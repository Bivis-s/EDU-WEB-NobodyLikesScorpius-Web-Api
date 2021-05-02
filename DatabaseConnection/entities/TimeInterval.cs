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

        [SerializableName("name")] public string Name { get; }

        [SerializableName("enum_number")] public TimeIntervalType Type { get; }

        public static string GetTableName()
        {
            return TableName.GetTableName(typeof(TimeInterval));
        }

        public static string GetNameColumnName()
        {
            return SerializableName.GetSerializableName(typeof(TimeInterval), "Name");
        }

        public static string GetTypeColumnName()
        {
            return SerializableName.GetSerializableName(typeof(TimeInterval), "Type");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TimeInterval) obj);
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