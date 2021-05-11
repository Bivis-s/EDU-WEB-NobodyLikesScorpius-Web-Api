using System;
using DatabaseConnection.attributes;

namespace DatabaseConnection.entities
{
    [TableName("admins")]
    public class Admin
    {
        public Admin(string name, string password)
        {
            Name = name;
            Password = password;
        }

        [SerializableName("name")] public string Name { get; }

        [SerializableName("password")] public string Password { get; }

        public static string GetTableName()
        {
            return TableName.GetTableName(typeof(Admin));
        }

        public static string GetNameColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Admin), "Name");
        }

        public static string GetPasswordColumnName()
        {
            return SerializableName.GetSerializableName(typeof(Admin), "Password");
        }

        private bool Equals(Admin other)
        {
            return Name == other.Name && Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Admin) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Password);
        }
    }
}