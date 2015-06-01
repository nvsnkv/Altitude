using System;
using Windows.UI.Core;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels.RightNow
{
    public class PositionViewModel:RightNowViewModelBase
    {
        private double _latitude;
        private double _longitude;
        private double _accuracy;

        public PositionViewModel([NotNull] ITracker tracker, [NotNull] CoreDispatcher dispatcher) : base(tracker, dispatcher)
        {
        }

        [UsedImplicitly]
        public double Latitude
        {
            get { return _latitude; }
            private set
            {
                if (value.Equals(_latitude)) return;
                _latitude = value;
                RaisePropertyChanged();
            }
        }

        [UsedImplicitly]
        public double Longitude
        {
            get { return _longitude; }
            private set
            {
                if (value.Equals(_longitude)) return;
                _longitude = value;
                RaisePropertyChanged();
            }
        }

        [UsedImplicitly]
        public double Accuracy
        {
            get { return _accuracy; }
            private set
            {
                if (value.Equals(_accuracy)) return;
                _accuracy = value;
                RaisePropertyChanged();
            }
        }

        protected override async void TrackerOnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            var position = e.Position;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 Latitude = position.Latitude;
                 Longitude = position.Longitude;
                 Accuracy = position.Accuracy.Horizontal;
             });
        }
    }
}