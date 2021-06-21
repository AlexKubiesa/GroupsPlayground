using System;
using System.Windows.Input;

namespace GroupsPlayground.UI
{
    public class GroupSizeViewModel : ViewModel
    {
        private int groupSize;

        public GroupSizeViewModel()
        {
            NextCommand = new Command(Next);
        }

        public event EventHandler NextClicked;

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