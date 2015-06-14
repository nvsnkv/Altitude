using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Altitude.Domain
{
    public struct Accuracy : IComparable<Accuracy>
    {
        private static readonly Regex ParsePattern = new Regex(@"""(?<H>.+)""\,""(?<V>.+)""");

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

        public static bool TryParse(string s, out Accuracy result)
        {
            result = new Accuracy();
            var match = ParsePattern.Match(s);
            if (!match.Success)
                return false;

            double horizontal, vertical;
            if (double.TryParse(match.Groups["H"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out horizontal)
                && double.TryParse(match.Groups["V"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out vertical))
            {
                result = new Accuracy(horizontal,vertical);
                return true;
            }

            return false;
        }
    }
}
