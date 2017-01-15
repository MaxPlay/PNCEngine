using PNCEngine.Core.Scenes;

namespace PNCEngine.Core.Interfaces
{
    public interface IScenegraphElement
    {
        #region Public Properties

        Scenegraph Scenegraph { get; }

        #endregion Public Properties
    }
}