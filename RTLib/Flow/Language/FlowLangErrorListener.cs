using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace RTLib.Flow.Language
{
    public class FlowLangErrorListener : BaseErrorListener
    {
        public static readonly FlowLangErrorListener Instance = new FlowLangErrorListener();

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new FlowLanguageException(string.Format("Syntax error at {0}:{1} - {2}", line, charPositionInLine, msg));
        }
    }
}
