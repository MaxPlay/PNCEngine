using PNCEngine.UI.Internal.Events;
using SFML.Graphics;
using System;

namespace PNCEngine.UI.Interfaces
{
    public interface IClickableUIElement : IUIElement
    {
        #region Public Events

        event EventHandler<UIClickedEventArgs> Click;

        event EventHandler<UIClickedEventArgs> DoubleClick;

        event EventHandler<UIMouseEventArgs> Entered;

        event EventHandler<UIMouseEventArgs> Exited;

        #endregion Public Events

        #region Public Properties

        FloatRect BoundingBox { get; set; }

        bool Hovered { get; set; }

        #endregion Public Properties
    }
}