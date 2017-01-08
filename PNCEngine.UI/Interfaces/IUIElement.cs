namespace PNCEngine.UI.Interfaces
{
    public interface IUIElement
    {
        #region Public Properties

        UIManager Manager { get; set; }
        string Name { get; set; }

        #endregion Public Properties
    }
}