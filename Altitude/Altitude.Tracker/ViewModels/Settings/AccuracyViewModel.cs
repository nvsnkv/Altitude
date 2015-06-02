using Windows.UI.Core;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels.Settings
{
    public class AccuracyViewModel:ViewModelBase
    {
        private double _horizontal;
        private double _vertical;

        public AccuracyViewModel([NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
        }

        [UsedImplicitly]
        public double Horizontal
        {
            get { return _horizontal; }
            set
            {
                if (value.Equals(_horizontal)) return;
                _horizontal = value;
                RaisePropertyChanged();
            }
        }

        [UsedImplicitly]
        public double Vertical
        {
            get { return _vertical; }
            set
            {
                if (value.Equals(_vertical)) return;
                _vertical = value;
                RaisePropertyChanged();
            }
        }
    }
}