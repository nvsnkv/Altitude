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
            var someAccuracy = new Accuracy { Horizontal = 3, Vertical = 3 };
            
            Assert.That(someAccuracy, Is.EqualTo(new Accuracy { Horizontal = 3, Vertical = 3 }));
            Assert.That(someAccuracy, Is.Not.EqualTo(new Accuracy { Horizontal = 3, Vertical = 5 }));
            Assert.That(someAccuracy, Is.Not.EqualTo(new Accuracy { Horizontal = 5, Vertical = 3 }));
            Assert.That(someAccuracy, Is.Not.EqualTo(new Accuracy { Horizontal = 3.2, Vertical = 5.4 }));
        }
        
        [Test]
        public void BeComparable()
        {
            var someAccuracy = new Accuracy { Horizontal = 2, Vertical = 2 };
            Assert.That(someAccuracy.CompareTo(new Accuracy { Horizontal = 2.73, Vertical = 2.73 }), Is.EqualTo(-1)); // less
            Assert.That(someAccuracy.CompareTo(new Accuracy { Horizontal = 2, Vertical = 2 }), Is.EqualTo(0)); // equals
            Assert.That(someAccuracy.CompareTo(new Accuracy { Horizontal = 1.9, Vertical = 1.5 }), Is.EqualTo(1)); // greater
        }
    }

}
