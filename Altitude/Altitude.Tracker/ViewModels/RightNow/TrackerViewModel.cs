using System;
using Windows.UI.Core;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels.RightNow
{
    public class TrackerViewModel:RightNowViewModelBase
    {
        private readonly ITracker _tracker;
        private TrackerState? _state;
        

        public TrackerViewModel([NotNull] ITracker tracker, [NotNull] CoreDispatcher dispatcher) : base(tracker, dispatcher)
        {
            if (tracker == null) throw new ArgumentNullException(nameof(tracker));
            _tracker = tracker;
            _state = null;
        }

        [UsedImplicitly]
        public TrackerState? State
        {
            get { return _state; }
            private set
            {
                if (value == _state) return;
                _state = value;
                RaisePropertyChanged();
            }
        }

        [UsedImplicitly]
        public bool IsEnabled
        {
            get { return _tracker.IsEnabled; }
            set
            {
                _tracker.IsEnabled = value;
            }
        }

        protected override async void TrackerOnStateChanged()
        {
            var state = _tracker.State;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                State = state;
            });
        }

        protected override async void TrackerIsEnabledChanged()
        {
            base.TrackerIsEnabledChanged();
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => RaisePropertyChanged(nameof(IsEnabled)));
        }
    }
}