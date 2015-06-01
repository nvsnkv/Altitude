using System;
using System.ComponentModel;

namespace Altitude.Domain.Tracking
{
    public interface ITracker:INotifyPropertyChanged
    {
        bool IsEnabled { get; set; }
        TrackerState? State { get; }

        event EventHandler<PositionChangedEventArgs> PositionChanged;
    }
}
