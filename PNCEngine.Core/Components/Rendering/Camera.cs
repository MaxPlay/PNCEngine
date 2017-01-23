using SFML.Graphics;
using SFML.System;
using System.Xml;

namespace PNCEngine.Core.Components.Rendering
{
    public class Camera : Component
    {
        #region Private Fields

        private static Camera main;
        private Color backgroundColor;
        private RenderTarget target;
        private View view;
        private float zoom;

        #endregion Private Fields

        #region Public Constructors

        public Camera(GameObject gameObject, RenderTarget target) : base(gameObject)
        {
            Transform.ValuesChanged += UpdateMatrix;
            this.target = target;
        }

        #endregion Public Constructors

        #region Public Properties

        public static Camera Main { get { return main; } }

        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }

            set
            {
                backgroundColor = value;
            }
        }

        public View View
        {
            get
            {
                return view;
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }

            set
            {
                zoom = value;
                UpdateMatrix();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Reset()
        {
            backgroundColor = Color.Black;
        }

        public Vector2f ScreenpointToView(Vector2i point)
        {
            return new Vector2f(point.X / target.Size.X, point.Y / target.Size.Y);
        }

        public Vector2f ScreenpointToWorld(Vector2i point)
        {
            return target.MapPixelToCoords(point, view);
        }

        public Vector2i ViewToSceenpoint(Vector2f point)
        {
            return new Vector2i((int)(point.X * target.Size.X), (int)(point.Y * target.Size.Y));
        }

        public Vector2f ViewToWorld(Vector2f point)
        {
            return ScreenpointToWorld(ViewToSceenpoint(point));
        }

        public Vector2i WorldToScreenpoint(Vector2f point)
        {
            return target.MapCoordsToPixel(point, view);
        }

        public Vector2f WorldToView(Vector2f point)
        {
            return ScreenpointToView(WorldToScreenpoint(point));
        }

        #endregion Public Methods

        #region Internal Methods

        internal override void Load(XmlReader reader)
        {
            backgroundColor = PNCEngine.Rendering.Extensions.ColorExtension.FromHex(reader.GetAttribute("BackgroundColor"), Color.White);
            float.TryParse(reader.GetAttribute("Zoom"), out zoom);
            UpdateMatrix();
        }

        #endregion Internal Methods

        #region Protected Methods

        protected void UpdateMatrix()
        {
            view.Center = Transform.Position;
            view.Rotation = Transform.Rotation;
            view.Size = new Vector2f(target.Size.X * zoom, target.Size.X * zoom);
        }

        #endregion Protected Methods
    }
}