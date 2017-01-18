using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PNCEngine.Core.Parser.Exceptions
{
    public class ParseException : Exception
    {
        private ParseException()
        {

        }

        public ParseException(string name) : base(string.Format("The type {0} could not be created, because it is not valid.", name))
        {

        }
        
        private ParseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
