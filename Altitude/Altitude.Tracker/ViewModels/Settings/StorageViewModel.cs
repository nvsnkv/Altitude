using System;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Core;
using Altitude.Tracker.Annotations;
using Altitude.Tracker.Commands;
using Altitude.Tracker.Storage;

namespace Altitude.Tracker.ViewModels.Settings
{
    public class StorageViewModel:ViewModelBase
    {
        private readonly LocalStorage _storage;
        private int _count;
        private bool _canClear;
        private bool _canExport;
        private ICommand _clearCommand;
        private ICommand _exportCommand;

        public StorageViewModel([NotNull] LocalStorage storage, [NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
            if (storage == null) throw new ArgumentNullException(nameof(storage));
            _storage = storage;

            _storage.PropertyChanged += StorageOnPropertyChanged;
        }

        [UsedImplicitly]
        public int Count
        {
            get { return _count; }
            private set 
            {
                if (value == _count) return;
                _count = value;
                RaisePropertyChanged();
            }
        }

        [UsedImplicitly]
        public bool CanClear
        {
            get { return _canClear; }
            private set
            {
                if (value == _canClear) return;
                _canClear = value;
                RaisePropertyChanged();
            }
        }

        [UsedImplicitly]
        public bool CanExport
        {
            get { return _canExport; }
            private set
            {
                if (value == _canExport) return;
                _canExport = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Clear => _clearCommand ?? (_clearCommand = new DelegateCommand(_storage.Clear));

        public ICommand Export => _exportCommand ?? (_exportCommand = new DelegateCommand(_storage.Export));

        private async void StorageOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Count":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Count = _storage.Count);
                    break;

                case "CanExport":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => CanExport = _storage.CanExport);
                    break;

                case "CanClear":
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => CanClear = _storage.CanClear);
                    break;
            }
        }
    }
}