using System;
using System.Runtime.Serialization;

namespace PNCEngine.Utils.Exceptions
{
    public class InvalidTagException : Exception
    {
        #region Public Constructors

        public InvalidTagException()
            : base("The given tag is invalid. Make sure that your tags are not null or empty.")
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected InvalidTagException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}