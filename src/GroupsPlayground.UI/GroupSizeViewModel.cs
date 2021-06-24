using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    public sealed class GroupSizeViewModel : ValidatedPropertyViewModel
    {
        private int value;

        public int Value
        {
            get => value;
            set
            {
                this.value = value;
                Notify();
            }
        }

        protected override string ValidateInternal() => CayleyTable.ValidateSize(Value);
    }
}