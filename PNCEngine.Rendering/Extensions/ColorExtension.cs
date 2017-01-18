using PNCEngine.Utils.Extensions;
using SFML.Graphics;
using System;

namespace PNCEngine.Rendering.Extensions
{
    public static class ColorExtension
    {
        #region Public Methods

        public static Color FromHex(string hex, Color fallback)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return fallback;

            hex = hex.ToLower();

            if (hex[0] == '#' && hex.Length == 5)
                hex = new string(new char[] { '#', hex[1], hex[1], hex[2], hex[2], hex[3], hex[3], hex[4], hex[4] });

            if (hex[0] == '#' && hex.Length == 9)
            {
                foreach (char c in hex)
                    if (!(c == '#' || char.IsDigit(c) || c >= 'a' && c <= 'f'))
                        return fallback;

                byte[] bytes = new byte[4];
                bytes[0] = Convert.ToByte(new string(new char[] { hex[1], hex[2] }), 16);
                bytes[1] = Convert.ToByte(new string(new char[] { hex[3], hex[4] }), 16);
                bytes[2] = Convert.ToByte(new string(new char[] { hex[5], hex[6] }), 16);
                bytes[3] = Convert.ToByte(new string(new char[] { hex[7], hex[8] }), 16);

                return new Color(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            return fallback;
        }

        public static Color SetAlpha(this Color a, float b)
        {
            byte newAlpha = (byte)(a.A * b.Clamp(0, 1));

            return new Color(a.R, a.G, a.B, newAlpha);
        }

        #endregion Public Methods
    }
}