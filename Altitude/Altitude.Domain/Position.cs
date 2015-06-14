using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Altitude.Domain
{
    public struct Position
    {
        private static readonly Regex ParsePattern = new Regex(@"""(?<timestamp>[^""]*)"",""(?<lat>[^""]*)"",""(?<lon>[^""]*)"",""(?<alt>[^""]*)"",(?<acc>.*)$");
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

        public override string ToString()
        {
            return $@"""{Timestamp.ToUniversalTime().ToString("O")}"",""{Latitude}"",""{Longitude}"",""{Altitude}"",{Accuracy}";
        }

        public static bool TryParse(string s, out Position position)
        {
            position = new Position();
            var match = ParsePattern.Match(s);
            if (match.Success)
            {
                double lat, lon, alt;
                Accuracy acc;
                DateTime timestamp;

                if (DateTime.TryParse(match.Groups["timestamp"].Value, out timestamp)
                    && double.TryParse(match.Groups["lat"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out lat)
                    && double.TryParse(match.Groups["lon"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out lon)
                    && double.TryParse(match.Groups["alt"].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out alt)
                    && Accuracy.TryParse(match.Groups["acc"].Value, out acc))
                {
                    position = new Position()
                    {
                        Latitude = lat,
                        Longitude = lon,
                        Altitude = alt,
                        Accuracy = acc,
                        Timestamp = timestamp
                    };

                    return true;
                }
            }

            return false;
        }
    }
}
