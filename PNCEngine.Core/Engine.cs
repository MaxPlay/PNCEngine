﻿using PNCEngine.Assets;
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

        private bool running;
        private EngineWindow window;
        private Clock clock;
        private float elapsedFixedUpdateTime;
        private float fixedUpdateTime;

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
            try
            {
                AssetManager.Initialize();
                SceneManager.Initialize();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                running = false;
            }
            clock = new Clock();
            fixedUpdateTime = 0.2f;
            window = new EngineWindow("PNCEngine");
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
                Draw(elapsedTime);
                window.Display();
            }
        }

        public void Draw(Time elapsedTime)
        {
            window.Clear();
            SceneManager.Draw(elapsedTime.AsSeconds());
        }

        public void Update(Time elapsedTime)
        {
            elapsedFixedUpdateTime += elapsedTime.AsSeconds();
            if(elapsedFixedUpdateTime >= fixedUpdateTime)
            {
                FixedUpdate();
                elapsedFixedUpdateTime = 0;
            }
            SceneManager.Update(elapsedTime.AsSeconds());
        }

        public void FixedUpdate()
        {
            SceneManager.FixedUpdate(elapsedFixedUpdateTime);
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