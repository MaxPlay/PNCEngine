using PNCEngine.Assets;
using PNCEngine.Rendering.Events;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace PNCEngine.Rendering
{
    public class SpriteBatch
    {
        #region Private Fields

        private bool beginCalled;
        private List<Drawable> sprites;
        private RenderTarget target;

        #endregion Private Fields

        #region Public Constructors

        public SpriteBatch() : this(null)
        {
        }

        public SpriteBatch(RenderTarget target)
        {
            sprites = new List<Drawable>(50);
            this.target = target;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<RenderTargetEventArgs> BeginBatch;

        public event EventHandler<RenderTargetEventArgs> EndBatch;

        public event EventHandler<RenderTargetEventArgs> PostRender;

        public event EventHandler<RenderTargetEventArgs> PreRender;

        public event EventHandler<RenderTargetEventArgs> TargetChanged;

        #endregion Public Events

        #region Public Properties

        public bool BeginCalled { get { return beginCalled; } }
        public RenderTarget Target { get { return target; } set { if (target == value) return; target = value; OnTargetChanged(); } }

        #endregion Public Properties

        #region Public Methods

        public void Begin()
        {
            beginCalled = true;
            OnBeginBatch();
        }

        public void Draw(TextureAsset asset, Vector2f position, Color color)
        {
            Sprite sprite = new Sprite(asset.Resource);
            sprite.Position = position;
            sprite.Color = color;

            sprites.Add(sprite);
        }

        public void Draw(TextureAsset asset, Vector2f position, Color color, float rotation, Vector2f scale)
        {
            Sprite sprite = new Sprite(asset.Resource);
            sprite.Position = position;
            sprite.Color = color;
            sprite.Rotation = rotation;
            sprite.Scale = scale;

            sprites.Add(sprite);
        }

        public void Draw(TextureAsset asset, Vector2f position, Color color, float rotation, Vector2f scale, Vector2f origin)
        {
            Sprite sprite = new Sprite(asset.Resource);
            sprite.Position = position;
            sprite.Color = color;
            sprite.Rotation = rotation;
            sprite.Scale = scale;
            sprite.Origin = origin;

            sprites.Add(sprite);
        }

        public void DrawSprite(TextureAsset asset, Vector2f position, Color color, int frame)
        {
            Sprite sprite = new Sprite(asset.Resource);
            sprite.Position = position;
            sprite.Color = color;
            sprite.TextureRect = asset.GetSprite(frame);

            sprites.Add(sprite);
        }

        public void DrawSprite(TextureAsset asset, Vector2f position, Color color, float rotation, Vector2f scale, Vector2f origin, int frame)
        {
            Sprite sprite = new Sprite(asset.Resource);
            sprite.Position = position;
            sprite.Color = color;
            sprite.Rotation = rotation;
            sprite.Scale = scale;
            sprite.Origin = origin;
            sprite.TextureRect = asset.GetSprite(frame);

            sprites.Add(sprite);
        }

        public void DrawSprite(TextureAsset asset, Vector2f position, Color color, float rotation, Vector2f scale, int frame)
        {
            Sprite sprite = new Sprite(asset.Resource);
            sprite.Position = position;
            sprite.Color = color;
            sprite.Rotation = rotation;
            sprite.Scale = scale;
            sprite.TextureRect = asset.GetSprite(frame);

            sprites.Add(sprite);
        }

        public void DrawString(FontAsset asset, string text, Vector2f position, Color color, uint size = 20)
        {
            Text textSprite = new Text(text, asset.Resource, size);
            textSprite.Position = position;
            textSprite.Color = color;

            sprites.Add(textSprite);
        }

        public void DrawString(FontAsset asset, string text, Vector2f position, Color color, uint size, Text.Styles style)
        {
            Text textSprite = new Text(text, asset.Resource, size);
            textSprite.Position = position;
            textSprite.Color = color;
            textSprite.Style = style;

            sprites.Add(textSprite);
        }

        public void DrawString(FontAsset asset, string text, Vector2f position, Color color, uint size, Text.Styles style, float rotation)
        {
            Text textSprite = new Text(text, asset.Resource, size);
            textSprite.Position = position;
            textSprite.Color = color;
            textSprite.Style = style;
            textSprite.Rotation = rotation;

            sprites.Add(textSprite);
        }

        public void DrawString(FontAsset asset, string text, Vector2f position, Color color, uint size, Text.Styles style, float rotation, Vector2f origin)
        {
            Text textSprite = new Text(text, asset.Resource, size);
            textSprite.Position = position;
            textSprite.Color = color;
            textSprite.Style = style;
            textSprite.Rotation = rotation;
            textSprite.Origin = origin;

            sprites.Add(textSprite);
        }

        public void DrawString(FontAsset asset, string text, Vector2f position, Color color, uint size, Text.Styles style, float rotation, Origin origin)
        {
            Text textSprite = new Text(text, asset.Resource, size);
            textSprite.Position = position;
            textSprite.Color = color;
            textSprite.Style = style;
            textSprite.Rotation = rotation;
            FloatRect originRect = textSprite.GetLocalBounds();
            Vector2f originPosition = new Vector2f();
            switch (origin)
            {
                case Origin.TopCenter:
                    originPosition.X = originRect.Width / 2f;
                    break;

                case Origin.TopRight:
                    originPosition.X = originRect.Width;
                    break;

                case Origin.MiddleLeft:
                    originPosition.Y = originRect.Height / 2f;
                    break;

                case Origin.Center:
                    originPosition.X = originRect.Width / 2f;
                    originPosition.Y = originRect.Height / 2f;
                    break;

                case Origin.MiddleRight:
                    originPosition.X = originRect.Width;
                    originPosition.Y = originRect.Height / 2f;
                    break;

                case Origin.BottomLeft:
                    originPosition.Y = originRect.Height;
                    break;

                case Origin.BottomCenter:
                    originPosition.X = originRect.Width / 2f;
                    originPosition.Y = originRect.Height;
                    break;

                case Origin.BottomRight:
                    originPosition.X = originRect.Width;
                    originPosition.Y = originRect.Height;
                    break;
            }

            textSprite.Origin = originPosition;

            sprites.Add(textSprite);
        }

        public void End()
        {
            OnEndBatch();
            OnPreRender();
            RenderBatch();
            OnPostRender();
        }

        #endregion Public Methods

        #region Private Methods

        private void OnBeginBatch()
        {
            BeginBatch?.Invoke(this, new RenderTargetEventArgs(target));
        }

        private void OnEndBatch()
        {
            EndBatch?.Invoke(this, new RenderTargetEventArgs(target));
        }

        private void OnPostRender()
        {
            PostRender?.Invoke(this, new RenderTargetEventArgs(target));
        }

        private void OnPreRender()
        {
            PreRender?.Invoke(this, new RenderTargetEventArgs(target));
        }

        private void OnTargetChanged()
        {
            TargetChanged?.Invoke(this, new RenderTargetEventArgs(target));
        }

        private void RenderBatch()
        {
            foreach (Drawable item in sprites)
            {
                target.Draw(item);
            }
            sprites.Clear();
        }

        #endregion Private Methods
    }
}