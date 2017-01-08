using PNCEngine.UI.Events;
using SFML.Graphics;

namespace PNCEngine.UI.Interfaces
{
    public interface IDrawableUIElement
    {
        #region Public Properties

        Color BackgroundColor { get; set; }
        Color ForegroundColor { get; set; }
        Color HoverBackgroundColor { get; set; }
        Color HoverForegroundColor { get; set; }
        int HoverTexture { get; set; }
        int Texture { get; set; }

        bool Visible { get; set; }

        #endregion Public Properties

        #region Public Methods

        void Draw(object sender, UIDrawEventArgs args);

        #endregion Public Methods
    }
}