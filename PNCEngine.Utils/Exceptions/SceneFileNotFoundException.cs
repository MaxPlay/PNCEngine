using System.IO;
using System.Runtime.Serialization;

namespace PNCEngine.Utils.Exceptions
{
    public class SceneFileNotFoundException : FileNotFoundException
    {
        #region Public Constructors

        public SceneFileNotFoundException(string filename) : base("Scene could not be loaded. The linked file does not exist.", filename)
        {
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected SceneFileNotFoundException() : base()
        {
        }

        protected SceneFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Protected Constructors
    }
}