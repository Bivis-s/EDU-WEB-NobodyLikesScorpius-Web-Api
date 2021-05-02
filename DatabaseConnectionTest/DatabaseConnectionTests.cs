using DatabaseConnection;
using DatabaseConnection.db_connections;
using DatabaseConnection.entities;
using NUnit.Framework;
using TryToWebApi.objects;

namespace DatabaseConnectionTest
{
    public class DatabaseConnectionTests
    {
        private IDatabaseConnection _connection;
        private Zodiac _actualZodiac;
        private TimeInterval _actualTimeIntervals;

        [SetUp]
        public void Setup()
        {
            _connection = new ZodiacDbConnection();
        }

        [Test, Order(1)]
        public void SaveAndGetZodiac()
        {
            var expectedZodiac = new Zodiac("Scorpion", ZodiacType.Scorpius);
            _connection.Save(expectedZodiac);
            _actualZodiac = _connection.GetZodiac(ZodiacType.Scorpius);
            Assert.AreEqual(expectedZodiac, _actualZodiac,
                $"Returned from db Zodiac '{_actualZodiac}' is not equal to saved in db Zodiac '{expectedZodiac}'");
        }

        [Test, Order(2)]
        public void SaveAndGetTimeInterval()
        {
            var expectedTimeInterval = new TimeInterval("На сегодня", TimeIntervalType.Today);
            _connection.Save(expectedTimeInterval);
            _actualTimeIntervals = _connection.GetTimeIntervals(TimeIntervalType.Today);
            Assert.AreEqual(expectedTimeInterval, _actualTimeIntervals,
                $"Returned from db TimeIntervals '{_actualTimeIntervals}' didn't contain saved in db TimeInterval '{expectedTimeInterval}'");
        }

        [Test, Order(3)]
        public void SaveAndGetPrediction()
        {
            var expectedPrediction = new Prediction(_actualZodiac, _actualTimeIntervals,
                "Скорпионов потянет на подвиги — как производственные, так и личные. Год будет богат на путешествия. Каждое будет по-своему значимо. Лишних событий в год Белого Металлического Быка точно не предвидится. Но при всем при этом необходимо помнить об отдыхе. Своевременное чередование труда и расслабления позволит вам держать себя в тонусе и добиться значительной финансовой отдачи от своей деятельности.");
            _connection.Save(expectedPrediction);
            var actualPrediction = _connection.GetPredictions(_actualZodiac, _actualTimeIntervals);
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