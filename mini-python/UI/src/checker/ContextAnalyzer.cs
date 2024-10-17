using System;
using System.Collections.Generic;
using Antlr4.Runtime;
using generated;

namespace UI.checker;

public class ContextAnalyzer : MPParserBaseVisitor<Object> {
    private SymbolTable SymbolTable;
    private List<string> ErrorList;

    public ContextAnalyzer()
    {
        SymbolTable = new SymbolTable();
        ErrorList = new List<string>();
    }

    private void reportError(string error, IToken offendingToken)
    {
        var err = new System.Text.StringBuilder();
        err.Append(error).
            Append(" ").
            Append("(Line: ").
            Append(offendingToken.Line).
            Append("; Column:").
            Append(offendingToken.Column+1).
            Append(")");
        ErrorList.Add(err.ToString());
    }

    public bool HasErrors()
    {
        return ErrorList.Count > 0;
    }
    public override object VisitProgram(MPParser.ProgramContext context)
    {
        SymbolTable.OpenScope();
        return base.VisitProgram(context);
    }

    public override object VisitMainStatement(MPParser.MainStatementContext context)
    {
        return base.VisitMainStatement(context);
    }

    public override object VisitStatement(MPParser.StatementContext context)
    {
        return base.VisitStatement(context);
    }

    public override object VisitDefStatement(MPParser.DefStatementContext context)
    {
        string functionName = context.IDENTIFIER().GetText();
        if (SymbolTable.SearchCurrentLevel(functionName) != null) {
            reportError($"The function '{functionName}' is already defined in this scope.", context.IDENTIFIER().Symbol);
        } else
        {
            var level = SymbolTable.CurrentLevel;
            if (level!= 0 && SymbolTable.SearchPreviousLevel(level-1, functionName) != null) {
                var symbol = SymbolTable.SearchPreviousLevel(level-1, functionName);
                if (symbol.Type == SymbolType.Function) {
                    var methodIdent = symbol as SymbolTable.MethodIdent;
                    if (methodIdent != null) {
                        int paramsCount = methodIdent.Params.Count;
                        int newParamsCount = 0;
                        foreach (var param in context.argList().IDENTIFIER())
                        {
                            newParamsCount++;
                        }
                        if (paramsCount == newParamsCount) {
                            reportError($"The function '{functionName}' is being redefined with the same {paramsCount} params.", context.IDENTIFIER().Symbol);
                            return null;
                        }
                    }
                }
            }
           List<string> paramList = new List<string>();
           if (context.argList() != null) {
               foreach (var param in context.argList().IDENTIFIER()){
                       paramList.Add(param.GetText());
               }
           }
           SymbolTable.InsertMethod(context.IDENTIFIER().Symbol, SymbolType.Function, paramList);
           SymbolTable.OpenScope();
           if (context.argList() != null) {
               foreach (var param in context.argList().IDENTIFIER()) {
                   SymbolTable.InsertVar(param.Symbol, SymbolType.Parameter);
               }
           }
           Visit(context.COLON());
           Visit(context.NEWLINE());
           Visit(context.sequence());
           SymbolTable.PrintTable();
           SymbolTable.CloseScope();
        }
        return null;
    }

    public override object VisitArgList(MPParser.ArgListContext context)
    {
        return base.VisitArgList(context);
    }

    public override object VisitIfStatement(MPParser.IfStatementContext context)
    {
        Visit(context.expression());
        Visit(context.COLON(0));
        SymbolTable.OpenScope();
        Visit(context.sequence(0)); 
        SymbolTable.PrintTable();
        SymbolTable.CloseScope();
        Visit(context.ELSE());
        Visit(context.COLON(1));
        SymbolTable.OpenScope();
        Visit(context.sequence(1)); 
        SymbolTable.PrintTable();
        SymbolTable.CloseScope();
        return null;
    }

    public override object VisitWhileStatement(MPParser.WhileStatementContext context)
    {
        Visit(context.expression());
        Visit(context.COLON());
        SymbolTable.OpenScope();
        Visit(context.sequence());
        SymbolTable.PrintTable();
        SymbolTable.CloseScope();
        return null;
    }

    public override object VisitForStatement(MPParser.ForStatementContext context)
    {
        Visit(context.expression());
        Visit(context.expressionList()); 
        Visit(context.COLON());
        SymbolTable.OpenScope();
        Visit(context.sequence()); 
        SymbolTable.PrintTable();
        SymbolTable.CloseScope();
        return null;
    }

    public override object VisitReturnStatement(MPParser.ReturnStatementContext context)
    {
        var expressionText = context.expression().GetText();
        if (!string.IsNullOrEmpty(expressionText) && !expressionText.Contains("("))
        {
            Visit(context.expression());
        }
        return null;
    }

    public override object VisitPrintStatement(MPParser.PrintStatementContext context)
    {
        var expressionText = context.expression().GetText();
        if (!string.IsNullOrEmpty(expressionText)) 
        {
            Visit(context.expression());
        }
        return null;
    }

    public override object VisitAssignStatement(MPParser.AssignStatementContext context)
    {
        string varName = context.IDENTIFIER().GetText();
        Visit(context.IDENTIFIER());
        if (SymbolTable.SearchCurrentLevel(varName) != null) {
            reportError($"The variable '{varName}' is already defined in this scope.", context.IDENTIFIER().Symbol);
        } else {
            Visit(context.ASSIGN());
            int initialErrorCount = ErrorList.Count;
            Visit(context.expression());
            if (ErrorList.Count > initialErrorCount){
                return null;
            }
            SymbolTable.InsertVar(context.IDENTIFIER().Symbol, SymbolType.Variable);
            Visit(context.NEWLINE());
        }
        return null;
    }

    public override object VisitFunctionCallStatement(MPParser.FunctionCallStatementContext context)
    {
        var functionName = context.IDENTIFIER().GetText();
        var functionSymbol = SymbolTable.Search(functionName);
        if (functionSymbol == null || functionSymbol.Type != SymbolType.Function)
        {
            reportError($"The function '{functionName}' is not defined.", context.IDENTIFIER().Symbol);
        }
        else
        {
            var methodSymbol = functionSymbol as SymbolTable.MethodIdent;
            int numArguments = context.expressionList()?.expression().Length ?? 0;
            int numParameters = methodSymbol.Params.Count;

            if (numArguments != numParameters)
            {
                reportError($"The function '{functionName}' has {numParameters} arguments, but received {numArguments}.", context.IDENTIFIER().Symbol);
            }
        } 
        return null;
    }

    public override object VisitSequence(MPParser.SequenceContext context)
    {
        return base.VisitSequence(context);
    }

    public override object VisitExpression(MPParser.ExpressionContext context)
    {
        return base.VisitExpression(context);
    }

    public override object VisitComparison(MPParser.ComparisonContext context)
    {
        return base.VisitComparison(context);
    }

    public override object VisitAdditionExpression(MPParser.AdditionExpressionContext context)
    {
        return base.VisitAdditionExpression(context);
    }

    public override object VisitMultiplicationExpression(MPParser.MultiplicationExpressionContext context)
    {
        return base.VisitMultiplicationExpression(context);
    }

    public override object VisitElementExpression(MPParser.ElementExpressionContext context)
    {
        return base.VisitElementExpression(context);
    }

    public override object VisitExpressionList(MPParser.ExpressionListContext context)
    {
        return base.VisitExpressionList(context);
    }

    public override object VisitGroupPEAST(MPParser.GroupPEASTContext context)
    {
        return base.VisitGroupPEAST(context);
    }

    public override object VisitLenPEAST(MPParser.LenPEASTContext context)
    {
        return base.VisitLenPEAST(context);
    }

    public override object VisitListPEAST(MPParser.ListPEASTContext context)
    {
        return base.VisitListPEAST(context);
    }

    public override object VisitLiteralPEAST(MPParser.LiteralPEASTContext context)
    {
        return base.VisitLiteralPEAST(context);
    }

    public override object VisitIdentifierPEAST(MPParser.IdentifierPEASTContext context)
    {
        return base.VisitIdentifierPEAST(context);
    }

    public override object VisitListExpression(MPParser.ListExpressionContext context)
    {
        return base.VisitListExpression(context);
    }
}