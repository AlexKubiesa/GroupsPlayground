using System;

namespace GroupsPlayground.Domain.Framework
{
    /// <summary>
    /// A user-facing error thrown when a domain object could not be constructed due to failing some validation rule.
    /// </summary>
    public class ValidationError : ApplicationException
    {
        public ValidationError(string message) : base(message)
        {
        }
    }
}