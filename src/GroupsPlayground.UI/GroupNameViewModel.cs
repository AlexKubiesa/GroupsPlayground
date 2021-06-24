using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public sealed class GroupNameViewModel : ViewModel
    {
        private string value;
        private string error;
        private bool isValid;

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                Notify();
            }
        }

        public string Error
        {
            get => error;
            private set
            {
                error = value;
                Notify();
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

        public void Validate()
        {
            Error = Group.ValidateName(Value);
            IsValid = (Error == null);
        }
    }
}