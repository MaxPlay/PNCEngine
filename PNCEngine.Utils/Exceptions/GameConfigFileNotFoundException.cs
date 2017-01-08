using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Utils.Exceptions
{
    public class GameConfigFileNotFoundException : FileNotFoundException
    {
        #region Public Constructors

        public GameConfigFileNotFoundException(string filename) : base("Game could not be loaded. No gameconfig present.", filename)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected GameConfigFileNotFoundException() : base()
        {
        }

        protected GameConfigFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}
