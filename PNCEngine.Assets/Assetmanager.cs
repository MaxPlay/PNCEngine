using PNCEngine.Utils;
using PNCEngine.Utils.Exceptions;
using System.Collections.Generic;
using System.IO;

namespace PNCEngine.Assets
{
    public static class AssetManager
    {
        #region Private Fields

        private static Dictionary<long, AudioAsset> audios;
        private static AudioAsset defaultAudio;
        private static FontAsset defaultFont;
        private static TextureAsset defaultTexture;
        private static Dictionary<long, FontAsset> fonts;
        private static Dictionary<long, TextureAsset> textures;

        #endregion Private Fields

        #region Public Methods

        public static int AquireAudio(string filename)
        {
            foreach (int id in audios.Keys)
            {
                if (audios[id].Filename == filename)
                    return id;
            }

            AudioAsset asset = new AudioAsset(filename);
            try
            {
                asset.Load();
                audios.Add(asset.ID, asset);
                return (int)asset.ID;
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError("Asset couldn't be loaded. Exception thrown:");
                Debug.LogError(e.Message);
                return -1;
            }
        }

        public static int AquireFont(string filename)
        {
            foreach (int id in audios.Keys)
            {
                if (audios[id].Filename == filename)
                    return id;
            }

            FontAsset asset = new FontAsset(filename);
            try
            {
                asset.Load();
                fonts.Add(asset.ID, asset);
                return (int)asset.ID;
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError("Asset couldn't be loaded. Exception thrown:");
                Debug.LogError(e.Message);
                return -1;
            }
        }

        public static int AquireTexture(string filename)
        {
            foreach (int id in textures.Keys)
            {
                if (textures[id].Filename == filename)
                    return id;
            }

            TextureAsset asset = new TextureAsset(filename);
            try
            {
                asset.Load();
                textures.Add(asset.ID, asset);
                return (int)asset.ID;
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError("Asset couldn't be loaded. Exception thrown:");
                Debug.LogError(e.Message);
                return -1;
            }
        }

        public static AudioAsset GetAudio(int id)
        {
            if (audios.ContainsKey(id))
                return audios[id];
            else
                return defaultAudio;
        }

        public static FontAsset GetFont(int id)
        {
            if (fonts.ContainsKey(id))
                return fonts[id];
            else
                return defaultFont;
        }

        public static TextureAsset GetTexture(int id)
        {
            if (textures.ContainsKey(id))
                return textures[id];
            else
                return defaultTexture;
        }

        public static void Initialize()
        {
            textures = new Dictionary<long, TextureAsset>();
            audios = new Dictionary<long, AudioAsset>();
            fonts = new Dictionary<long, FontAsset>();
            defaultTexture = new TextureAsset("Resources/Textures/default.png");
            defaultAudio = new AudioAsset("Resources/Audio/default.ogg");
            defaultFont = new FontAsset("Resources/Font/default.ttf");
            try
            {
                defaultTexture.Load();
                defaultAudio.Load();
                defaultFont.Load();
            }
            catch (FileNotFoundException e)
            {
                Debug.LogError(e.Message);
                throw new NotInitializedException("The default resources couldn't be found. They are required to run the application. Assetmanager initialization cancelled.");
            }
        }

        #endregion Public Methods
    }
}