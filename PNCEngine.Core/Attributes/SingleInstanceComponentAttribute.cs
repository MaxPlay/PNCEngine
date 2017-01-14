using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SingleInstanceComponentAttribute : Attribute
    {
        public SingleInstanceComponentAttribute() { }
    }
}
