using System.Xml;

namespace PNCEngine.Core
{
    public abstract class Component : EngineObject
    {
        #region Private Fields

        private GameObject gameObject;

        private bool initialized;

        #endregion Private Fields

        #region Public Constructors

        public Component(GameObject gameObject) : base()
        {
            SetGameObject(gameObject);
            Reset();
            Initialize();
        }

        #endregion Public Constructors

        #region Private Constructors

        private Component()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public bool Initialized
        {
            get { return initialized; }
        }

        public string Tag
        {
            get { return gameObject.Tag; }
            set
            {
                gameObject.Tag = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override Component AddComponent(Component component)
        {
            return gameObject.AddComponent(component);
        }

        public override bool CompareTag(string tag)
        {
            return TagManager.CompareTag(gameObject.Tag, tag);
        }

        public override T GetComponent<T>()
        {
            return gameObject.GetComponent<T>();
        }

        public override Component[] GetComponents()
        {
            return gameObject.GetComponents();
        }

        public virtual void Initialize()
        {
            initialized = true;
        }

        #endregion Public Methods

        #region Internal Methods

        internal abstract void Load(XmlReader reader);

        internal virtual void SetGameObject(GameObject gameObject)
        {
            if (gameObject == null)
                return;
            this.gameObject = gameObject;
        }

        #endregion Internal Methods
    }
}