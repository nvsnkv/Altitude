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
    }
}