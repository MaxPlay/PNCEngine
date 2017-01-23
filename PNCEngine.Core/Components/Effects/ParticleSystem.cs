using System.Xml;

namespace PNCEngine.Core.Components.Effects
{
    public class ParticleSystem : Component
    {
        #region Public Constructors

        public ParticleSystem(GameObject gameObject) : base(gameObject)
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