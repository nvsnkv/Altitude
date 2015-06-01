using System;
using System.ComponentModel;
using Windows.UI.Core;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels.RightNow
{
    public abstract class RightNowViewModelBase : ViewModelBase
    {
        private readonly ITracker _tracker;

        protected RightNowViewModelBase([NotNull] ITracker tracker, [NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
            if (tracker == null) throw new ArgumentNullException(nameof(tracker));
            _tracker = tracker;
            _tracker.PropertyChanged += TrackerOnPropertyChanged;
        }

        private void TrackerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "State":
                    TrackerOnStateChanged();
                    break;

                case "IsEnabled":
                    TrackerIsEnabledChanged();
                    break;
            }
        }

        protected virtual void TrackerIsEnabledChanged()
        {
            if (_tracker.IsEnabled)
            {
                _tracker.PositionChanged += TrackerOnPositionChanged;
            }
            else
            {
                _tracker.PositionChanged -= TrackerOnPositionChanged;
            }
        }

        protected virtual void TrackerOnStateChanged()
        {
            
        }

        protected virtual void TrackerOnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            
        }
    }
}