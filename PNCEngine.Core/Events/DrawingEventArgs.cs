using PNCEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Core.Events
{
    public class DrawingEventArgs : EventArgs
    {
        private SpriteBatch spriteBatch;

        public DrawingEventArgs(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        private DrawingEventArgs() { }
    }
}
