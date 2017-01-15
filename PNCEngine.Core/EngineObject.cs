using SFML.System;
using System;

namespace PNCEngine.Core
{
    public abstract class EngineObject : IEquatable<EngineObject>
    {
        #region Private Fields

        private static long instanceNewID;
        private bool active;

        #endregion Private Fields

        #region Public Constructors

        public EngineObject()
        {
            Register();
            active = true;
        }

        #endregion Public Constructors

        #region Public Delegates

        public delegate void EngineObjectEventHandler(EngineObject obj, EventArgs e);

        public delegate void EngineObjectEventHandler<T>(EngineObject obj, T e) where T : EventArgs;

        #endregion Public Delegates

        #region Public Events

        public event EngineObjectEventHandler Activated;

        public event EngineObjectEventHandler Deactivated;

        #endregion Public Events

        #region Public Properties

        public bool Active
        {
            get { return active; }
            set
            {
                if (active == value) return;
                active = value;
                OnActiveChanged();
            }
        }

        #endregion Public Properties

        #region Protected Properties

        protected long instanceID { get; private set; }

        #endregion Protected Properties

        #region Public Methods

        public static void Destroy(EngineObject obj)
        {
            if (obj is GameObject)
                ((GameObject)obj).OnDestroyed();
            
            if (obj is Component)
                ((Component)obj).GameObject.RemoveComponent((Component)obj);

            obj = null;
            GC.Collect();
        }

        public event EngineObjectEventHandler Destroyed;

        internal void OnDestroyed()
        {
            Destroyed?.Invoke(this, new EventArgs());
        }
        
        public static GameObject Instantiate(GameObject obj, Vector2f position, float rotation)
        {
            GameObject newObj = new GameObject(obj);
            newObj.Transform.LocalPosition = position;
            newObj.Transform.LocalRotation = rotation;
            return newObj;
        }

        public abstract Component AddComponent(Component component);

        public Component AddComponent<T>() where T : Component, new()
        {
            return AddComponent(new T());
        }

        public abstract bool CompareTag(string tag);

        public bool Equals(EngineObject other)
        {
            return instanceID == other.instanceID;
        }

        public abstract T GetComponent<T>() where T : Component;

        public abstract Component[] GetComponents();

        public abstract void Reset();

        #endregion Public Methods

        #region Protected Methods

        protected void OnActiveChanged()
        {
            if (this.active)
                Activated?.Invoke(this, new EventArgs());
            else
                Deactivated?.Invoke(this, new EventArgs());
        }

        protected virtual void Register()
        {
            this.instanceID = instanceNewID++;
        }

        #endregion Protected Methods
    }
}