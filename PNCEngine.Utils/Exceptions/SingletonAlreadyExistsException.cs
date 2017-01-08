using System;

namespace PNCEngine.Exceptions
{
    [Serializable]
    public class SingletonAlreadyExistsException : Exception
    {
        #region Public Constructors

        public SingletonAlreadyExistsException(string type)
        : base("Only one instance of the " + type + " allowed. Access the " + type + " parameters using the static Instance property.")
        {
        }

        #endregion Public Constructors
    }
}