using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlekTeams.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Set<T>(ref T prop, T value, [CallerMemberName] string caller = "")
        {
            prop = value;
            OnPropertyChanged(caller);
        }

        private void OnPropertyChanged(string caller) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));

    }
}
