using System;
using DatabaseConnection.attributes;
using TryToWebApi.objects;

namespace DatabaseConnection.entities
{
    [TableName("zodiacs")]
    public class Zodiac
    {
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

        [SerializableName("id")] public int Id { get; }

        [SerializableName("name")] public string Name { get; }

        [SerializableName("enum_number")] public ZodiacType Type { get; }

        public static string GetTableName()
        {
            return TableName.GetTableName(typeof(Zodiac));
        }

        public static string GetIdColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Zodiac), "Id");
        }

        public static string GetNameColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Zodiac), "Name");
        }

        public static string GetTypeColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Zodiac), "Type");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Zodiac) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, (int) Type);
        }

        private bool Equals(Zodiac other)
        {
            return Name == other.Name && Type == other.Type;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Type)}: {Type}";
        }
    }
}