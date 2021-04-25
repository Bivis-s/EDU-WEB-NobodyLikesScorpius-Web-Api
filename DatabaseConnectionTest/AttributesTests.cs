using DatabaseConnection.attributes;
using DatabaseConnection.entities;
using NUnit.Framework;
using TryToWebApi.objects;

namespace DatabaseConnectionTest
{
    [TestFixture]
    public class AttributesTests
    {
        private Zodiac _zodiac;

        [SetUp]
        public void Setup()
        {
            _zodiac = new Zodiac(1, "Aquarius", ZodiacType.Aquarius);
        }

        [Test]
        public void GetSerializableName()
        {
            var expectedSerializableName = "enum_number";
            var actualSerializableName = SerializableName.GetSerializableName(_zodiac.GetType(), "Type");
            Assert.AreEqual(expectedSerializableName, actualSerializableName);
        }

        [Test]
        public void GetTableName()
        {
            var expectedTableName = "zodiacs";
            var actualTableName = TableName.GetTableName(_zodiac.GetType());
            Assert.AreEqual(expectedTableName, actualTableName);
        }
    }
}