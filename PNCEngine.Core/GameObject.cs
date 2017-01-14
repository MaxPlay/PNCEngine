using PNCEngine.Core.Attributes;
using PNCEngine.Core.Components;
using PNCEngine.Utils.Exceptions;
using PNCEngine.Utils.Extensions;
using System;
using System.Collections.Generic;

namespace PNCEngine.Core
{
    [RequireComponent(typeof(Transform))]
    public sealed class GameObject : EngineObject
    {
        #region Private Fields

        private List<Component> components;
        private bool isStatic;
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

        #region Public Properties

        public bool IsStatic { get { return isStatic; } }
        public Transform Transform { get { return transform; } }

        #endregion Public Properties

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

        public void RemoveComponent(Component component)
        {
            if (component is Transform)
                return;

            components.Remove(component);
        }

        public override void Reset()
        {
            foreach (Component c in components)
            {
                c.Reset();
            }
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

        private string tag;

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
        
        public override bool CompareTag(string tag)
        {
            return TagManager.CompareTag(this.tag, tag);
        }

        #endregion Private Methods
    }
}