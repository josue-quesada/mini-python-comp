using System.IO;

namespace UI;

using Antlr4.Runtime;
using System;
using System.Collections.Generic;

public class ErrorListener : BaseErrorListener, IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
{
    public List<string> ErrorMessages { get; private set; } = new List<string>();

    public bool HasErrors()
    {
        return ErrorMessages.Count > 0;
    }

    public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine,
        string msg, RecognitionException e)
    {
        if (recognizer is Antlr4.Runtime.Parser)
        {
            ErrorMessages.Add($"PARSER ERROR - line {line}:{charPositionInLine} {msg}");
        }
        else if (recognizer is Antlr4.Runtime.Lexer)
        {
            ErrorMessages.Add($"SCANNER ERROR - line {line}:{charPositionInLine} {msg}");
        }
        else
        {
            ErrorMessages.Add("Unrecognized Error");
        }
    }

    public override string ToString()
    {
        if (!HasErrors())
        {
            return "No errors";
        }

        return string.Join(Environment.NewLine, ErrorMessages);
    }
}