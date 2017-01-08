using System;

namespace PNCEngine.UI.Events
{
    public class UIUpdateEventArgs : EventArgs
    {
        #region Private Fields

        private float elapsedTime;

        #endregion Private Fields

        #region Public Constructors

        public UIUpdateEventArgs(float elapsedTime)
        {
            this.elapsedTime = elapsedTime;
        }

        #endregion Public Constructors

        #region Public Properties

        public float ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        #endregion Public Properties
    }
}