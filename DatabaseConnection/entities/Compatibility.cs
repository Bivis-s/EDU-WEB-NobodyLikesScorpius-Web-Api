using System;
using DatabaseConnection.attributes;

namespace DatabaseConnection.entities
{
    [TableName("compatibilities")]
    public class Compatibility
    {
        public Compatibility(Zodiac zodiac1, Zodiac zodiac2, int compatibilityValue, string textValue)
        {
            Zodiac1 = zodiac1;
            Zodiac2 = zodiac2;
            CompatibilityValue = compatibilityValue;
            TextValue = textValue;
        }

        [SerializableName("zodiac1_type")] public Zodiac Zodiac1 { get; set; }

        [SerializableName("zodiac2_type")] public Zodiac Zodiac2 { get; set; }

        [SerializableName("compatibility_value")]
        public int CompatibilityValue { get; set; }

        [SerializableName("text_value")] public string TextValue { get; set; }

        private bool Equals(Compatibility other)
        {
            return Equals(Zodiac1, other.Zodiac1) && Equals(Zodiac2, other.Zodiac2) &&
                   CompatibilityValue == other.CompatibilityValue && TextValue == other.TextValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Compatibility) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Zodiac1, Zodiac2, CompatibilityValue, TextValue);
        }

        public override string ToString()
        {
            return
                $"{nameof(Zodiac1)}: {Zodiac1}, {nameof(Zodiac2)}: {Zodiac2}, {nameof(CompatibilityValue)}: {CompatibilityValue}, {nameof(TextValue)}: {TextValue}";
        }

        public static string GetTableName()
        {
            return TableName.GetTableName(typeof(Compatibility));
        }

        public static string GetZodiac1ColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Compatibility), "Zodiac1");
        }

        public static string GetZodiac2ColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Compatibility), "Zodiac2");
        }

        public static string GetCompatibilityValueColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Compatibility), "CompatibilityValue");
        }

        public static string GetTextValueColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Compatibility), "TextValue");
        }
    }
}