using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTLib.Flow
{
    public static class FlowUtilities
    {
        public static T BuildParameter<T>(FlowScene scene, IDictionary<string, IFlowValue> parameters, string parameterName,
            bool required = true, T defaultValue = default(T))
        {
            IFlowValue flowValue = null;
            if (!parameters.TryGetValue(parameterName, out flowValue))
            {
                if(required)
                    throw new FlowException(string.Format("Parameter {0} is required", parameterName));
                else
                {
                    return defaultValue;
                }
            }

            object value = flowValue.GetValue(scene);
            if (value is T)
            {
                return (T) value;
            }
            else
            {
                throw new FlowException(string.Format("Cannot convert parameter {0} to type {1} (got {2})",
                    parameterName, typeof (T).FullName, value.GetType().FullName));
            }
        }
    }
}
