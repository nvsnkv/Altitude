using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Altitude.Domain;
using Altitude.Tracker.Annotations;
using Altitude.Tracker.Commands;
using Altitude.Tracker.Storage;

namespace Altitude.Tracker.ViewModels.Settings
{
    public class SettingsViewModel:ViewModelBase
    {
        private bool _hasChanges;
        private bool _preventLockScreen;

        private ICommand _applyCommand;
        private ICommand _resetCommand;

        private readonly LocalStorage _storage;
        private DisplayRequest _displayRequest;

        public SettingsViewModel([NotNull] LocalStorage storage, [NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
            if (storage == null) throw new ArgumentNullException(nameof(storage));
            _storage = storage;
            LoadAccuracySettitng();

            Accuracy = new AccuracyViewModel(dispatcher)
            {
                Horizontal =_storage.DesiredAccuracy.Horizontal,
                Vertical = _storage.DesiredAccuracy.Vertical
            };

            Storage = new StorageViewModel(storage,dispatcher);

            Accuracy.PropertyChanged += AccuracyOnPropertyChanged;
        }

        [UsedImplicitly]
        public AccuracyViewModel Accuracy { get; private set; }

        [UsedImplicitly]
        public StorageViewModel Storage { get; private set; }

        [UsedImplicitly]
        public bool PreventLockScreen
        {
            get { return _preventLockScreen; }
            set
            {
                if (value == _preventLockScreen) return;
                _preventLockScreen = value;

                if (_preventLockScreen)
                {
                    _displayRequest = new DisplayRequest();
                    _displayRequest.RequestActive();
                }
                else
                {
                    _displayRequest.RequestRelease();
                    _displayRequest = null;
                }

                RaisePropertyChanged();
            }
        }

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
            Accuracy.Horizontal = _storage.DesiredAccuracy.Horizontal;
            Accuracy.Vertical = _storage.DesiredAccuracy.Vertical;
        }

        private void ApplyChanges()
        {
            _storage.DesiredAccuracy = new Accuracy(Accuracy.Horizontal, Accuracy.Vertical);

            SaveAccuracySettings();
            UpdateHasChanges();
        }

        private void UpdateHasChanges()
        {
            HasChanges = Math.Abs(_storage.DesiredAccuracy.Horizontal - Accuracy.Horizontal) > Double.Epsilon ||
                         Math.Abs(_storage.DesiredAccuracy.Vertical - Accuracy.Vertical) > Double.Epsilon;
        }

        private void SaveAccuracySettings()
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values["Accruacy.Horizontal"] = _storage.DesiredAccuracy.Horizontal;
            settings.Values["Accuracy.Vertical"] = _storage.DesiredAccuracy.Vertical;
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

            _storage.DesiredAccuracy = new Accuracy((double)horizontalAccuracy, (double)verticalAccuracy);
        }
    }
}