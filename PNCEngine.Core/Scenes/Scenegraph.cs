using PNCEngine.Core.Events;
using PNCEngine.Core.Interfaces;
using PNCEngine.Rendering;

namespace PNCEngine.Core.Scenes
{
    public class Scenegraph : IDrawable, IUpdateable
    {
        #region Private Fields

        private SpriteBatch spriteBatch;

        #endregion Private Fields

        #region Public Constructors

        public Scenegraph(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void DrawingEventHandler(DrawingEventArgs e);

        public delegate void ScenegraphEventHandler();

        public delegate void UpdateEventHandler();

        #endregion Public Delegates

        #region Public Events

        public event DrawingEventHandler Drawed;

        public event UpdateEventHandler FixedUpdated;

        public event ScenegraphEventHandler Unloaded;

        public event UpdateEventHandler Updated;

        #endregion Public Events

        #region Public Properties

        public SpriteBatch SpriteBatch { get { return spriteBatch; } set { spriteBatch = value; } }

        #endregion Public Properties

        #region Public Methods

        public void Draw()
        {
            Drawed?.Invoke(new DrawingEventArgs(spriteBatch));
        }

        public void FixedUpdate()
        {
            FixedUpdated?.Invoke();
        }

        public void Unload()
        {
            Unloaded?.Invoke();
        }

        public void Update()
        {
            Updated?.Invoke();
        }

        #endregion Public Methods
    }
}