using System.Collections.Generic;

namespace PNCEngine.Core.Interfaces
{
    public interface IParent<T> : IEnumerable<T>
    {
        #region Public Properties

        T[] Children { get; }
        T Parent { get; set; }

        #endregion Public Properties

        #region Public Indexers

        T this[int index] { get; }

        #endregion Public Indexers
    }
}