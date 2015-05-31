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
    }
}
