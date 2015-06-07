using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
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
            LoadAccuracySettitng();

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
            UpdateHasChanges();
        }

        [UsedImplicitly]
        public ICommand Apply => _applyCommand ?? (_applyCommand = new DelegateCommand(ApplyChanges));

        [UsedImplicitly]
        public ICommand Reset => _resetCommand ?? (_resetCommand = new DelegateCommand(ResetChanges));

        private void ResetChanges()
        {
            Accuracy.Horizontal = _initialAccuracy.Horizontal;
            Accuracy.Vertical = _initialAccuracy.Vertical;
        }

        private void ApplyChanges()
        {
            _initialAccuracy = new Accuracy(Accuracy.Horizontal, Accuracy.Vertical);

            SaveAccuracySettings();
            UpdateHasChanges();
        }

        private void UpdateHasChanges()
        {
            HasChanges = Math.Abs(_initialAccuracy.Horizontal - Accuracy.Horizontal) > Double.Epsilon ||
                         Math.Abs(_initialAccuracy.Vertical - Accuracy.Vertical) > Double.Epsilon;
        }

        private void SaveAccuracySettings()
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["Accruacy.Horizontal"] = _initialAccuracy.Horizontal;
            settings.Values["Accuracy.Vertical"] = _initialAccuracy.Vertical;
        }

        private void LoadAccuracySettitng()
        {
            var settings = ApplicationData.Current.LocalSettings;

            var horizontalAccuracy = settings.Values["Accruacy.Horizontal"];
            if (horizontalAccuracy == null)
            {
                settings.Values["Accruacy.Horizontal"] = horizontalAccuracy = 6.0d;
            }

            var verticalAccuracy = settings.Values["Accuracy.Vertical"];
            if (verticalAccuracy == null)
            {
                settings.Values["Accruacy.Vertical"] = verticalAccuracy = 6.0d;
            }

            _initialAccuracy  = new Accuracy((double)horizontalAccuracy, (double)verticalAccuracy);
        }
    }
}