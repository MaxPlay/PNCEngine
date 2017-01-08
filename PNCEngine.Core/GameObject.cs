using PNCEngine.Core.Interfaces;
using PNCEngine.Core.Scenes;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PNCEngine.Core
{
    public class GameObject : IEquatable<GameObject>, IEnumerable<IScenegraphElement>, ITransform, IGameObject
    {
        #region Private Fields

        private static long newID;
        private List<IScenegraphElement> children;
        private long id;
        private string name;
        private IScenegraphElement parent;
        private Vector2f position;
        private float rotation;

        private Vector2f scale;

        private Scenegraph scenegraph;

        #endregion Private Fields

        #region Public Constructors

        public GameObject() : this(string.Empty)
        {
        }

        public GameObject(string name)
        {
            id = newID++;
            position = new Vector2f();
            rotation = 0f;
            scale = new Vector2f(1, 1);
            this.name = name;
            children = new List<IScenegraphElement>();
        }

        #endregion Public Constructors

        #region Public Properties

        public IScenegraphElement[] Children
        {
            get
            {
                return children.ToArray();
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public IScenegraphElement Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                if (parent == value)
                    return;
                parent?.RemoveChild(this);
                parent = value;

                if (parent == null)
                    parent = scenegraph.Root;
                parent.AddChild(this);
            }
        }

        public Vector2f Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector2f Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Scenegraph Scenegraph
        {
            get
            {
                return scenegraph;
            }

            set
            {
                if (scenegraph == value)
                    return;

                RemoveFromScenegraph();
                scenegraph = value;
                AddToScenegraph(scenegraph);
            }
        }

        #endregion Public Properties

        #region Public Indexers

        public IScenegraphElement this[int index]
        {
            get
            {
                if (index < 0 || children.Count >= index)
                    return null;
                return children[index];
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public void AddChild(IScenegraphElement element)
        {
            if (children.Contains(element))
                children.Add(element);
        }

        public void AddToScenegraph(Scenegraph scenegraph)
        {
            if (this.scenegraph != scenegraph)
                this.scenegraph = scenegraph;

            this.scenegraph.AddElement(this);
        }

        public virtual void Draw(float elapsedTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Draw(elapsedTime);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObject)
                return Equals(obj as GameObject);
            return false;
        }

        public bool Equals(GameObject other)
        {
            return other.id == id;
        }

        public virtual void FixedUpdate(float elapsedTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].FixedUpdate(elapsedTime);
            }
        }

        public IEnumerator<IScenegraphElement> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + (int)id;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }

        public void RemoveChild(IScenegraphElement element)
        {
            children.Remove(element);
        }

        public void RemoveFromScenegraph()
        {
            scenegraph?.RemoveElement(this);
        }

        public override string ToString()
        {
            return name;
        }

        public virtual void Update(float elapsedTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Update(elapsedTime);
            }
        }

        #endregion Public Methods
    }
}