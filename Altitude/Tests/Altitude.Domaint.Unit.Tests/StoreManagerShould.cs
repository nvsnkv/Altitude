using Altitude.Domain;
using Altitude.Domain.Storage;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Domaint.Unit.Tests
{
    [TestFixture]
    public class StoreManagerShould
    {
        [Test]
        public void NotAddPositionsWithAccuracy_GreaterThanDesired()
        {
            var storeMock = new Mock<ICollection<Position>>();
            storeMock.Setup(s => s.Add(It.IsAny<Position>())).Verifiable();

            var desiredAccuracy = new Accuracy { Horizontal = 5, Vertical = 5 };

            var storeManager = new StorageManager(storeMock.Object, desiredAccuracy);

            var badPosition = new Position
            {
                Accuracy = new Accuracy
                {
                    Horizontal = 5,
                    Vertical = 6,
                }
            };

            var goodPosition = new Position
            {
                Accuracy = new Accuracy
                {
                    Horizontal = 3,
                    Vertical = 4
                }
            };

            var badResult = storeManager.Add(badPosition);
            var goodResult = storeManager.Add(goodPosition);

            Assert.That(badResult, Is.False);
            Assert.That(goodResult, Is.True);
            storeMock.Verify(s => s.Add(It.IsAny<Position>()), Times.Once);
        }

        [Test]
        public void ThrowsArgumentNullExceptionForNullCollection()
        {
            Assert.Throws<ArgumentNullException>(
                () => new StorageManager(null, new Accuracy())
            );
        }

        [Test]
        public void ThrowsArgumentOutOfRangeExceptionForZeroAccuracy()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new StorageManager(new Mock<ICollection<Position>>().Object, new Accuracy() { Horizontal = 0, Vertical = 0})
            );
        }

        [Test]
        public void ThrowsArgumentOutOfRangeExceptionForNegativeAccuracy()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new StorageManager(new Mock<ICollection<Position>>().Object, new Accuracy() { Horizontal = -1, Vertical = -0.73 })
            );
        }
    }
}


