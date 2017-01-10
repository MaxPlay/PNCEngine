using SFML.Audio;
using System.IO;
using System;
using System.Xml;

namespace PNCEngine.Assets
{
    public class AudioAsset : Asset<SoundBuffer>
    {
        #region Private Fields

        private Channels channels;

        private float duration;

        private int sampleRate;

        #endregion Private Fields

        #region Public Constructors

        public AudioAsset(string filename) : base(filename)
        {
        }

        #endregion Public Constructors

        #region Public Enums

        public enum Channels
        {
            Mono = 1,
            Stereo = 2,
            Multichannel
        }

        #endregion Public Enums

        #region Public Properties

        public Channels Channel
        {
            get { return channels; }
        }

        public float Duration { get { return duration; } }

        public int SampleRate
        {
            get { return sampleRate; }
        }

        #endregion Public Properties

        #region Public Methods

        public override Asset<SoundBuffer> Clone()
        {
            AudioAsset clone = (AudioAsset)MemberwiseClone();
            clone.resource = new SoundBuffer(resource);
            clone.assignNewID();
            return clone;
        }

        public override bool Load()
        {
            if (File.Exists(filename))
            {
                resource = new SoundBuffer(filename);
                if (resource == null)
                    return false;
                duration = resource.Duration.AsSeconds();
                sampleRate = (int)resource.SampleRate;
                switch (resource.ChannelCount)
                {
                    case 1:
                    case 2:
                        channels = (Channels)resource.ChannelCount;
                        break;

                    default:
                        channels = Channels.Multichannel;
                        break;
                }
            }
            else
                throw new FileNotFoundException(filename);

            return resource != null;
        }

        public void LoadXML(XmlReader reader)
        {

        }

        public void SaveXML(XmlWriter writer)
        {

        }

        #endregion Public Methods
    }
}