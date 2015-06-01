using System;
using Windows.UI.Core;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels.RightNow
{
    public class RightNowViewModel:RightNowViewModelBase
    {
        private double _altitude;
        private double _accuracy;
        private DateTime _timestamp;

        public RightNowViewModel([NotNull] ITracker tracker, [NotNull] CoreDispatcher dispatcher) : base(tracker,dispatcher)
        {
            Position = new PositionViewModel(tracker,dispatcher);
            Tracker = new TrackerViewModel(tracker,dispatcher);
        }

        [UsedImplicitly]
        public PositionViewModel Position { get; private set; }

        [UsedImplicitly]
        public TrackerViewModel Tracker { get; private set; }

        [UsedImplicitly]
        public double Altitude
        {
            get { return _altitude; }
            private set
            {
                if (value.Equals(_altitude)) return;
                _altitude = value;
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

        [UsedImplicitly]
        public DateTime Timestamp
        {
            get { return _timestamp; }
            private set
            {
                if (value.Equals(_timestamp)) return;
                _timestamp = value;
                RaisePropertyChanged();
            }
        }

        protected override async void TrackerOnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            var position = e.Position;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Altitude = position.Altitude;
                Accuracy = position.Accuracy.Vertical;
                Timestamp = position.Timestamp;
            });
        }
    }
}