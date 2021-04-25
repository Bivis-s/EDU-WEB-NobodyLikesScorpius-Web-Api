using System;
using System.Collections.Generic;
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

        [SerializableName("name")] public string Name { get; set; }

        [SerializableName("enum_number")] public ZodiacType Type { get; set; }

        private bool Equals(Zodiac other)
        {
            return Id == other.Id && Name == other.Name && Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Zodiac) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, (int) Type);
        }
    }
}