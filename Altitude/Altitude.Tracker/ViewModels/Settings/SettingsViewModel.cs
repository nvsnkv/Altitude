using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Core;
using Altitude.Domain;
using Altitude.Tracker.Annotations;
using Altitude.Tracker.Commands;

namespace Altitude.Tracker.ViewModels.Settings
{
    public class SettingsViewModel:ViewModelBase
    {
        private bool _hasChanges;
        private ICommand _applyCommand;
        private ICommand _resetCommand;
        private Accuracy _initialAccuracy;

        public SettingsViewModel([NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
            _initialAccuracy = new Accuracy {Horizontal = 6, Vertical = 6};

            Accuracy = new AccuracyViewModel(dispatcher)
            {
                Horizontal = _initialAccuracy.Horizontal,
                Vertical = _initialAccuracy.Vertical
            };

            Accuracy.PropertyChanged += AccuracyOnPropertyChanged;
        }

        [UsedImplicitly]
        public AccuracyViewModel Accuracy { get; private set; }
        [UsedImplicitly]
        public bool HasChanges
        {
            get { return _hasChanges; }
            private set
            {
                if (value == _hasChanges) return;
                _hasChanges = value;
                RaisePropertyChanged();
            }
        }

        private void AccuracyOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            HasChanges = Math.Abs(_initialAccuracy.Horizontal - Accuracy.Horizontal) > Double.Epsilon ||
                         Math.Abs(_initialAccuracy.Vertical - Accuracy.Vertical) > Double.Epsilon;
        }

        [UsedImplicitly]
        public ICommand Apply => _applyCommand ?? (_applyCommand = new DelegateCommand(ApplyChanges));

        [UsedImplicitly]
        public ICommand Reset => _resetCommand ?? (_resetCommand = new DelegateCommand(ResetChanges));

        private void ResetChanges()
        {
            throw new System.NotImplementedException();
        }

        private void ApplyChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}