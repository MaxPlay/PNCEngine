using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNCEngine.Core.Events;

namespace PNCEngine.Core.Components.Rendering
{
    public abstract class Renderer : Component
    {
        internal override void SetGameObject(GameObject gameObject)
        {
            if (GameObject == gameObject)
                return;

            if (GameObject != null)
                GameObject.Drawed -= Draw;
            base.SetGameObject(gameObject);
            if (GameObject != null)
                GameObject.Drawed += Draw;
        }

        protected abstract void Draw(DrawingEventArgs e);
    }
}
