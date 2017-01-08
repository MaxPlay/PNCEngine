using SFML.System;
using System;

namespace PNCEngine.UI.Internal.Events
{
    public class UIMouseEventArgs : EventArgs
    {
        #region Private Fields

        private Vector2i mousePosition;

        #endregion Private Fields

        #region Public Constructors

        public UIMouseEventArgs(Vector2i mousePosition)
        {
            this.mousePosition = mousePosition;
        }

        #endregion Public Constructors

        #region Public Properties

        public Vector2i MousePosition { get { return mousePosition; } }

        #endregion Public Properties
    }
}