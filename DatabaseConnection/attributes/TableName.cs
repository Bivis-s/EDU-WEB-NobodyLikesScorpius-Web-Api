using System;

namespace DatabaseConnection.attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableName : Attribute
    {
        public TableName(string value)
        {
            Value = value;
        }

        private string Value { get; set; }
        
        public static string GetTableName(Type type)
        {
            var attribute = (TableName) GetCustomAttribute(type, typeof(TableName));
            if (attribute != null)
            {
                return attribute.Value;
            }
            throw new ArgumentException($"{type.FullName} has no attribute TableName");
        }
    }
}