using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroupsPlayground.UI
{
    public class ViewModel : INotifyPropertyChanged
    {
        protected void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
