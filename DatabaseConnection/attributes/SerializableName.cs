using System;
using System.Reflection;

namespace DatabaseConnection.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializableName : Attribute
    {
        public SerializableName(string value)
        {
            Value = value;
        }

        private string Value { get; }

        public static string GetSerializableName(Type type, string propertyName)
        {
            var attribute = type
                .GetProperty(propertyName)?
                .GetCustomAttribute<SerializableName>()?
                .Value;

            if (attribute != null) return attribute;

            throw new ArgumentException(
                $"Property {propertyName} in type {type.Name} has no attribute SerializableName or such property doesn't exists");
        }
    }
}