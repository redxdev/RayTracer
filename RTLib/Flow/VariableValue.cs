using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow
{
    public class VariableValue : IFlowValue
    {
        public string Variable { get; set; }
        public object GetValue(FlowScene scene)
        {
            IFlowValue value = null;
            if (!scene.Variables.TryGetValue(Variable, out value))
            {
                throw new KeyNotFoundException(string.Format("Unknown variable \"{0}\"", Variable));
            }

            return value.GetValue(scene);
        }
    }
}
