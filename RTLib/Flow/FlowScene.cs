using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Flow.Modules;

namespace RTLib.Flow
{
    public class FlowScene
    {
        public IDictionary<string, IFlowValue> Variables = new Dictionary<string, IFlowValue>();

        public IDictionary<string, ContextModule> Contexts = new Dictionary<string, ContextModule>();
    }
}
