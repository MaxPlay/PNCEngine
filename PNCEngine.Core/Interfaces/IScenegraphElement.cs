using PNCEngine.Core.Scenes;

namespace PNCEngine.Core.Interfaces
{
    public interface IScenegraphElement : IDrawable, IUpdateable
    {
        #region Public Properties

        Scenegraph Scenegraph { get; set; }

        #endregion Public Properties

        #region Public Methods

        void AddChild(IScenegraphElement element);

        void AddToScenegraph(Scenegraph scenegraph);

        void RemoveChild(IScenegraphElement element);

        void RemoveFromScenegraph();

        #endregion Public Methods
    }
}