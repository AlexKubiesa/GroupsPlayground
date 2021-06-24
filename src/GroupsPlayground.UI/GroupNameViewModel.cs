using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public sealed class GroupNameViewModel : ValidatedPropertyViewModel
    {
        private string value;

        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                Notify();
            }
        }

        protected override string ValidateInternal() => Group.ValidateName(Value);
    }
}