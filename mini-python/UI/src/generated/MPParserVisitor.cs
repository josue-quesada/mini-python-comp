//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/Josue/Documents/GitHub/mini-python-comp/mini-python/UI/src/MPParser.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace generated {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="MPParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IMPParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] MPParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.mainStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMainStatement([NotNull] MPParser.MainStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] MPParser.StatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.defStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDefStatement([NotNull] MPParser.DefStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.argList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgList([NotNull] MPParser.ArgListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] MPParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] MPParser.WhileStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] MPParser.ForStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.returnStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnStatement([NotNull] MPParser.ReturnStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.printStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrintStatement([NotNull] MPParser.PrintStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.assignStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignStatement([NotNull] MPParser.AssignStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.functionCallStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallStatement([NotNull] MPParser.FunctionCallStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.sequence"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSequence([NotNull] MPParser.SequenceContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] MPParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.comparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] MPParser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.additionExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdditionExpression([NotNull] MPParser.AdditionExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.multiplicationExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicationExpression([NotNull] MPParser.MultiplicationExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.elementExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElementExpression([NotNull] MPParser.ElementExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.expressionList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionList([NotNull] MPParser.ExpressionListContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>groupPEAST</c>
	/// labeled alternative in <see cref="MPParser.primitiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroupPEAST([NotNull] MPParser.GroupPEASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>lenPEAST</c>
	/// labeled alternative in <see cref="MPParser.primitiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLenPEAST([NotNull] MPParser.LenPEASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>listPEAST</c>
	/// labeled alternative in <see cref="MPParser.primitiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListPEAST([NotNull] MPParser.ListPEASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>literalPEAST</c>
	/// labeled alternative in <see cref="MPParser.primitiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteralPEAST([NotNull] MPParser.LiteralPEASTContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>identifierPEAST</c>
	/// labeled alternative in <see cref="MPParser.primitiveExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifierPEAST([NotNull] MPParser.IdentifierPEASTContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="MPParser.listExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListExpression([NotNull] MPParser.ListExpressionContext context);
}
} // namespace generated
