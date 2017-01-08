namespace PNCEngine.Core.Interfaces
{
    public interface IUpdateable
    {
        #region Public Methods

        void FixedUpdate(float elapsedTime);

        void Update(float elapsedTime);

        #endregion Public Methods
    }
}