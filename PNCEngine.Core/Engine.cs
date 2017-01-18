using PNCEngine.Assets;
using PNCEngine.Core.Scenes;
using PNCEngine.Exceptions;
using PNCEngine.Utils;
using SFML.System;
using SFML.Window;
using System;

namespace PNCEngine.Core
{
    public class Engine : IDisposable
    {
        #region Private Fields

        private static Engine instance;

        private Clock clock;
        private float elapsedFixedUpdateTime;
        private float fixedUpdateTime;
        private bool running;
        private EngineWindow window;

        #endregion Private Fields

        #region Public Constructors

        public Engine(string[] args)
        {
            running = true;
            if (instance != null)
                throw new SingletonAlreadyExistsException("Engine");
            instance = this;
            new Debug();
            Debug.Log("Engine initialized.");
            new Settings();
            window = new EngineWindow("PNCEngine");
            try
            {
                AssetManager.Initialize();
                SceneManager.TargetWindow = window;
                SceneManager.Initialize();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                running = false;
            }
            clock = new Clock();
            fixedUpdateTime = 0.2f;
            window.SettingsChanged += Window_SettingsChanged;
        }

        #endregion Public Constructors

        #region Public Properties

        public static Engine Instance
        {
            get { return instance; }
        }

        public EngineWindow Window
        {
            get { return window; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            if (window.IsOpen)
                window.Close();
            Settings.Instance.Save();
            Debug.Instance.Save();
        }

        public void Draw()
        {
            window.Clear();
            SceneManager.Draw();
        }

        public void FixedUpdate()
        {
            SceneManager.FixedUpdate();
        }

        public void Run()
        {
            Time elapsedTime = clock.ElapsedTime;
            clock.Restart();
            while (window.IsOpen && running)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    running = false;

                window.DispatchEvents();
                Update(elapsedTime);
                Draw();
                window.Display();
            }
        }

        public void Update(Time elapsedTime)
        {
            elapsedFixedUpdateTime += elapsedTime.AsSeconds();
            if (elapsedFixedUpdateTime >= fixedUpdateTime)
            {
                FixedUpdate();
                GameTime.ElapsedFixedTime = elapsedFixedUpdateTime;
                elapsedFixedUpdateTime = 0;
            }
            GameTime.ElapsedTime = elapsedTime.AsSeconds();
            SceneManager.Update();
        }

        #endregion Public Methods

        #region Private Methods

        private void Window_SettingsChanged(object sender, Events.WindowSettingsChangedArgs e)
        {
            if (e.Fullscreen == window.Fullscreen)
                window = new EngineWindow(window.SystemHandle, e.Settings, e.VideoMode, e.Vsync);
            else
            {
                string title = window.Title;
                window.Close();
                if (e.Fullscreen)
                    window = new EngineWindow(e.VideoMode, title, e.Vsync, e.Settings, Styles.Fullscreen);
                else
                    window = new EngineWindow(e.VideoMode, title, e.Vsync, e.Settings, Styles.Titlebar | Styles.Close);
            }
        }

        #endregion Private Methods
    }
}