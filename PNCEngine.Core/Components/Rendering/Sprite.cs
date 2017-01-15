using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using PNCEngine.Rendering;

namespace PNCEngine.Core.Components.Rendering
{
    public class Sprite : Component
    {
        private int texture;

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private Origin origin;

        public Origin Origin
        {
            get { return origin; }
            set { origin = value; UpdateOrigin(); }
        }

        private void UpdateOrigin()
        {
            throw new NotImplementedException();
        }

        public Vector2f GetOrigin()
        {
            return new Vector2f();
        }

        public override void Reset()
        {
            texture = -1;
            color = Color.White;
        }
    }
}
