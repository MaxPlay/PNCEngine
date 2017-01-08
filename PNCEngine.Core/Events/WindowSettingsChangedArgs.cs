using SFML.Window;
using System;

namespace PNCEngine.Core.Events
{
    public class WindowSettingsChangedArgs : EventArgs
    {
        #region Private Fields

        private bool fullscreen;
        private ContextSettings settings;
        private VideoMode videomode;
        private bool vsync;

        #endregion Private Fields

        #region Public Constructors

        public WindowSettingsChangedArgs(ContextSettings settings, VideoMode videomode, bool vsync, bool fullscreen)
        {
            this.settings = settings;
            this.videomode = videomode;
            this.vsync = vsync;
            this.fullscreen = fullscreen;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Fullscreen { get { return fullscreen; } }
        public ContextSettings Settings { get { return settings; } }
        public VideoMode VideoMode { get { return videomode; } }
        public bool Vsync { get { return vsync; } }

        #endregion Public Properties
    }
}