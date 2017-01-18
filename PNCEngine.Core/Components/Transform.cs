using PNCEngine.Core.Attributes;
using PNCEngine.Core.Interfaces;
using PNCEngine.Core.Scenes;
using PNCEngine.Utils;
using SFML.System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace PNCEngine.Core.Components
{
    [SingleInstanceComponent]
    public class Transform : Component, IEnumerable, IScenegraphElement
    {
        #region Protected Fields

        protected List<Transform> children;
        protected float cosineOfRotation, sineOfRotation;
        protected Transform parent;
        protected Vector2f position;
        protected float rotation;
        protected Vector2f scale;

        #endregion Protected Fields

        #region Private Fields

        private Scenegraph scenegraph;

        #endregion Private Fields

        #region Public Constructors

        public Transform(GameObject gameObject) : base(gameObject)
        {
            scenegraph = SceneManager.CurrentScene.Scenegraph;
        }

        #endregion Public Constructors

        #region Public Properties

        public int ChildCount { get { return children.Count; } }
        public Vector2f Down { get { return new Vector2f(sineOfRotation, -cosineOfRotation); } }

        public IEnumerator Enumerator
        {
            get
            {
                return new InnerEnumerator(this);
            }
        }

        public Vector2f Left { get { return new Vector2f(-cosineOfRotation, -sineOfRotation); } }
        public Vector2f LocalPosition { get { return this.position; } set { this.position = value; } }

        public float LocalRotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                sineOfRotation = (float)Math.Sin(rotation);
                cosineOfRotation = (float)Math.Cos(rotation);
            }
        }

        public Vector2f LocalScale { get { return scale; } set { scale = value; } }

        public Vector2f LossyScale
        {
            get
            {
                if (parent == null)
                    return this.scale;
                if (parent.LossyScale == new Vector2f())
                    return new Vector2f();
                return new Vector2f(scale.X / parent.LossyScale.X, scale.Y / parent.LossyScale.Y);
            }
        }

        public Transform Parent
        {
            get { return parent; }
            set
            {
                if (parent == value)
                    return;

                GameObject.RemoveSubscriptions(parent);
                parent?.children.Remove(this);
                parent = value;
                parent?.children.Add(this);
                GameObject.AddSubscriptions(parent);
            }
        }

        public Vector2f Position
        {
            get
            {
                if (parent != null)
                    return new Vector2f(
                        parent.cosineOfRotation * position.X - parent.sineOfRotation * position.Y,
                        parent.sineOfRotation * position.X - parent.cosineOfRotation * position.Y
                        ) + parent.Position;
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2f Right { get { return new Vector2f(cosineOfRotation, sineOfRotation); } }

        public float Rotation
        {
            get
            {
                if (parent != null)
                    return rotation + parent.Rotation;
                return rotation;
            }
            set
            {
                if (parent != null)
                    rotation = value - parent.Rotation;
                else
                    rotation = value;

                sineOfRotation = (float)Math.Sin(rotation);
                cosineOfRotation = (float)Math.Cos(rotation);
            }
        }

        public Scenegraph Scenegraph
        {
            get
            {
                return scenegraph;
            }
            set
            {
                if (value != null)
                    scenegraph = value;
            }
        }

        public Vector2f Up { get { return new Vector2f(-sineOfRotation, cosineOfRotation); } }

        #endregion Public Properties

        #region Public Methods

        public Transform GetChild(int index)
        {
            return children[index];
        }

        public IEnumerator GetEnumerator()
        {
            return new InnerEnumerator(this);
        }

        public override void Reset()
        {
            position = new Vector2f();
            rotation = 0;
            scale = new Vector2f(1, 1);
            sineOfRotation = (float)Math.Sin(rotation);
            cosineOfRotation = (float)Math.Cos(rotation);
        }

        public Transform Root()
        {
            Transform upper = this;
            while (upper.parent != null)
                upper = upper.parent;
            return upper;
        }

        public void Rotate(float degrees)
        {
            rotation += MathHelper.ToRadians(degrees);
        }

        public void Scale(Vector2f scale)
        {
            this.scale.X *= scale.X;
            this.scale.Y *= scale.Y;
        }

        public void Scale(float scale)
        {
            this.scale *= scale;
        }

        public void Scale(float x, float y)
        {
            scale.X *= x;
            scale.Y *= y;
        }

        public override string ToString()
        {
            return string.Format("{0},{1};{2};{3},{4}", position.X, position.Y, rotation, scale.X, scale.Y);
        }

        public void Translate(Vector2f translation, Space relativeSpace = Space.World)
        {
            if (relativeSpace == Space.World)
                position += translation;
            else
                position += new Vector2f(
                        cosineOfRotation * translation.X - sineOfRotation * translation.Y,
                        sineOfRotation * translation.X + cosineOfRotation * translation.Y
                    );
        }

        public void Translate(float x, float y, Space relativeSpace = Space.World)
        {
            Translate(new Vector2f(x, y), relativeSpace);
        }

        #endregion Public Methods

        #region Internal Methods

        internal override void Load(XmlReader reader)
        {
            float positionX, positionY, rotation, scaleX, scaleY;
            float.TryParse(reader.GetAttribute("Position.X"), out positionX);
            float.TryParse(reader.GetAttribute("Position.Y"), out positionY);
            if (!float.TryParse(reader.GetAttribute("Scale.X"), out scaleX))
                scaleX = 1;
            if (!float.TryParse(reader.GetAttribute("Scale.Y"), out scaleY))
                scaleY = 1;
            float.TryParse(reader.GetAttribute("Rotation"), out rotation);

            Rotation = rotation;
            Position = new Vector2f(positionX, positionY);
            scale = new Vector2f(scaleX, scaleY);
        }

        #endregion Internal Methods

        #region Private Classes

        private sealed class InnerEnumerator : IEnumerator
        {
            #region Private Fields

            private int currentIndex;
            private Transform outer;

            #endregion Private Fields

            #region Internal Constructors

            internal InnerEnumerator(Transform outer)
            {
                this.outer = outer;
            }

            #endregion Internal Constructors

            #region Public Properties

            public object Current
            {
                get
                {
                    return this.outer.GetChild(this.currentIndex);
                }
            }

            #endregion Public Properties

            #region Public Methods

            public bool MoveNext()
            {
                int childCount = outer.ChildCount;
                return ++this.currentIndex < childCount;
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }

            #endregion Public Methods
        }

        #endregion Private Classes
    }
}