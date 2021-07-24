using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Groups
{
    public abstract class Group : AggregateRoot
    {
        private string name;

        protected Group(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            set
            {
                string error = value switch
                {
                    { } valid when valid.All(c => char.IsLetterOrDigit(c) || c == '_') => null,
                    var missing when string.IsNullOrEmpty(missing) => "Missing group name.",
                    _ => "Invalid characters in group name."
                };

                if (error != null)
                    throw new ValidationError(error);

                name = value;
            }
        }

        public int? Size { get; protected set; }

        public virtual void ComputeSize()
        {
        }
    }
}