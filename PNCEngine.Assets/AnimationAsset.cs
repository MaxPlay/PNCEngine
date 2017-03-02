using PNCEngine.Animations;

namespace PNCEngine.Assets
{
    public class AnimationAsset : Asset<Animation>
    {
        #region Public Constructors

        public AnimationAsset(string filename) : base(filename)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override Asset<Animation> Clone()
        {
            return null;
        }

        public override bool Load()
        {
            return false;
        }

        #endregion Public Methods
    }
}