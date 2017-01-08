using SFML.Window;
using System;

namespace PNCEngine.UI.Events
{
    public class ClickEventArgs : EventArgs
    {
        #region Private Fields

        private Mouse.Button buttonClicked;

        #endregion Private Fields

        #region Public Constructors

        public ClickEventArgs(Mouse.Button buttonClicked)
        {
            this.buttonClicked = buttonClicked;
        }

        #endregion Public Constructors

        #region Public Properties

        public Mouse.Button ButtonClicked
        {
            get { return buttonClicked; }
            set { buttonClicked = value; }
        }

        #endregion Public Properties
    }
}