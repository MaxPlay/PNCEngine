using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNCEngine.Core.Events;
using PNCEngine.Assets;

namespace PNCEngine.Core.Components.Rendering
{
    public class SpriteRenderer : Renderer
    {
        private Sprite sprite;
        private Transform transform;

        public override void Reset()
        {
            transform = GetComponent<Transform>();
        }

        protected override void Draw(DrawingEventArgs e)
        {
            if (sprite == null)
                sprite = GetComponent<Sprite>();

            TextureAsset asset = AssetManager.GetTexture(sprite.Texture);
            e.SpriteBatch.Draw(AssetManager.GetTexture(sprite.Texture), transform.Position, sprite.Color, transform.Rotation, transform.LossyScale, sprite.GetOrigin());
        }
    }
}
