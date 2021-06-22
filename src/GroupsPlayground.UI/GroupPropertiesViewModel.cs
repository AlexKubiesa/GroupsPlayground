using System;
using System.Windows.Input;

namespace GroupsPlayground.UI
{
    public class GroupPropertiesViewModel : ViewModel
    {
        private string groupName;
        private int groupSize;

        public GroupPropertiesViewModel()
        {
            NextCommand = new Command(Next);
        }

        public event EventHandler NextClicked;

        public string GroupName
        {
            get => groupName;
            set
            {
                groupName = value;
                Notify();
            }
        }

        public int GroupSize
        {
            get => groupSize;
            set
            {
                groupSize = value;
                Notify();
            }
        }

        public ICommand NextCommand { get; }

        private void Next() => NextClicked?.Invoke(this, EventArgs.Empty);
    }
}