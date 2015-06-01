using Windows.UI.Core;
using Altitude.Tracker.Annotations;

namespace Altitude.Tracker.ViewModels
{
    public class SettingsViewModel:ViewModelBase
    {
        public SettingsViewModel([NotNull] CoreDispatcher dispatcher) : base(dispatcher)
        {
        }
    }
}