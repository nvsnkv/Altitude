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
            return Equals(other)
                        ? 0
                        : GetRadius() < other.GetRadius()
                                ? -1
                                : 1;
        }

        private double GetRadius()
        {
            return Math.Sqrt(Horizontal * Horizontal + Vertical * Vertical);
        }

        public override string ToString()
        {
            return $@"""{Horizontal}"",""{Vertical}""";
        }
    }
}
