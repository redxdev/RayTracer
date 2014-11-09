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
        private IDictionary<string, IModuleBuilder> moduleBuilders = new Dictionary<string, IModuleBuilder>();

        private IDictionary<string, IFlowValue> variables = new Dictionary<string, IFlowValue>();

        public IDictionary<string, IFlowValue> Variables { get { return variables; } }

        private IDictionary<string, IModuleBuilder> ModuleBuilders { get { return moduleBuilders; } }

        public void RegisterModuelBuilder(IModuleBuilder builder)
        {
            ModuleBuilders.Add(builder.GetModuleName(), builder);
        }

        public IFlowValue CreateModule(string moduleName, IDictionary<string, IFlowValue> parameters)
        {
            IModuleBuilder builder;
            if (!ModuleBuilders.TryGetValue(moduleName, out builder))
            {
                throw new KeyNotFoundException(string.Format("Unknown module \"{0}\"", moduleName));
            }

            return builder.CreateModule(this, parameters);
        }

        public void AddVariable(string name, IFlowValue value)
        {
            if (Variables.ContainsKey(name))
            {
                throw new FlowException(string.Format("Variable {0} already exists!", name));
            }

            Variables.Add(name, value);
        }
    }
}
