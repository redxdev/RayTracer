using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace RTLib.Flow.Language
{
    public static class LanguageUtilities
    {
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
