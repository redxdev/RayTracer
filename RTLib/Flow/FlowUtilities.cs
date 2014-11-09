using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using RTLib.Flow.Language;
using RTLib.Flow.Modules;

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


        public static FlowScene ParseString(string input)
        {
            return Parse(new AntlrInputStream(input));
        }

        public static FlowScene ParseFile(string filename)
        {
            return Parse(new AntlrFileStream(filename));
        }

        public static FlowScene Parse(ICharStream input)
        {
            FlowLangLexer lexer = new FlowLangLexer(input);

            CommonTokenStream tokenStream = new CommonTokenStream(lexer);

            FlowLangParser parser = new FlowLangParser(tokenStream);

            parser.Scene = new FlowScene();
            List<IModuleBuilder> builders = CreateModuleBuilders();
            foreach (IModuleBuilder builder in builders)
            {
                parser.Scene.RegisterModuleBuilder(builder);
            }

            parser.RemoveErrorListeners();
            parser.AddErrorListener(FlowLangErrorListener.Instance);

            parser.compileUnit();
            return parser.Scene;
        }

        public static List<IModuleBuilder> CreateModuleBuilders()
        {
            List<IModuleBuilder> moduleBuilders = new List<IModuleBuilder>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IModuleBuilder).IsAssignableFrom(type) && type.GetCustomAttribute<ModuleAttribute>(false) != null)
                    {
                        try
                        {
                            moduleBuilders.Add((IModuleBuilder)Activator.CreateInstance(type));
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }

            return moduleBuilders;
        }
    }
}
