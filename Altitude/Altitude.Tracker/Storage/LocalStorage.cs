using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Altitude.Domain;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.Storage
{
    public class LocalStorage:INotifyPropertyChanged
    {
        private Accuracy _desiredAccuracy;
        private int _count;

        public LocalStorage(Accuracy desiredAccuracy)
        {
            if (desiredAccuracy.CompareTo(Accuracy.Zero) <= 0)
                throw new ArgumentOutOfRangeException(nameof(desiredAccuracy),"Desired accuracy should be positive!");

            _desiredAccuracy = desiredAccuracy;
        }

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

        public void Add(Position position)
        {
            if (position.Accuracy.CompareTo(DesiredAccuracy) <= 0)
            {
                //Add;
                Count++;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Export(Exporter exporter)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}