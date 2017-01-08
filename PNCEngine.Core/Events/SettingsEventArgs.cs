using SFML.Window;

namespace PNCEngine.Core.Events
{
    public class SettingsEventArgs
    {
        #region Private Fields

        private Settings settings;
        private VideoMode videomode;

        #endregion Private Fields

        #region Public Constructors

        public SettingsEventArgs(Settings settings)
        {
            this.settings = settings;
            this.videomode = settings.VideoMode;
        }

        #endregion Public Constructors

        #region Public Properties

        public Settings Settings { get { return settings; } }
        public VideoMode Videomode { get { return videomode; } }

        #endregion Public Properties
    }
}