using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTLib.Flow.Modules;
using RTLib.Render;
using RTLib.Scene;

namespace RTLib.Flow
{
    public class FlowScene
    {
        public string RelativePath { get; set; }

        public IDictionary<string, IFlowValue> Variables { get { return variables; } }

        private IDictionary<string, IModuleBuilder> moduleBuilders = new Dictionary<string, IModuleBuilder>();

        private IDictionary<string, IFlowValue> variables = new Dictionary<string, IFlowValue>();

        private IDictionary<string, IModuleBuilder> ModuleBuilders { get { return moduleBuilders; } }

        public void RegisterModuleBuilder(IModuleBuilder builder)
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

        public SceneGraph BuildGraph()
        {
            SceneGraph graph = new SceneGraph();

            foreach (IFlowValue value in Variables.Values)
            {
                object obj = value.GetValue(this);
                if (obj is SceneObject)
                {
                    graph.Objects.AddLast((SceneObject) obj);
                }
                else if (obj is Light)
                {
                    graph.Lights.AddLast((Light) obj);
                }
            }

            return graph;
        }

        public Camera BuildCamera()
        {
            foreach (IFlowValue value in Variables.Values)
            {
                object obj = value.GetValue(this);
                if (obj is Camera)
                    return (Camera) obj;
            }

            return null;
        }
    }
}
