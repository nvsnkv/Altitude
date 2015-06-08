using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Altitude.Domain
{
    public struct Position
    {
        public double Latitude;
        public double Longitude;
        public double Altitude;

        public Accuracy Accuracy;

        public DateTime Timestamp;

        public bool Equals(Position other, bool ignoreTimestamp)
        {
            return Math.Abs(Latitude - other.Latitude) < double.Epsilon
                   && Math.Abs(Longitude - other.Longitude) < double.Epsilon
                   && Math.Abs(Altitude - other.Altitude) < double.Epsilon
                   && Accuracy.Equals(other.Accuracy)
                   && (ignoreTimestamp || Timestamp.Equals(other.Timestamp));
        }
    }
}
