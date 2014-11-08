using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow
{
    public class RawValue : IFlowValue
    {
        public object Value { get; set; }

        public object GetValue(FlowScene scene)
        {
            return Value;
        }
    }
}
