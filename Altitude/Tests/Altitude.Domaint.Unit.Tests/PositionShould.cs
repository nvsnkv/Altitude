using System;
using Altitude.Domain;
using NUnit.Framework;

namespace Altitude.Domaint.Unit.Tests
{
    [TestFixture]
    public class PositionShould
    {
        [Test]
        public void BeEquatable()
        {
            var now = DateTime.Now;

            var position = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = now
            };

            var samePosition = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = now
            };

            var elderPosition = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = DateTime.FromFileTime(0)
            };

            var otherPosition = new Position
            {
                Latitude = 10,
                Longitude = 1,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = now
            };

            var anotherPosition = new Position
            {
                Latitude = 4,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = now
            };

            var yetAnotherPosition = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 3,

                Accuracy = new Accuracy(5, 3),
                Timestamp = now
            };

            var andAnotherOnePosition = new Position
            {
                Latitude = 4,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(2, 3),
                Timestamp = now
            };


            Assert.That(position.Equals(samePosition, ignoreTimestamp: true), Is.True);
            Assert.That(position.Equals(elderPosition, ignoreTimestamp: false), Is.False);
            Assert.That(position.Equals(otherPosition, ignoreTimestamp: true), Is.False);
            Assert.That(position.Equals(anotherPosition, ignoreTimestamp: true), Is.False);
            Assert.That(position.Equals(yetAnotherPosition, ignoreTimestamp: true), Is.False);
            Assert.That(position.Equals(andAnotherOnePosition, ignoreTimestamp: true), Is.False);
        }

        [Test]
        public void BeEquatableUpToTimestamp()
        {
            var position = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = DateTime.Now
            };

            var elderPosition = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = DateTime.FromFileTime(0)
            };

            Assert.That(position.Equals(elderPosition, ignoreTimestamp: true), Is.True);
            Assert.That(position.Equals(elderPosition, ignoreTimestamp: false), Is.False);
        }

        [Test]
        public void BePrintable()
        {
            var now = DateTime.Now;
            var position = new Position
            {
                Latitude = 10,
                Longitude = 10,
                Altitude = 5,

                Accuracy = new Accuracy(5, 3),
                Timestamp = now
            };

            Assert.That(position.ToString(), Is.EqualTo($@"""{now.ToUniversalTime().ToString("O")}"",""10"",""10"",""5"",""5"",""3"""));
        }

        [Test]
        public void BeParseable()
        {
            const string POSITION_STRING = @"""2005-08-09T18:31:42.201"",""10.5"",""9"",""8"",""7"",""6""";
            var expected = new Position()
            {
                Latitude = 10.5,
                Longitude = 9,
                Altitude = 8,
                Accuracy = new Accuracy(7, 6),
                Timestamp = DateTime.Parse("2005-08-09T18:31:42.201")
            };

            Position actual;
            var result = Position.TryParse(POSITION_STRING, out actual);
            Assert.That(result, Is.True);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}