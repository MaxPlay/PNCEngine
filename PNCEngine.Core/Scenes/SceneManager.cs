using PNCEngine.Utils;
using PNCEngine.Utils.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace PNCEngine.Core.Scenes
{
    public static class SceneManager
    {
        #region Private Fields

        private const string SRC_FILE = "scenes.cat";
        private static Scene currentScene;
        private static Dictionary<string, string> scenes;
        private static string startup;
        private static EngineWindow targetWindow;

        #endregion Private Fields

        #region Public Properties

        public static EngineWindow TargetWindow
        {
            get { return targetWindow; }
            set { targetWindow = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public static void Draw(float elapsedTime)
        {
            currentScene?.Draw(elapsedTime);
        }

        public static void FixedUpdate(float elapsedTime)
        {
            currentScene?.FixedUpdate(elapsedTime);
        }

        public static void Initialize()
        {
            scenes = new Dictionary<string, string>();
            Load();
        }

        public static Scene LoadScene(string name)
        {
            if (!scenes.ContainsKey(name ?? ""))
            {
                Debug.LogError("The scene \"{0}\" does not exist.", name);
                return null;
            }

            Scene scene = new Scene(name, scenes[name]);
            scene.Load();
            scene.SetRenderTarget(targetWindow);
            return scene;
        }

        public static void Update(float elapsedTime)
        {
            currentScene?.Update(elapsedTime);
        }

        #endregion Public Methods

        #region Private Methods

        private static void Load()
        {
            if (!File.Exists("gameconfig.game"))
                throw new GameConfigFileNotFoundException("gameconfig.game");

            using (Stream stream = File.OpenRead("gameconfig.game"))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string name = reader.ReadString();
                        string filename = reader.ReadString();
                        scenes.Add(name, filename);
                        if (i == 0)
                            startup = name;
                    }
                }
            }

            currentScene = LoadScene(startup);
        }

        #endregion Private Methods
    }
}