using DatabaseConnection.db_connections;
using NUnit.Framework;

namespace DatabaseConnectionTest
{
    [TestFixture]
    public class DatabaseManagerTest
    {
        [Test]
        public void GetSqlConnection()
        {
            Assert.NotNull(DatabaseConnectionManager.GetSqlConnection());
        }
    }
}