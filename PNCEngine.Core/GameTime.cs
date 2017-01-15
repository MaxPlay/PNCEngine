using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Core
{
    public static class GameTime
    {
        private static float elapsedTime;

        public static float ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        private static float elapsedFixedTime;

        public static float ElapsedFixedTime
        {
            get { return elapsedFixedTime; }
            set { elapsedFixedTime = value; }
        }
    }
}
