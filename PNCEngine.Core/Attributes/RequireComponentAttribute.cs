using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequireComponentAttribute : Attribute
    {
        private Type requiredType;

        public Type RequiredType
        {
            get { return requiredType; }
        }

        public RequireComponentAttribute(Type requireComponent)
        {
            if (requireComponent.IsSubclassOf(typeof(Component)))
                this.requiredType = requireComponent;
        }
    }
}
