using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Altitude.Domain;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.Storage
{
    public class LocalStorage:INotifyPropertyChanged
    {
        private const string BLOB_DATA = "altitude.localStorage.blob";
        private const string DATA_COUNT = "altitude.localStorage.count";

        private readonly ITracker _tracker;
        private readonly StorageFolder _folder;
        private int _count;
        private bool _canExport;
        private bool _canClear;
        private Accuracy _desiredAccuracy;
        private StorageFile _blob;
        private StorageFile _counter;
        private Position _lastPosition = new Position {Accuracy = new Accuracy(-1, -1)};

        public Accuracy DesiredAccuracy
        {
            get { return _desiredAccuracy; }
            set
            {
                if (value.Equals(_desiredAccuracy)) return;
                _desiredAccuracy = value;
                RaisePropertyChanged();
            }
        }

        public int Count
        {
            get { return _count; }
            private set
            {
                if (value == _count) return;
                _count = value;

                UpdateCanClearAndCanExport();
                RaisePropertyChanged();
            }
        }

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

        public LocalStorage([NotNull] ITracker tracker, Accuracy desiredAccuracy)
        {
            if (tracker == null) throw new ArgumentNullException(nameof(tracker));
            if (desiredAccuracy.CompareTo(Accuracy.Zero) <= 0)
                throw new ArgumentOutOfRangeException(nameof(desiredAccuracy), "Desired accuracy should be greater positive");

            _tracker = tracker;

            _tracker.PropertyChanged += TrackerOnPropertyChanged;
            _tracker.PositionChanged += TrackerOnPositionChanged;

            _folder = ApplicationData.Current.LocalFolder;

            Initialize();
        }

        private async void Initialize()
        {
            _blob = await _folder.CreateFileAsync(BLOB_DATA, CreationCollisionOption.OpenIfExists);
            _counter = await _folder.CreateFileAsync(DATA_COUNT, CreationCollisionOption.OpenIfExists);

            var countText = await FileIO.ReadTextAsync(_counter);

            int count;
            if (int.TryParse(countText, out count))
                Count = count;
        }

        public async void Clear()
        {
            if (!CanClear)
                throw new InvalidOperationException("Unable to clear storage at the moment");

            await _blob.DeleteAsync();
            _blob = await _folder.CreateFileAsync(BLOB_DATA, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(_counter, 0.ToString());
            Count = 0;
        }

        public async void Export()
        {
            if (!CanExport)
                throw new InvalidOperationException("Unable to export data at the moment");

            var fileName = $"Altitude_Data_{DateTime.Now.Ticks}_{Count}.txt";
            var result = await _blob.CopyAsync(ApplicationData.Current.TemporaryFolder, fileName, NameCollisionOption.GenerateUniqueName);

            await Launcher.LaunchFileAsync(result, new LauncherOptions {DisplayApplicationPicker = true});
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TrackerOnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            if (e.Position.Accuracy.CompareTo(DesiredAccuracy) <= 0)
            {
                if (!_lastPosition.Equals(e.Position, true))
                {
                    Add(e.Position);
                    _lastPosition = e.Position;
                }
            }
        }

        private void TrackerOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "State")
            {
                UpdateCanClearAndCanExport();
            }
        }

        private void UpdateCanClearAndCanExport()
        {
            CanClear = CanExport = Count > 0 && _tracker.State == null;
        }

        private async void Add(Position position)
        {
            var line = $"'{position.Timestamp.ToUniversalTime()}','{position.Latitude}','{position.Longitude}','{position.Altitude}','{position.Accuracy.Horizontal}''{position.Accuracy.Vertical}'";
            await FileIO.AppendTextAsync(_blob, line);
            await FileIO.WriteTextAsync(_counter, (Count += 1).ToString());
        }

    }
}