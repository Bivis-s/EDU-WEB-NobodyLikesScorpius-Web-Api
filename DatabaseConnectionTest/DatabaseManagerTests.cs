using DatabaseConnection.db_connections;
using NUnit.Framework;

namespace DatabaseConnectionTest
{
    [TestFixture]
    public class DatabaseManagerTests
    {
        [Test]
        public void GetSqlConnection()
        {
            Assert.NotNull(DatabaseConnectionManager.GetSqlConnection());
        }
    }
}