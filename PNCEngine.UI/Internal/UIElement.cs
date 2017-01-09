using PNCEngine.UI.Interfaces;
using SFML.System;
using System.Xml;

namespace PNCEngine.UI.Internal
{
    public abstract class UIElement : IUIElement
    {
        #region Protected Fields

        protected bool enabled;
        protected GuiHandle handle;
        protected string name;
        protected Vector2i position;

        #endregion Protected Fields

        #region Private Fields

        private UIManager manager;

        #endregion Private Fields

        #region Public Constructors

        public UIElement(UIManager manager)
        {
            this.manager = manager;
            subscribe();
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public GuiHandle Handle
        {
            get { return handle; }
            set { handle = value; }
        }

        public UIManager Manager
        {
            get { return manager; }
            set { if (manager == value) return; unsubscribe(); manager = value; subscribe(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Vector2i Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion Public Properties

        #region Protected Methods

        protected abstract void subscribe();

        protected abstract void unsubscribe();

        public abstract void Serialize(XmlWriter writer);

        public abstract void Deserialize(XmlReader reader);

        #endregion Protected Methods
    }
}