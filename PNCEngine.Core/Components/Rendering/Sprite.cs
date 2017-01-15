using PNCEngine.Assets;
using PNCEngine.Rendering;
using PNCEngine.Utils;
using SFML.Graphics;
using SFML.System;

namespace PNCEngine.Core.Components.Rendering
{
    public class Sprite : Component
    {
        #region Private Fields

        private Color color;
        private Origin origin;
        private Vector2f physicalOrigin;
        private int texture;

        #endregion Private Fields

        #region Public Constructors

        public Sprite(GameObject gameObject) : base(gameObject)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Origin Origin
        {
            get { return origin; }
            set { if (origin == value) return; origin = value; UpdateOrigin(); }
        }

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public Vector2f GetOrigin()
        {
            return physicalOrigin;
        }

        public Vector2f GetOrigin(Space space)
        {
            return physicalOrigin + (space == Space.World ? GameObject.Transform.Position : new Vector2f());
        }

        public override void Reset()
        {
            texture = -1;
            color = Color.White;
            origin = Origin.TopLeft;
            UpdateOrigin();
        }

        #endregion Public Methods

        #region Private Methods

        private void UpdateOrigin()
        {
            TextureAsset tex = AssetManager.GetTexture(texture);

            switch (origin)
            {
                case Origin.TopLeft:
                    physicalOrigin = new Vector2f();
                    break;

                case Origin.TopCenter:
                    physicalOrigin = new Vector2f(0, 0.5f * tex.Height);
                    break;

                case Origin.TopRight:
                    physicalOrigin = new Vector2f(0, tex.Height);
                    break;

                case Origin.MiddleLeft:
                    physicalOrigin = new Vector2f(0.5f * tex.Width, 0);
                    break;

                case Origin.Center:
                    physicalOrigin = new Vector2f(0.5f * tex.Width, 0.5f * tex.Height);
                    break;

                case Origin.MiddleRight:
                    physicalOrigin = new Vector2f(0.5f * tex.Width, tex.Height);
                    break;

                case Origin.BottomLeft:
                    physicalOrigin = new Vector2f(tex.Width, 0);
                    break;

                case Origin.BottomCenter:
                    physicalOrigin = new Vector2f(tex.Width, 0.5f * tex.Height);
                    break;

                case Origin.BottomRight:
                    physicalOrigin = new Vector2f(tex.Width, tex.Height);
                    break;
            }
        }

        #endregion Private Methods
    }
}