using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        protected readonly CoreDispatcher Dispatcher;

        protected ViewModelBase([NotNull] CoreDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException(nameof(dispatcher));
            Dispatcher = dispatcher;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}