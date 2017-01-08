using SFML.Window;
using System;

namespace PNCEngine.UI.Events
{
    public class KeyPressedEventArgs : EventArgs
    {
        #region Private Fields

        private bool ctrlPressed;
        private Keyboard.Key pressedKey;

        private bool shiftPressed;

        #endregion Private Fields

        #region Public Constructors

        public KeyPressedEventArgs(Keyboard.Key pressedKey, bool shiftPressed, bool ctrlPressed)
        {
            this.pressedKey = pressedKey;
            this.shiftPressed = shiftPressed;
            this.ctrlPressed = ctrlPressed;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool CtrlPressed
        {
            get { return ctrlPressed; }
            set { ctrlPressed = value; }
        }

        public Keyboard.Key PressedKey
        {
            get { return pressedKey; }
            set { pressedKey = value; }
        }

        public bool ShiftPressed
        {
            get { return shiftPressed; }
            set { shiftPressed = value; }
        }

        #endregion Public Properties
    }
}