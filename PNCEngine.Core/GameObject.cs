using PNCEngine.Core.Attributes;
using PNCEngine.Core.Components;
using PNCEngine.Core.Events;
using PNCEngine.Core.Parser;
using PNCEngine.Core.Scenes;
using PNCEngine.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Xml;
using static PNCEngine.Core.Scenes.Scenegraph;

namespace PNCEngine.Core
{
    [RequireComponent(typeof(Transform))]
    public sealed class GameObject : EngineObject
    {
        #region Private Fields

        private List<Component> components;
        private bool isStatic;
        private string tag;
        private Transform transform;

        #endregion Private Fields

        #region Public Constructors

        public GameObject()
        {
            components = new List<Component>();
            aquireComponents(typeof(GameObject));
            transform = GetComponent<Transform>();
            isStatic = false;
        }

        public GameObject(GameObject obj)
        {
            components = new List<Component>();
            foreach (Component c in obj.components)
            {
                aquireComponents(c.GetType());
            }
            isStatic = false;
            transform = GetComponent<Transform>();
        }

        #endregion Public Constructors

        #region Public Events

        public event DrawingEventHandler Drawed;

        public event UpdateEventHandler FixedUpdated;

        public event ScenegraphEventHandler Unloaded;

        public event UpdateEventHandler Updated;

        #endregion Public Events

        #region Public Properties

        public bool IsStatic { get { return isStatic; } }

        public string Tag
        {
            get { return tag; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new InvalidTagException();
                tag = value;
                TagManager.RegisterTag(value);
            }
        }

        public Transform Transform { get { return transform; } }

        #endregion Public Properties

        #region Public Methods

        public override Component AddComponent(Component component)
        {
            foreach (Attribute a in component.GetType().GetCustomAttributes(true))
            {
                if (a is SingleInstanceComponentAttribute)
                {
                    foreach (Component c in components)
                    {
                        if (c is Component)
                            return c;
                    }
                }
            }

            components.Add(component);
            component.SetGameObject(this);
            component.Reset();
            return component;
        }

        public override bool CompareTag(string tag)
        {
            return TagManager.CompareTag(this.tag, tag);
        }

        public override T GetComponent<T>()
        {
            foreach (Component c in components)
            {
                if (c is T)
                    return c as T;
            }
            return null;
        }

        public override Component[] GetComponents()
        {
            return components.ToArray();
        }

        public void RemoveComponent(Component component)
        {
            if (component is Transform)
                return;

            component.OnDestroyed();
            components.Remove(component);
        }

        public override void Reset()
        {
            foreach (Component c in components)
            {
                c.Reset();
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal void AddSubscriptions(Transform parent)
        {
            if (parent == null)
            {
                SceneManager.CurrentScene.Scenegraph.Updated += Update;
                SceneManager.CurrentScene.Scenegraph.FixedUpdated += FixedUpdate;
                SceneManager.CurrentScene.Scenegraph.Drawed += Draw;
                SceneManager.CurrentScene.Scenegraph.Unloaded += Unload;
            }
            else
            {
                parent.GameObject.Updated += Update;
                parent.GameObject.FixedUpdated += FixedUpdate;
                parent.GameObject.Drawed += Draw;
                parent.GameObject.Unloaded += Unload;
            }
        }

        internal void Load(XmlReader reader, GameObject parent, ComponentIndexer componentIndexer, Scenegraph scenegraph)
        {
            transform.Scenegraph = scenegraph;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "GameObject")
                    return;

                if (reader.NodeType == XmlNodeType.Element)
                    if (reader.Name == "Children")
                    {
                        LoadChildren(reader, componentIndexer);
                    }
                    else
                    {
                        Component c = componentIndexer.GetComponentByName(reader.Name);
                        if (c is Transform)
                        {
                            components[0].Load(reader);
                            if (parent != null)
                                ((Transform)components[0]).Parent = parent.transform;
                        }
                        else
                        {
                            c.Load(reader);
                            components.Add(c);
                        }
                    }
            }
        }

        internal void RemoveSubscriptions(Transform parent)
        {
            if (parent == null)
            {
                SceneManager.CurrentScene.Scenegraph.Updated -= Update;
                SceneManager.CurrentScene.Scenegraph.FixedUpdated -= FixedUpdate;
                SceneManager.CurrentScene.Scenegraph.Drawed -= Draw;
                SceneManager.CurrentScene.Scenegraph.Unloaded -= Unload;
            }
            else
            {
                parent.GameObject.Updated -= Update;
                parent.GameObject.FixedUpdated -= FixedUpdate;
                parent.GameObject.Drawed -= Draw;
                parent.GameObject.Unloaded -= Unload;
            }
        }

        #endregion Internal Methods

        #region Private Methods

        private void aquireComponents(Type aquiredType)
        {
            foreach (Attribute a in aquiredType.GetCustomAttributes(true))
            {
                if (a is RequireComponentAttribute)
                {
                    RequireComponentAttribute attribute = a as RequireComponentAttribute;
                    if (attribute.RequiredType != null)
                        this.AddComponent((Component)Activator.CreateInstance((attribute).RequiredType, this));
                }
            }
        }

        private void Draw(DrawingEventArgs e)
        {
            Drawed?.Invoke(e);
        }

        private void FixedUpdate()
        {
            FixedUpdated?.Invoke();
        }

        private void LoadChildren(XmlReader reader, ComponentIndexer componentIndexer)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Component")
                    return;

                if (reader.NodeType == XmlNodeType.Element)
                    if (reader.Name == "GameObject")
                    {
                        GameObject g = new GameObject();
                        g.Load(reader, this, componentIndexer, transform.Scenegraph);
                    }
            }
        }

        private void Unload()
        {
            Unloaded?.Invoke();
        }

        private void Update()
        {
            Updated?.Invoke();
        }

        #endregion Private Methods
    }
}