using System.Collections.Generic;

namespace PNCEngine.Core
{
    internal static class TagManager
    {
        #region Private Fields

        private static Dictionary<string, int> tagList;

        #endregion Private Fields

        #region Public Constructors

        static TagManager()
        {
            tagList = new Dictionary<string, int>();
        }

        #endregion Public Constructors

        #region Public Properties

        public static Dictionary<string, int> TagList { get { return tagList; } }

        #endregion Public Properties

        #region Internal Methods

        internal static bool CompareTag(string tag1, string tag2)
        {
            return tagList[tag1] == tagList[tag2];
        }

        internal static void RegisterTag(string value)
        {
            if (!tagList.ContainsKey(value))
                tagList.Add(value, tagList.Count);
        }

        #endregion Internal Methods
    }
}