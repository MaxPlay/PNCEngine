using PNCEngine.Assets.Importers;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PNCEngine.Assets
{
    public class TextureAsset : Asset<Texture>, IXmlSerializable
    {
        #region Private Fields

        private static Dictionary<string, Attributes> elements;
        private int height;

        private bool repeated;
        private bool smooth;
        private Dictionary<int, IntRect> sprites;
        private int width;

        #endregion Private Fields

        #region Public Constructors

        public TextureAsset(string filename) : base(filename)
        {
            if (elements == null)
            {
                elements = new Dictionary<string, Attributes>();
                elements.Add("smooth", Attributes.Smooth);
                elements.Add("repeated", Attributes.Repeated);
            }
        }

        #endregion Public Constructors

        #region Private Enums

        private enum Attributes
        {
            Smooth,
            Repeated
        }

        #endregion Private Enums

        #region Public Properties

        public int Height
        {
            get { return height; }
        }

        public bool Repeated
        {
            get { return repeated; }
            set { repeated = value; resource.Repeated = value; }
        }

        public bool Smooth
        {
            get { return smooth; }
            set { smooth = value; resource.Smooth = value; }
        }

        public Dictionary<int, IntRect> Sprites
        {
            get { return sprites; }
        }

        public int Width
        {
            get { return width; }
        }

        #endregion Public Properties

        #region Public Methods

        public override Asset<Texture> Clone()
        {
            TextureAsset clone = (TextureAsset)MemberwiseClone();
            clone.resource = new Texture(resource.CopyToImage());
            clone.assignNewID();
            return clone;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public IntRect GetSprite(int frame)
        {
            if (sprites == null || !sprites.ContainsKey(frame))
                return new IntRect(new Vector2i(), new Vector2i((int)resource.Size.X, (int)resource.Size.Y));
            return sprites[frame];
        }

        public override bool Load()
        {
            if (File.Exists(filename))
            {
                resource = new Texture(filename);
                if (resource == null)
                    return false;

                height = (int)resource.Size.Y;
                width = (int)resource.Size.X;
            }
            else
                throw new FileNotFoundException(filename);

            SpritesheetImporter importer = SpritesheetImporter.Create(SpritesheetType.Xml);
            sprites = importer.Import(filename);

            return resource != null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "texture")
                    return;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    string element = reader.Name.ToLower();
                    if (elements.ContainsKey(element))
                        switch (elements[element])
                        {
                            case Attributes.Smooth:
                                if (reader.IsEmptyElement)
                                    return;
                                reader.Read();
                                int smooth = 0;
                                if (!int.TryParse(reader.Value, out smooth))
                                    return;
                                Smooth = smooth > 0;
                                break;

                            case Attributes.Repeated:
                                if (reader.IsEmptyElement)
                                    return;
                                reader.Read();
                                int repeated = 0;
                                if (!int.TryParse(reader.Value, out repeated))
                                    return;
                                Repeated = repeated > 0;
                                break;
                        }
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("texture");
            writer.WriteAttributeString("filename", filename);
            writer.WriteElementString("smooth", smooth.ToString());
            writer.WriteElementString("repeated", repeated.ToString());
            writer.WriteEndElement();
        }

        #endregion Public Methods
    }
}