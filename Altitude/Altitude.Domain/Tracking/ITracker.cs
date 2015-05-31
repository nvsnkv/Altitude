using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Domain.Tracking
{
    interface ITracker:INotifyPropertyChanged
    {
        bool IsEnabled { get; set; }
        TrackerState? State { get; }

        event EventHandler<PositionChangedEventArgs> PositionChanged;
    }
}
