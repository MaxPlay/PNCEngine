using PNCEngine.Assets;
using PNCEngine.Core.Events;
using SFML.System;
using System.Xml;

namespace PNCEngine.Core.Components.Rendering
{
    public class SpriteRenderer : Renderer
    {
        #region Private Fields

        private bool flipX;
        private bool flipY;
        private Sprite sprite;

        private Transform transform;

        #endregion Private Fields

        #region Public Constructors

        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {
            gameObject.Drawed += Draw;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool FlipX
        {
            get { return flipX; }
            set { flipX = value; }
        }

        public bool FlipY
        {
            get { return flipY; }
            set { flipY = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Reset()
        {
            transform = GameObject.Transform;
        }

        #endregion Public Methods

        #region Internal Methods

        internal override void Load(XmlReader reader)
        {
            bool.TryParse(reader.GetAttribute("FlipX"), out flipX);
            bool.TryParse(reader.GetAttribute("FlipY"), out flipY);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected override void Draw(DrawingEventArgs e)
        {
            if (sprite == null)
                sprite = GetComponent<Sprite>();

            TextureAsset asset = AssetManager.GetTexture(sprite.Texture);

            Vector2f scale = transform.LossyScale;
            scale.X *= flipX ? -1 : 1;
            scale.Y *= flipY ? -1 : 1;
            e.SpriteBatch.Draw(asset, transform.Position, sprite.Color, transform.Rotation, scale, sprite.GetOrigin());
        }

        #endregion Protected Methods
    }
}