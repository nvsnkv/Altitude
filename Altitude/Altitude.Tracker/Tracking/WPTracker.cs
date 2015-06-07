using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Devices.Geolocation;
using Altitude.Domain;
using Altitude.Domain.Tracking;
using Altitude.Tracker.Annotations;
using PositionChangedEventArgs = Altitude.Domain.Tracking.PositionChangedEventArgs;

namespace Altitude.Tracker.Tracking
{
    public sealed class WPTracker:ITracker
    {
        private bool _isEnabled;
        private TrackerState? _state;
        private readonly Geolocator _geolocator;

        public WPTracker()
        {
            _geolocator = new Geolocator
            {
                DesiredAccuracy = PositionAccuracy.High,
                ReportInterval = 1000
            };
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;

                if (_isEnabled)
                    Start();
                else
                    Stop();

                RaisePropertyChanged();
            }
        }

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

        public event EventHandler<PositionChangedEventArgs> PositionChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        private async void Start()
        {
            State = TrackerState.Initializing;

            _geolocator.PositionChanged += GeolocatorOnPositionChanged;
            _geolocator.StatusChanged += GeolocatorOnStatusChanged;

            var position = await _geolocator.GetGeopositionAsync();
            if (position?.Coordinate != null)
                RaisePositionChanged(Convert(position.Coordinate));

           
        }

        private void Stop()
        {
            _geolocator.PositionChanged -= GeolocatorOnPositionChanged;
            _geolocator.StatusChanged -= GeolocatorOnStatusChanged;

            State = null;
        }

        private static Position Convert(Geocoordinate geocoordinate)
        {
            return new Position
            {
                Latitude = geocoordinate.Point.Position.Latitude,
                Longitude = geocoordinate.Point.Position.Longitude,
                Altitude = geocoordinate.Point.Position.Altitude,
                Timestamp = geocoordinate.Timestamp.LocalDateTime,

                Accuracy = new Accuracy(geocoordinate.Accuracy, geocoordinate.AltitudeAccuracy ?? double.PositiveInfinity)
            };
        }

        private void GeolocatorOnPositionChanged(Geolocator sender, Windows.Devices.Geolocation.PositionChangedEventArgs args)
        {
            var position = args.Position;
            if (position?.Coordinate != null)
                RaisePositionChanged(Convert(position.Coordinate));
        }

        private void GeolocatorOnStatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            switch (args.Status)
            {
                case PositionStatus.Ready:
                    State = TrackerState.Ready;
                    break;
                case PositionStatus.Initializing:                    
                case PositionStatus.NoData:
                    State = TrackerState.Initializing;
                    break;
                case PositionStatus.Disabled:
                case PositionStatus.NotInitialized:
                case PositionStatus.NotAvailable:
                    State = TrackerState.DataUnavailable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotifyPropertyChangedInvocator]
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RaisePositionChanged(Position position)
        {
            PositionChanged?.Invoke(this, new PositionChangedEventArgs {Position = position});
        }
    }
}