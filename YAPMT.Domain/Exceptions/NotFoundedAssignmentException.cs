using System;
using System.Runtime.Serialization;

namespace YAPMT.Domain.Exceptions
{
    public class NotFoundedAssignmentException : Exception
    {
        public const string DEFAUTL_MESSAGE = "Assignment not founded";

        public NotFoundedAssignmentException(): base(DEFAUTL_MESSAGE)
        {
        }

        public NotFoundedAssignmentException(string message) : base(message)
        {
        }

        public NotFoundedAssignmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundedAssignmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
