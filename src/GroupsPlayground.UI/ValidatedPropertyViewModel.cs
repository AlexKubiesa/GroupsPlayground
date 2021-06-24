namespace GroupsPlayground.UI
{
    public abstract class ValidatedPropertyViewModel : ViewModel
    {
        private string error;
        private bool isValid;

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

        protected abstract string ValidateInternal();

        public void Validate()
        {
            Error = ValidateInternal();
            IsValid = (Error == null);
        }
    }
}