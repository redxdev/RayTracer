using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow
{
    [Serializable]
    public class FlowException : Exception
    {
        public FlowException() : base()
        {
        }

        public FlowException(string message) : base(message)
        {
        }

        public FlowException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
