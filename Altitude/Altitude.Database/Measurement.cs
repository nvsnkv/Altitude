using System;
using System.ComponentModel.DataAnnotations.Schema;
using Altitude.Domain;

namespace Altitude.Database
{
    public class Measurement
    {
        public Measurement() { }

        public Measurement(Position p)
        {
            Latitude = p.Latitude;
            Longitude = p.Longitude;
            Altitude = p.Altitude;
            HorizontalAccuracy = p.Accuracy.Horizontal;
            VerticalAccuracy = p.Accuracy.Vertical;
            Timestamp = p.Timestamp;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

        public double HorizontalAccuracy { get; set; }
        public double VerticalAccuracy { get; set; }

        public DateTime Timestamp { get; set; }
    }
}