using DatabaseConnection.db_connections;
using DatabaseConnection.entities;
using NUnit.Framework;
using TryToWebApi.objects;

namespace DatabaseConnectionTest
{
    public class DatabaseTests
    {
        private IDatabaseConnection _connection;

            [SetUp]
        public void Setup()
        {
            _connection = new DatabaseConnection.db_connections.DatabaseConnection();
        }

        [Test]
        public void GetZodiac()
        {
            var expected = new Zodiac(1, "Aquarius", ZodiacType.Aquarius);
            var actual = _connection.GetZodiac(ZodiacType.Aquarius);
            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void Teardown()
        {
        }
    }
}