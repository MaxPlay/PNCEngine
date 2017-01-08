using PNCEngine.Rendering;
using System;

namespace PNCEngine.UI.Events
{
    public class UIDrawEventArgs : EventArgs
    {
        #region Private Fields

        private SpriteBatch currentBatch;

        #endregion Private Fields

        #region Public Constructors

        public UIDrawEventArgs(SpriteBatch spriteBatch)
        {
            this.currentBatch = spriteBatch;
        }

        #endregion Public Constructors

        #region Public Properties

        public SpriteBatch CurrentBatch
        {
            get { return currentBatch; }
            set { currentBatch = value; }
        }

        #endregion Public Properties
    }
}