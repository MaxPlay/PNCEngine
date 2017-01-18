using PNCEngine.Assets;
using PNCEngine.Core.Parser;
using PNCEngine.Rendering;
using PNCEngine.Utils;
using PNCEngine.Utils.Exceptions;
using SFML.Graphics;
using System;
using System.IO;
using System.Xml;

namespace PNCEngine.Core.Scenes
{
    public class Scene
    {
        #region Public Fields

        public const string extension = ".scene";

        #endregion Public Fields

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

        public void Draw()
        {
            spriteBatch.Begin();
            scenegraph.Draw();
            spriteBatch.End();
        }

        public void FixedUpdate()
        {
            scenegraph.FixedUpdate();
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

        public void Save()
        {
            if (File.Exists(filename))
                File.Delete(filename);

            string content = string.Empty;

            using (StringWriter stream = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stream))
                {
                    SaveTextures(writer);
                    SaveAudio(writer);
                    SaveEntities(writer);
                }

                content = stream.ToString();
            }
            File.WriteAllBytes(filename, ZipCompressor.Zip(content));
        }

        public void SetRenderTarget(RenderTarget target)
        {
            spriteBatch = null;
            GC.Collect();
            spriteBatch = new SpriteBatch(target);
            scenegraph.SpriteBatch = spriteBatch;
        }

        public void Update()
        {
            scenegraph.Update();
        }

        #endregion Public Methods

        #region Internal Methods

        internal void Unload()
        {
            scenegraph.Unload();
        }

        #endregion Internal Methods

        #region Private Methods

        private void LoadAudio(XmlReader reader)
        {
            if (reader.AttributeCount > 0)
            {
                int count = 0;
                if (!int.TryParse(reader.GetAttribute("count"), out count))
                    return;
                this.loadedAudio = new int[count];
            }

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "audio")
                    return;

                if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "audiosource")
                {
                    if (reader.AttributeCount > 0)
                    {
                        int id = AssetManager.AquireAudio(reader.GetAttribute("filename"));
                        AssetManager.GetAudio(id).ReadXml(reader);
                    }
                }
            }
        }

        private void LoadEntities(XmlReader reader)
        {
            ComponentIndexer componentIndexer = new ComponentIndexer();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "entities")
                    return;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "GameObject")
                    {
                        GameObject g = new GameObject();
                        g.Load(reader, null, componentIndexer, scenegraph);
                    }
                }
            }
        }

        private void LoadTextures(XmlReader reader)
        {
            if (reader.AttributeCount > 0)
            {
                int count = 0;
                if (!int.TryParse(reader.GetAttribute("count"), out count))
                    return;
                this.loadedTextures = new int[count];
            }

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "textures")
                    return;

                if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower() == "texture")
                {
                    if (reader.AttributeCount > 0)
                    {
                        int id = AssetManager.AquireTexture(reader.GetAttribute("filename"));
                        AssetManager.GetTexture(id).ReadXml(reader);
                    }
                }
            }
        }

        private void SaveAudio(XmlWriter writer)
        {
            writer.WriteStartElement("audio");
            foreach (int i in loadedTextures)
            {
                AssetManager.GetAudio(i).WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        private void SaveEntities(XmlWriter writer)
        {
        }

        private void SaveTextures(XmlWriter writer)
        {
            writer.WriteStartElement("textures");
            foreach (int i in loadedTextures)
            {
                AssetManager.GetTexture(i).WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        #endregion Private Methods
    }
}