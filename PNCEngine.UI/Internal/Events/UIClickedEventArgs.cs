using SFML.Window;
using System;

namespace PNCEngine.UI.Internal.Events
{
    public class UIClickedEventArgs : EventArgs
    {
        #region Private Fields

        private Mouse.Button button;
        private UIElement element;

        #endregion Private Fields

        #region Public Constructors

        public UIClickedEventArgs(UIElement element, Mouse.Button button)
        {
            this.element = element;
            this.button = button;
        }

        #endregion Public Constructors

        #region Public Properties

        public Mouse.Button Button
        {
            get { return button; }
            set { button = value; }
        }

        public UIElement Element
        {
            get { return element; }
            set { element = value; }
        }

        #endregion Public Properties
    }
}