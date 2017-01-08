using SFML.Graphics;
using System.Collections.Generic;

namespace PNCEngine.Assets.Importers
{
    public abstract class SpritesheetImporter
    {
        public abstract Dictionary<int, IntRect> Import(string filename);

        public static SpritesheetImporter Create(SpritesheetType type)
        {
            switch (type)
            {
                case SpritesheetType.Xml:
                    return new XmlSpriteSheetImporter();
                case SpritesheetType.Json:
                    return new JsonSpriteSheetImporter();
                case SpritesheetType.Txt:
                    return new TxtSpriteSheetImporter();
                case SpritesheetType.Cocos2D:
                    return new Cocos2DSpriteSheetImporter();
                case SpritesheetType.Unity:
                    return new UnitySpriteSheetImporter();
                case SpritesheetType.AppGameKit:
                    return new AppGameKitSpriteSheetImporter();
                default:
                    return null;
            }
        }
    }
}
