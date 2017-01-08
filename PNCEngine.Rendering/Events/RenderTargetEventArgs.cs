using SFML.Graphics;
using System;

namespace PNCEngine.Rendering.Events
{
    public class RenderTargetEventArgs : EventArgs
    {
        #region Private Fields

        private RenderTarget target;

        #endregion Private Fields

        #region Public Constructors

        public RenderTargetEventArgs(RenderTarget target)
        {
            this.target = target;
        }

        #endregion Public Constructors

        #region Public Properties

        public RenderTarget Target
        {
            get { return target; }
        }

        #endregion Public Properties
    }
}