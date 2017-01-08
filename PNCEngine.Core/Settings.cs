using PNCEngine.Core.Events;
using PNCEngine.Exceptions;
using PNCEngine.Utils;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;

namespace PNCEngine.Core
{
    public sealed class Settings
    {
        #region Public Fields

        public const int DEFAULT_ANTIALIASING = 0;

        public const int DEFAULT_DEPTH = 32;

        public const bool DEFAULT_FULLSCREEN = false;

        public const int DEFAULT_HEIGHT = 600;

        public const bool DEFAULT_VSYNC = false;

        public const int DEFAULT_WIDTH = 800;

        public const string FILENAME = "settings.pref";

        #endregion Public Fields

        #region Private Fields

        private static List<GameObject> gameObjects;
        private static Settings instance;
        private int antialiasing;
        private int depth;

        private Dictionary<string, Lines> elements;

        private bool fullscreen;

        private int height;

        private VideoMode videoMode;

        private bool vsync;

        private int width;

        #endregion Private Fields

        #region Public Constructors

        public Settings()
        {
            if (instance != null)
                throw new SingletonAlreadyExistsException("Settings");
            instance = this;

            elements = new Dictionary<string, Lines>();
            elements.Add("aa", Lines.antialiasing);
            elements.Add("antialiasing", Lines.antialiasing);
            elements.Add("depth", Lines.depth);
            elements.Add("fs", Lines.fullscreen);
            elements.Add("fullscreen", Lines.fullscreen);
            elements.Add("height", Lines.height);
            elements.Add("vs", Lines.vsync);
            elements.Add("vsync", Lines.vsync);
            elements.Add("width", Lines.width);

            gameObjects = new List<GameObject>();

            Load();
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<SettingsEventArgs> Applied;

        #endregion Public Events

        #region Private Enums

        private enum Lines
        {
            antialiasing,
            depth,
            vsync,

            fullscreen,
            height,
            width
        }

        #endregion Private Enums

        #region Public Properties

        public static Settings Instance
        {
            get { return instance; }
        }

        public int Antialiasing
        {
            get { return antialiasing; }
            set { antialiasing = value; }
        }

        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public string Filename { get { return FILENAME; } }

        public bool Fullscreen
        {
            get { return fullscreen; }
            set { fullscreen = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public VideoMode VideoMode
        {
            get { return videoMode; }
            set { videoMode = value; }
        }

        public bool Vsync
        {
            get { return vsync; }
            set { vsync = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Apply()
        {
            videoMode = new VideoMode((uint)width, (uint)height, (uint)depth);
            OnApply();
        }

        public void Load()
        {
            if (File.Exists(FILENAME))
                Parse(File.ReadAllLines(FILENAME));
            else
                LoadDefaults();
            videoMode = new VideoMode((uint)width, (uint)height, (uint)depth);

            Debug.Log("Settings loaded:");
            Debug.Log("VideoMode");
            Debug.Log("Bits Per Pixel: {0}", videoMode.BitsPerPixel);
            Debug.Log("Width: {0}", videoMode.Width);
            Debug.Log("Height: {0}{1}", videoMode.Height, Environment.NewLine);
            Debug.Log("Antialiasing: {0}", antialiasing);
            Debug.Log("Fullscreen: {0}", fullscreen);
            Debug.Log("Vsync: {0}{1}", vsync, Environment.NewLine);
        }

        public void LoadDefaults()
        {
            antialiasing = DEFAULT_ANTIALIASING;
            depth = DEFAULT_DEPTH;
            fullscreen = DEFAULT_FULLSCREEN;
            height = DEFAULT_HEIGHT;
            vsync = DEFAULT_VSYNC;
            width = DEFAULT_WIDTH;
        }

        public void Save()
        {
            using (StreamWriter writer = File.CreateText(FILENAME))
            {
                writer.WriteLine("{0}={1}", "antialiasing", antialiasing);
                writer.WriteLine("{0}={1}", "depth", depth);
                writer.WriteLine("{0}={1}", "fullscreen", fullscreen ? 1 : 0);
                writer.WriteLine("{0}={1}", "height", height);
                writer.WriteLine("{0}={1}", "vsync", vsync ? 1 : 0);
                writer.WriteLine("{0}={1}", "width", width);
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal static void RegisterGameObject(GameObject obj)
        {
            if (!gameObjects.Contains(obj))
                gameObjects.Add(obj);
        }

        internal static void UnregisterGameObject(GameObject obj)
        {
            gameObjects.Remove(obj);
        }

        #endregion Internal Methods

        #region Private Methods

        private void OnApply()
        {
            Applied?.Invoke(this, new SettingsEventArgs(this));
        }

        private void Parse(string[] content)
        {
            for (int i = 0; i < content.Length; i++)
            {
                string[] linedata = content[i].Split('=');

                string identifier = linedata[0].ToLower();

                if (elements.ContainsKey(identifier))
                    switch (elements[identifier])
                    {
                        case Lines.antialiasing:
                            setInt32(ref antialiasing, linedata, DEFAULT_ANTIALIASING);
                            break;

                        case Lines.vsync:
                            setBoolean(ref vsync, linedata, DEFAULT_VSYNC);
                            break;

                        case Lines.fullscreen:
                            setBoolean(ref fullscreen, linedata, DEFAULT_FULLSCREEN);
                            break;

                        case Lines.height:
                            setInt32(ref height, linedata, DEFAULT_HEIGHT);
                            break;

                        case Lines.width:
                            setInt32(ref width, linedata, DEFAULT_WIDTH);
                            break;

                        case Lines.depth:
                            setInt32(ref depth, linedata, DEFAULT_DEPTH);
                            break;
                    }
            }
        }

        private void setBoolean(ref bool field, string[] linedata, bool defaultvalue)
        {
            if (linedata.Length > 1)
            {
                int value = 0;
                if (!int.TryParse(linedata[1], out value))
                    field = defaultvalue;
                else
                    field = value > 0;
            }
            else
                field = defaultvalue;
        }

        private void setInt32(ref int field, string[] linedata, int defaultvalue)
        {
            if (linedata.Length > 1)
            {
                if (!int.TryParse(linedata[1], out field))
                    field = defaultvalue;
            }
            else
                field = defaultvalue;
        }

        #endregion Private Methods
    }
}