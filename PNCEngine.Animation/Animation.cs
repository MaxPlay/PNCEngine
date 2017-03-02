using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Animations
{
    public class Animation
    {
        Keyframes keyframes;
        TimeSpan length;
        bool looped;

        public Keyframes Keyframes { get { return keyframes; } }

        public float GetFloat(string name, float timestamp)
        {
            return keyframes.GetFloat(name, timestamp);
        }

        public bool GetBool(string name, float timestamp)
        {
            return keyframes.GetBool(name, timestamp);
        }

        public Color GetColor(string name, float timestamp)
        {
            return keyframes.GetColor(name, timestamp);
        }

        public int GetInt(string name, float timestamp)
        {
            return keyframes.GetInt(name, timestamp);
        }
    }
}
