using System.Xml;

namespace PNCEngine.Core.Components.Audio
{
    public class AudioListener : Component
    {
        #region Public Constructors

        public AudioListener(GameObject gameObject) : base(gameObject)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Reset()
        {
        }

        #endregion Public Methods

        #region Internal Methods

        internal override void Load(XmlReader reader)
        {
        }

        #endregion Internal Methods
    }
}