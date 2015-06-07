using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Domain
{
    public struct Accuracy : IComparable<Accuracy>
    {
        public readonly double Horizontal;
        public readonly double Vertical;
        public static readonly Accuracy Zero = new Accuracy(0,0);

        public Accuracy(double horizontal, double vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public int CompareTo(Accuracy other)
        {
            return EqualsTo(other)
                        ? 0
                        : GetRadius() < other.GetRadius()
                                ? -1
                                : 1;
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(other, null)) return false;

            return (other is Accuracy) && EqualsTo((Accuracy)other);
        }

        public override int GetHashCode()
        {
            return Vertical.GetHashCode() ^ Horizontal.GetHashCode(); // safe dummy way
        }

        private bool EqualsTo(Accuracy other)
        {
            return Math.Abs(Vertical - other.Vertical) < double.Epsilon && Math.Abs(Horizontal - other.Horizontal) < double.Epsilon;
        }

        private double GetRadius()
        {
            return Math.Sqrt(Horizontal * Horizontal + Vertical * Vertical);
        }
    }
}
