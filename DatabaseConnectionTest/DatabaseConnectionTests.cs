using DatabaseConnection;
using DatabaseConnection.db_connections;
using DatabaseConnection.entities;
using NUnit.Framework;
using TryToWebApi.objects;

namespace DatabaseConnectionTest
{
    public class DatabaseConnectionTests
    {
        private TimeInterval _actualTimeIntervals;
        private Zodiac _actualZodiac;
        private IDatabaseConnection _connection;

        [SetUp]
        public void Setup()
        {
            _connection = new ZodiacDbConnection();
        }

        [Test]
        [Order(1)]
        public void SaveAndGetZodiac()
        {
            var expectedZodiac = new Zodiac("Scorpius", ZodiacType.Scorpius);
            _connection.SaveOrUpdate(expectedZodiac);
            _actualZodiac = _connection.GetZodiac(ZodiacType.Scorpius);
            Assert.AreEqual(expectedZodiac, _actualZodiac,
                $"Returned from db Zodiac '{_actualZodiac}' is not equal to saved in db Zodiac '{expectedZodiac}'");
        }

        [Test]
        [Order(2)]
        public void SaveAndGetTimeInterval()
        {
            var expectedTimeInterval = new TimeInterval("На сегодня", TimeIntervalType.Today);
            _connection.SaveOrUpdate(expectedTimeInterval);
            _actualTimeIntervals = _connection.GetTimeInterval(TimeIntervalType.Today);
            Assert.AreEqual(expectedTimeInterval, _actualTimeIntervals,
                $"Returned from db TimeIntervals '{_actualTimeIntervals}' didn't contain saved in db TimeInterval '{expectedTimeInterval}'");
        }

        [Test]
        [Order(3)]
        public void SaveAndGetPrediction()
        {
            var expectedPrediction = new Prediction(_actualZodiac, _actualTimeIntervals,
                "Скорпионов 4потянет на подвиги — как производственные, так и личные. Год будет богат на путешествия. Каждое будет по-своему значимо. Лишних событий в год Белого Металлического Быка точно не предвидится. Но при всем при этом необходимо помнить об отдыхе. Своевременное чередование труда и расслабления позволит вам держать себя в тонусе и добиться значительной финансовой отдачи от своей деятельности.");
            _connection.SaveOrUpdate(expectedPrediction);
            var actualPrediction = _connection.GetPrediction(_actualZodiac.Type, _actualTimeIntervals.Type);
            Assert.AreEqual(expectedPrediction, actualPrediction,
                $"Returned from db Prediction '{actualPrediction}' isn't equal to saved in db Prediction '{expectedPrediction}'");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            _connection.ClearAll();
        }
    }
}