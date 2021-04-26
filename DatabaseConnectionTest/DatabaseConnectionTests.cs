using DatabaseConnection.db_connections;
using DatabaseConnection.entities;
using NUnit.Framework;
using TryToWebApi.objects;

namespace DatabaseConnectionTest
{
    public class DatabaseConnectionTests
    {
        private IDatabaseConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new DatabaseConnection.db_connections.DatabaseConnection();
        }

        [Test]
        public void SaveAndGetZodiac()
        {
            var expectedZodiac = new Zodiac("Scorpion", ZodiacType.Scorpius);
            _connection.Save(expectedZodiac);
            var actualZodiac = _connection.GetZodiac(ZodiacType.Scorpius);
            Assert.True(actualZodiac.EqualsWithoutId(expectedZodiac),
                $"Returned from db Zodiac '{actualZodiac}' is not equal to saved in db Zodiac '{expectedZodiac}'");
        }

        [TearDown]
        public void Teardown()
        {
            //TODO clear methods
        }
    }
}