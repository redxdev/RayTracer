using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using RTLib.Flow.Language;

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


        public static void ParseString(string input)
        {
            Parse(new AntlrInputStream(input));
        }

        public static void ParseFile(string filename)
        {
            Parse(new AntlrFileStream(filename));
        }

        public static void Parse(ICharStream input)
        {
            FlowLangLexer lexer = new FlowLangLexer(input);

            CommonTokenStream tokenStream = new CommonTokenStream(lexer);

            FlowLangParser parser = new FlowLangParser(tokenStream);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(FlowLangErrorListener.Instance);

            parser.compileUnit();
        }
    }
}
