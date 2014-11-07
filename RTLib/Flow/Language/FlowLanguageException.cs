using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow.Language
{
    [Serializable]
    public class FlowLanguageException : Exception
    {
        public FlowLanguageException()
            : base()
        {
        }

        public FlowLanguageException(string message)
            : base(message)
        {
        }

        public FlowLanguageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
