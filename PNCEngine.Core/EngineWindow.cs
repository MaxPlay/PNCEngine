using PNCEngine.Core.Events;
using SFML.Graphics;
using SFML.Window;
using System;

namespace PNCEngine.Core
{
    public class EngineWindow : RenderWindow
    {
        #region Private Fields

        private bool fullscreen;
        private string title;

        #endregion Private Fields

        #region Public Constructors

        public EngineWindow(string title)
            : base(Core.Settings.Instance.VideoMode, title, Styles.Titlebar | Styles.Close)
        {
            Core.Settings.Instance.Applied += SettingsApplied;
            SetActive(true);
            SetVisible(true);
            SetVerticalSyncEnabled(Core.Settings.Instance.Vsync);
            fullscreen = false;
        }

        public EngineWindow(IntPtr handle, ContextSettings settings, VideoMode videoMode, bool vsync) : base(handle, settings)
        {
            this.SetVerticalSyncEnabled(vsync);
            this.Size = new SFML.System.Vector2u(videoMode.Width, videoMode.Height);
        }

        public EngineWindow(VideoMode videoMode, string title, bool vsync, ContextSettings settings, Styles styles) : base(videoMode, title, styles, settings)
        {
            this.fullscreen = (styles & Styles.Fullscreen) == Styles.Fullscreen;
            SetVerticalSyncEnabled(vsync);
            this.title = title;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<WindowSettingsChangedArgs> SettingsChanged;

        #endregion Public Events

        #region Public Properties

        public bool Fullscreen
        {
            get { return fullscreen; }
        }

        public string Title
        {
            get { return title; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Screenshot(string filename)
        {
            Image image = Capture();
            image.SaveToFile(filename);
        }

        #endregion Public Methods

        #region Protected Methods

        protected void OnSettingsChanged(ContextSettings settings, VideoMode videomode, bool vsync, bool fullscreen)
        {
            SettingsChanged?.Invoke(this, new WindowSettingsChangedArgs(settings, videomode, vsync, fullscreen));
        }

        #endregion Protected Methods

        #region Private Methods

        private void SettingsApplied(object sender, SettingsEventArgs e)
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = (uint)e.Settings.Antialiasing;
            settings.DepthBits = (uint)e.Settings.Depth;
            settings.StencilBits = Settings.StencilBits;
            settings.MinorVersion = Settings.MinorVersion;
            settings.MajorVersion = Settings.MajorVersion;

            OnSettingsChanged(settings, e.Videomode, e.Settings.Vsync, e.Settings.Fullscreen);
        }

        #endregion Private Methods
    }
}