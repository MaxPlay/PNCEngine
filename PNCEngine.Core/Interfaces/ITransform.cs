using SFML.System;

namespace PNCEngine.Core.Interfaces
{
    public interface ITransform
    {
        #region Public Properties

        Vector2f Position { get; set; }
        float Rotation { get; set; }
        Vector2f Scale { get; set; }

        #endregion Public Properties
    }
}