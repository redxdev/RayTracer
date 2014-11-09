using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow
{
    public class GenericValue<T> : IFlowValue
    {
        public T Value { get; set; }

        public object GetValue(FlowScene scene)
        {
            return Value;
        }
    }
}
