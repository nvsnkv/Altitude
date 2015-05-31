using System;

namespace Altitude.Domain.Tracking
{
    public class PositionChangedEventArgs:EventArgs
    {
        public Position Position;
    }
}