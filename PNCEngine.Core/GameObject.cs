using PNCEngine.Core.Interfaces;
using PNCEngine.Core.Scenes;
using PNCEngine.Utils.Extensions;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PNCEngine.Core
{
    public class GameObject : IEquatable<GameObject>, IEnumerable<IScenegraphElement>, ITransform, IGameObject, IXmlSerializable
    {
        #region Private Fields

        private static long newID;
        private List<IScenegraphElement> children;
        private bool enabled;
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

        #region Public Events

        public event EventHandler SetDisabled;

        public event EventHandler SetEnabled;

        #endregion Public Events

        #region Public Properties

        public IScenegraphElement[] Children
        {
            get
            {
                return children.ToArray();
            }
        }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    if (value)
                    {
                        enabled = true;
                        OnSetEnabled();
                    }
                    else
                    {
                        enabled = false;
                        OnSetDisabled();
                    }
                }
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + (int)id;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            Dictionary<string, char> elements = new Dictionary<string, char>();
            elements.Add("name", 'n');
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "gameobject")
                    return;

                if (reader.NodeType == XmlNodeType.Element)
                    if (elements.ContainsKey(reader.Name.ToLower()))
                        switch (elements[reader.Name.ToLower()])
                        {
                            case 'n':
                                if (reader.IsEmptyElement)
                                    break;
                                reader.Read();
                                name = reader.Value;
                                break;
                        }
            }
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

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("gameobject");
            XmlWriteGameObject(writer);
            writer.WriteEndElement();
        }

        #endregion Public Methods

        #region Protected Methods

        protected void XmlWriteGameObject(XmlWriter writer)
        {
            writer.WriteElementString("name", name);
            writer.WriteElementString("enabled", enabled ? "1" : "0");
            position.WriteXml(writer, "position");
            writer.WriteElementString("rotation", rotation.ToString());
            scale.WriteXml(writer, "scale");
        }

        #endregion Protected Methods

        #region Private Methods

        private void OnSetDisabled()
        {
            SetDisabled?.Invoke(this, new EventArgs());
        }

        private void OnSetEnabled()
        {
            SetEnabled?.Invoke(this, new EventArgs());
        }

        #endregion Private Methods
    }
}