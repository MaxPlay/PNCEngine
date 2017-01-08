using PNCEngine.Assets;
using PNCEngine.Rendering;
using PNCEngine.Utils.Exceptions;
using System.IO;
using System;
using SFML.Graphics;
using System.Xml;
using System.IO.Compression;
using PNCEngine.Utils;

namespace PNCEngine.Core.Scenes
{
    public class Scene
    {
        #region Private Fields

        private string filename;
        private int[] loadedAudio;
        private int[] loadedTextures;
        private string name;
        private Scenegraph scenegraph;
        private SpriteBatch spriteBatch;

        #endregion Private Fields

        #region Public Constructors

        public Scene()
        {
            scenegraph = new Scenegraph(spriteBatch);
        }

        public Scene(string name, string filename) : this()
        {
            this.name = name;
            this.filename = filename;
        }

        public void SetRenderTarget(RenderTarget target)
        {
            spriteBatch = null;
            GC.Collect();
            spriteBatch = new SpriteBatch(target);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Filename
        {
            get { return filename; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Scenegraph Scenegraph
        {
            get { return scenegraph; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Draw(float elapsedTime)
        {
            scenegraph.Draw(elapsedTime);
        }

        public void FixedUpdate(float elapsedTime)
        {
            scenegraph.FixedUpdate(elapsedTime);
        }

        public void Load()
        {
            if (!File.Exists(filename))
                throw new SceneFileNotFoundException(filename);

            string content = ZipCompressor.Unzip(File.ReadAllBytes(filename));
            
            using (StringReader stream = new StringReader(content))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    LoadTextures(reader);
                    LoadAudio(reader);
                    LoadEntities(reader);
                }
            }
        }

        public void Update(float elapsedTime)
        {
            scenegraph.Update(elapsedTime);
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadAudio(XmlReader reader)
        {

        }

        private void LoadEntities(XmlReader reader)
        {

        }

        private void LoadTextures(XmlReader reader)
        {

        }

        #endregion Private Methods
    }
}