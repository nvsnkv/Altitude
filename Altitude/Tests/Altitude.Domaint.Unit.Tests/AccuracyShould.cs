using Altitude.Domain;
using NUnit.Framework;

namespace Altitude.Domaint.Unit.Tests
{
    [TestFixture]
    public class AccuracyShould
    {
        [Test]
        public void BeEquatable()
        {
            var someAccuracy = new Accuracy(3, 3);
            
            Assert.That(someAccuracy, Is.EqualTo(new Accuracy(3, 3)));
            Assert.That(someAccuracy, Is.Not.EqualTo(new Accuracy(3, 5)));
            Assert.That(someAccuracy, Is.Not.EqualTo(new Accuracy(5, 3)));
            Assert.That(someAccuracy, Is.Not.EqualTo(new Accuracy(3.2, 5.4)));
        }
        
        [Test]
        public void BeComparable()
        {
            var someAccuracy = new Accuracy(2, 2);
            Assert.That(someAccuracy.CompareTo(new Accuracy(2.73, 2.73)), Is.EqualTo(-1)); // less
            Assert.That(someAccuracy.CompareTo(new Accuracy(2, 2)), Is.EqualTo(0)); // equals
            Assert.That(someAccuracy.CompareTo(new Accuracy(1.9, 1.50)), Is.EqualTo(1)); // greater
        }

        [Test]
        public void BePrintable()
        {
            var someAccuracy = new Accuracy(2, 2);
            Assert.That(someAccuracy.ToString(), Is.EqualTo(@"""2"",""2"""));
        }

        [Test]
        public void BeParseable()
        {
            const string ACCURACY_STRING = @"""2"",""2""";
            var expected = new Accuracy(2,2);
            Accuracy actual;

            var result = Accuracy.TryParse(ACCURACY_STRING, out actual);

            Assert.That(result, Is.True);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }

}
