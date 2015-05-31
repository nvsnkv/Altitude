using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Domain
{
    public struct Accuracy : IComparable<Accuracy>
    {
        public double Horizontal;
        public double Vertical;

        public int CompareTo(Accuracy other)
        {
            return this.EqualsTo(other)
                        ? 0
                        : GetRadius() < other.GetRadius()
                                ? -1
                                : 1;
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return (other is Accuracy) && this.EqualsTo((Accuracy)other);
        }

        public override int GetHashCode()
        {
            return Vertical.GetHashCode() ^ Horizontal.GetHashCode(); // safe dummy way
        }

        private bool EqualsTo(Accuracy other)
        {
            return Vertical == other.Vertical && Horizontal == other.Horizontal;
        }

        private double GetRadius()
        {
            return Math.Sqrt(Horizontal * Horizontal + Vertical * Vertical);
        }
    }
}
