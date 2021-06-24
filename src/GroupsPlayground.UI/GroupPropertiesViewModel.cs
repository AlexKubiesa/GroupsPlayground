using System;
using System.Windows.Input;
using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public class GroupPropertiesViewModel : ViewModel
    {
        private bool isValid;

        public GroupPropertiesViewModel()
        {
            GroupName = new ValidatedPropertyViewModel<string>(Group.ValidateName);
            GroupSize = new ValidatedPropertyViewModel<int>(CayleyTable.ValidateSize);
            NextCommand = new Command(Next);
        }

        public event EventHandler NextClicked;

        public ValidatedPropertyViewModel<string> GroupName { get; }
        public ValidatedPropertyViewModel<int> GroupSize { get; }

        public ICommand NextCommand { get; }

        public bool IsValid
        {
            get => isValid;
            private set
            {
                isValid = value;
                Notify();
            }
        }

        private void Validate()
        {
            GroupName.Validate();
            GroupSize.Validate();
            IsValid = GroupName.IsValid && GroupSize.IsValid;
        }

        private void Next()
        {
            Validate();
            if (IsValid)
                NextClicked?.Invoke(this, EventArgs.Empty);
        }

        
    }
}