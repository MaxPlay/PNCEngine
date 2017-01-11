using SFML.System;
using System.Collections.Generic;
using System.Xml;

namespace PNCEngine.Utils.Extensions
{
    public static class VectorExtension
    {
        #region Private Fields

        private static Dictionary<string, Attributes> elements;

        #endregion Private Fields

        #region Public Constructors

        static VectorExtension()
        {
            elements = new Dictionary<string, Attributes>();
            elements.Add("x", Attributes.X);
            elements.Add("y", Attributes.Y);
        }

        #endregion Public Constructors

        #region Private Enums

        private enum Attributes
        {
            X,
            Y
        }

        #endregion Private Enums

        #region Public Methods

        public static Vector2f ReadXml(this Vector2f v2, XmlReader reader, string name)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == name.ToLower())
                    return v2;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    float value = 0f;
                    switch (elements[reader.Name])
                    {
                        case Attributes.X:
                            if (reader.IsEmptyElement)
                            {
                                v2.X = 0;
                                break;
                            }
                            reader.Read();
                            if (float.TryParse(reader.Value, out value))
                                v2.X = value;
                            break;

                        case Attributes.Y:
                            if (reader.IsEmptyElement)
                            {
                                v2.Y = 0;
                                break;
                            }
                            reader.Read();
                            if (float.TryParse(reader.Value, out value))
                                v2.Y = value;
                            break;
                    }
                }
            }
            return v2;
        }

        public static Vector2i ReadXml(this Vector2i v2, XmlReader reader, string name)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == name.ToLower())
                    return v2;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    int value = 0;
                    switch (elements[reader.Name])
                    {
                        case Attributes.X:
                            if (reader.IsEmptyElement)
                            {
                                v2.X = 0;
                                break;
                            }
                            reader.Read();
                            if (int.TryParse(reader.Value, out value))
                                v2.X = value;
                            break;

                        case Attributes.Y:
                            if (reader.IsEmptyElement)
                            {
                                v2.Y = 0;
                                break;
                            }
                            reader.Read();
                            if (int.TryParse(reader.Value, out value))
                                v2.Y = value;
                            break;
                    }
                }
            }
            return v2;
        }

        public static Vector2u ReadXml(this Vector2u v2, XmlReader reader, string name)
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == name.ToLower())
                    return v2;

                if (reader.NodeType == XmlNodeType.Element)
                {
                    uint value = 0;
                    switch (elements[reader.Name])
                    {
                        case Attributes.X:
                            if (reader.IsEmptyElement)
                            {
                                v2.X = 0;
                                break;
                            }
                            reader.Read();
                            if (uint.TryParse(reader.Value, out value))
                                v2.X = value;
                            break;

                        case Attributes.Y:
                            if (reader.IsEmptyElement)
                            {
                                v2.Y = 0;
                                break;
                            }
                            reader.Read();
                            if (uint.TryParse(reader.Value, out value))
                                v2.Y = value;
                            break;
                    }
                }
            }
            return v2;
        }

        public static void WriteXml(this Vector2f v2, XmlWriter writer, string name)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString("type", "Vector2f");
            writer.WriteElementString("x", v2.X.ToString());
            writer.WriteElementString("y", v2.Y.ToString());
            writer.WriteEndElement();
        }

        public static void WriteXml(this Vector2i v2, XmlWriter writer, string name)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString("type", "Vector2f");
            writer.WriteElementString("x", v2.X.ToString());
            writer.WriteElementString("y", v2.Y.ToString());
            writer.WriteEndElement();
        }

        public static void WriteXml(this Vector2u v2, XmlWriter writer, string name)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString("type", "Vector2f");
            writer.WriteElementString("x", v2.X.ToString());
            writer.WriteElementString("y", v2.Y.ToString());
            writer.WriteEndElement();
        }

        #endregion Public Methods
    }
}