using SFML.Graphics;
using System.Collections.Generic;
using System.IO;
using System;
using SFML.System;
using System.Xml;
using PNCEngine.Assets.Importers;

namespace PNCEngine.Assets
{
    public class TextureAsset : Asset<Texture>
    {
        #region Private Fields

        private int height;

        private int width;

        private bool repeated;

        public bool Repeated
        {
            get { return repeated; }
            set { repeated = value; resource.Repeated = value; }
        }

        private bool smooth;

        public bool Smooth
        {
            get { return smooth; }
            set { smooth = value; resource.Smooth = value; }
        }

        private Dictionary<int, IntRect> sprites;

        public Dictionary<int, IntRect> Sprites
        {
            get { return sprites; }
        }

        #endregion Private Fields

        #region Public Constructors

        public TextureAsset(string filename) : base(filename)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public int Height
        {
            get { return height; }
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

        public IntRect GetSprite(int frame)
        {
            if (sprites == null || !sprites.ContainsKey(frame))
                return new IntRect(new Vector2i(), new Vector2i((int)resource.Size.X, (int)resource.Size.Y));
            return sprites[frame];
        }

        #endregion Public Methods
    }
}