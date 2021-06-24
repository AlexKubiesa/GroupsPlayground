using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public sealed class GroupSizeViewModel : ViewModel
    {
        private int value;
        private string error;
        private bool isValid;

        public int Value
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
            Error = CayleyTable.ValidateSize(Value);
            IsValid = (Error == null);
        }
    }
}