using System;

namespace GroupsPlayground.UI
{
    public class ValidatedPropertyViewModel<T> : ViewModel
    {
        private readonly Func<T, string> validate;
        private T value;
        private string error;
        private bool isValid;

        public ValidatedPropertyViewModel(Func<T, string> validate)
        {
            this.validate = validate ?? throw new ArgumentNullException(nameof(validate));
        }

        public T Value
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
            private set
            {
                isValid = value;
                Notify();
            }
        }

        public void Validate()
        {
            Error = validate(Value);
            IsValid = (Error == null);
        }
    }

    public sealed class ValidatedPropertyViewModel : ValidatedPropertyViewModel<object>
    {
        public ValidatedPropertyViewModel(Func<object, string> validate) : base(validate)
        {
        }
    }
}