using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Altitude.Database
{
    public class Measurement
    {
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