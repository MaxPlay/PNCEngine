using PNCEngine.Core.Interfaces;
using PNCEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace PNCEngine.Core.Scenes
{
    public class Scenegraph : IDrawable, IUpdateable
    {
        #region Private Fields

        private IScenegraphElement root;
        private SpriteBatch spriteBatch;

        #endregion Private Fields

        #region Public Constructors

        public Scenegraph(SpriteBatch spriteBatch)
        {
            root = new RootElement(this);
            this.spriteBatch = spriteBatch;
        }

        #endregion Public Constructors

        #region Public Properties

        public IScenegraphElement Root { get { return root; } }

        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        #endregion Public Properties

        #region Public Methods

        public void AddElement(IScenegraphElement element)
        {
            root.AddChild(element);
        }

        public void Draw(float elapsedTime)
        {
            spriteBatch.Begin();
            root.Draw(elapsedTime);
            spriteBatch.End();
        }

        public void FixedUpdate(float elapsedTime)
        {
            root.FixedUpdate(elapsedTime);
        }

        public void RemoveElement(IScenegraphElement element)
        {
            root.RemoveChild(element);
        }

        public void Update(float elapsedTime)
        {
            root.Update(elapsedTime);
        }

        #endregion Public Methods

        #region Private Classes

        private class RootElement : IScenegraphElement
        {
            #region Private Fields

            private List<IScenegraphElement> children;

            private Scenegraph scenegraph;

            #endregion Private Fields

            #region Public Constructors

            public RootElement(Scenegraph scenegraph)
            {
                children = new List<IScenegraphElement>();
                this.scenegraph = scenegraph;
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

            public IScenegraphElement Parent
            {
                get
                {
                    return this;
                }

                set
                {
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
                if (!children.Contains(element))
                    children.Add(element);
            }

            public void AddToScenegraph(Scenegraph scenegraph)
            {
            }

            public void Draw(float elapsedTime)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Draw(elapsedTime);
                }
            }

            public void FixedUpdate(float elapsedTime)
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
            

            public void RemoveChild(IScenegraphElement element)
            {
                children.Remove(element);
            }

            public void RemoveFromScenegraph()
            {
            }

            public void Update(float elapsedTime)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Update(elapsedTime);
                }
            }

            #endregion Public Methods
        }

        #endregion Private Classes
    }
}