using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PNCEngine.Core.Parser
{
    public class ComponentIndexer
    {
        #region Private Fields

        private Dictionary<string, Type> types;

        #endregion Private Fields

        #region Public Constructors

        public ComponentIndexer()
        {
            types = (from t in Assembly.GetExecutingAssembly().GetTypes() where t.IsClass && typeof(Component).IsAssignableFrom(t) && !t.IsAbstract select t).ToDictionary(f => f.Name.ToLower());
        }

        #endregion Public Constructors

        #region Public Methods

        public Component GetComponentByName(string name, GameObject gameObject)
        {
            string lowerName = name.ToLower();

            if (types.ContainsKey(lowerName))
                return Activator.CreateInstance(types[lowerName], gameObject) as Component;

            return null;
        }

        #endregion Public Methods
    }
}