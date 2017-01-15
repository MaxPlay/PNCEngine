using PNCEngine.Assets;
using PNCEngine.Core.Events;

namespace PNCEngine.Core.Components.Rendering
{
    public class SpriteRenderer : Renderer
    {
        #region Private Fields

        private Sprite sprite;

        private Transform transform;

        #endregion Private Fields

        #region Public Constructors

        public SpriteRenderer(GameObject gameObject) : base(gameObject)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Reset()
        {
            transform = GameObject.Transform;
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void Draw(DrawingEventArgs e)
        {
            if (sprite == null)
                sprite = GetComponent<Sprite>();

            TextureAsset asset = AssetManager.GetTexture(sprite.Texture);
            e.SpriteBatch.Draw(AssetManager.GetTexture(sprite.Texture), transform.Position, sprite.Color, transform.Rotation, transform.LossyScale, sprite.GetOrigin());
        }

        #endregion Protected Methods
    }
}