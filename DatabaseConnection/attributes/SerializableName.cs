using System;

namespace DatabaseConnection.attributes
{
    public class SerializableName : Attribute
    {
        public SerializableName(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}