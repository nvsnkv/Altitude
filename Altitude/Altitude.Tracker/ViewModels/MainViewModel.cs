using System;
using Windows.UI.Core;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;
using Altitude.Tracker.ViewModels.RightNow;
using Altitude.Tracker.ViewModels.Settings;

namespace Altitude.Tracker.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel([NotNull] ITracker tracker, [NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
            if (tracker == null) throw new ArgumentNullException(nameof(tracker));
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));

            RightNow = new RightNowViewModel(tracker, dispatcher);
            Settings = new SettingsViewModel(dispatcher);
        }

        [UsedImplicitly]
        public RightNowViewModel RightNow { get; private set; }

        [UsedImplicitly]
        public SettingsViewModel Settings { get; private set; }

    }
}