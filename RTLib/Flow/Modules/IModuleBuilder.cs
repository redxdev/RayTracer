using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow.Modules
{
    public interface IModuleBuilder
    {
        string GetModuleName();

        IFlowValue CreateModule(FlowScene scene, IDictionary<string, IFlowValue> parameters);
    }
}
