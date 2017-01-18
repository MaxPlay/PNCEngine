using PNCEngine.Core.Events;

namespace PNCEngine.Core.Components.Rendering
{
    public abstract class Renderer : Component
    {
        #region Public Constructors

        public Renderer(GameObject gameObject) : base(gameObject)
        {
        }

        #endregion Public Constructors

        #region Internal Methods

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

        #endregion Internal Methods

        #region Protected Methods

        protected abstract void Draw(DrawingEventArgs e);

        #endregion Protected Methods
    }
}