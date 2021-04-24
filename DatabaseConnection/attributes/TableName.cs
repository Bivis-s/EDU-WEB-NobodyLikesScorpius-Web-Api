using System;

namespace DatabaseConnection.attributes
{
    public class TableName : Attribute
    {
        public TableName(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}