using PNCEngine.Utils.Extensions;
using SFML.Graphics;

namespace PNCEngine.Rendering.Extensions
{
    public static class ColorExtension
    {
        #region Public Methods

        public static Color SetAlpha(this Color a, float b)
        {
            byte newAlpha = (byte)(a.A * b.Clamp(0, 1));

            return new Color(a.R, a.G, a.B, newAlpha);
        }

        #endregion Public Methods
    }
}