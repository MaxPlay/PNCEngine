using System;
using System.Runtime.Serialization;

namespace PNCEngine.Utils.Exceptions
{
    public class NotInitializedException : Exception
    {
        #region Public Constructors

        public NotInitializedException(string message) : base(message)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected NotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}