using System;
using System.Windows.Input;
using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public class GroupPropertiesViewModel : ViewModel
    {
        private string groupName;
        private string groupNameError;
        private int groupSize;
        private bool isValid;

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
                Validate();
            }
        }

        public string GroupNameError
        {
            get => groupNameError;
            private set
            {
                groupNameError = value;
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
                Validate();
            }
        }

        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                Notify();
            }
        }

        public ICommand NextCommand { get; }

        private void Next()
        {
            Validate();
            if (IsValid)
                NextClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Validate()
        {
            GroupNameError = Group.ValidateName(GroupName);
            IsValid = (GroupNameError == null);
        }
    }
}