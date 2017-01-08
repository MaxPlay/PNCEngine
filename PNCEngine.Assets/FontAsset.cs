using SFML.Graphics;
using System.IO;

namespace PNCEngine.Assets
{
    public class FontAsset : Asset<Font>
    {
        #region Private Fields

        private string family;

        #endregion Private Fields

        #region Public Constructors

        public FontAsset(string filename) : base(filename)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public string Family
        {
            get { return family; }
        }

        #endregion Public Properties

        #region Public Methods

        public override Asset<Font> Clone()
        {
            FontAsset clone = (FontAsset)MemberwiseClone();
            clone.resource = new Font(resource);
            clone.family = family;
            return clone;
        }

        public Glyph GetGlyph(int codePoint, int characterSize, bool bold)
        {
            return resource.GetGlyph((uint)codePoint, (uint)characterSize, bold);
        }

        public override bool Load()
        {
            if (File.Exists(filename))
            {
                resource = new Font(filename);
                if (resource == null)
                    return false;
                family = resource.GetInfo().Family;
            }
            else
                throw new FileNotFoundException(filename);

            return resource != null;
        }

        public FloatRect GetDimensions(string text, uint size = 20)
        {
            Text tempText = new Text(text, resource, size);
            return tempText.GetLocalBounds();
        }

        public FloatRect GetDimensions(string text, uint size, Text.Styles style)
        {
            Text tempText = new Text(text, resource, size);
            tempText.Style = style;
            return tempText.GetLocalBounds();
        }

        #endregion Public Methods
    }
}