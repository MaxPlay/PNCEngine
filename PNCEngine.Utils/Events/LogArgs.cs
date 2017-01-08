using System;

namespace PNCEngine.Utils.Events
{
    public class LogArgs : EventArgs
    {
        #region Private Fields

        private Debug.LogDepth depth;
        private string message;

        private DateTime time;

        #endregion Private Fields

        #region Public Constructors

        public LogArgs(string message, Debug.LogDepth depth, DateTime time)
        {
            this.message = message;
            this.depth = depth;
            this.time = time;
        }

        #endregion Public Constructors

        #region Public Properties

        public Debug.LogDepth Depth
        {
            get { return depth; }
        }

        public string Message
        {
            get { return message; }
        }

        public DateTime Time
        {
            get { return time; }
        }

        #endregion Public Properties
    }
}