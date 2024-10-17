parser grammar MPParser;

options {
    tokenVocab = MPLexer;
}
program :  mainStatement* EOF;

mainStatement: defStatement 
    | assignStatement;

statement:
    defStatement
  | ifStatement
  | returnStatement   
  | printStatement
  | whileStatement
  | forStatement
  | assignStatement
  | functionCallStatement;

defStatement : DEF IDENTIFIER LP argList RP COLON NEWLINE sequence;

argList : (IDENTIFIER  (COMMA IDENTIFIER)*)?;

ifStatement : IF expression COLON sequence ELSE COLON sequence;

whileStatement : WHILE expression COLON sequence;

forStatement : FOR expression IN expressionList COLON sequence;

returnStatement : RETURN expression NEWLINE;

printStatement : PRINT expression NEWLINE;

assignStatement : IDENTIFIER ASSIGN expression NEWLINE;

functionCallStatement : IDENTIFIER LP expressionList RP NEWLINE?;

sequence : INDENT statement+ DEDENT;

expression : additionExpression comparison?;

comparison : ( LT | GT | LE | GE | EQ ) additionExpression;

additionExpression : multiplicationExpression (( PLUS | MINUS ) multiplicationExpression )* ;

multiplicationExpression : elementExpression ((MUL | DIV ) elementExpression)*;

elementExpression : primitiveExpression (LB expression RB)?;

expressionList : (expression  (COMMA expression )*)?;

primitiveExpression :  LP expression RP                                         #groupPEAST
                   | LEN LP expression RP                                       #lenPEAST
                   | listExpression                                             #listPEAST
                   | ( PLUS | MINUS)? (INTEGER | FLOAT | CHAR | STRING)         #literalPEAST
                   | IDENTIFIER (LP expressionList RP)?                         #identifierPEAST;

listExpression : LB expressionList RB;