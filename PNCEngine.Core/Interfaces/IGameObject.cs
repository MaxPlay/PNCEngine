using System;

namespace PNCEngine.Core.Interfaces
{
    public interface IGameObject : ITransform, IEquatable<GameObject>, IScenegraphElement
    {
        #region Public Properties

        string Name { get; set; }

        #endregion Public Properties
    }
}