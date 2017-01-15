using PNCEngine.Core.Interfaces;
using PNCEngine.Rendering;
using System.Collections;
using System.Collections.Generic;
using System;
using PNCEngine.Core.Events;

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

        #region Public Properties

        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        #endregion Public Properties

        #region Public Methods

        public event DrawingEventHandler Drawed;
        public event UpdateEventHandler Updated;
        public event UpdateEventHandler FixedUpdated;
        public event ScenegraphEventHandler Unloaded;

        public delegate void DrawingEventHandler(DrawingEventArgs e);
        public delegate void UpdateEventHandler();
        public delegate void ScenegraphEventHandler();

        public void Draw()
        {
            spriteBatch.Begin();
            Drawed(new DrawingEventArgs(spriteBatch));
            spriteBatch.End();
        }

        public void FixedUpdate()
        {
            FixedUpdated();
        }

        public void Update()
        {
            Updated();
        }

        #endregion Public Methods

        #region Private Classes

        public void Unload()
        {
            Unloaded();
        }

        #endregion Private Classes
    }
}