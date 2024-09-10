parser grammar MPParser;

options {
    tokenVocab = MPLexer;
}

program :  mainStatement* ;

mainStatement : defStatement | assignStatement ;
statement : defStatement 
           | ifStatement 
           | returnStatement 
           | printStatement 
           | whileStatement 
           | assignStatement 
           | functionCallStatement ;

defStatement : DEF ID LP argList RP COLON sequence ;

argList : (ID  (COMMA ID)*)?;

ifStatement : IF expression COLON sequence ELSE COLON sequence;

whileStatement : WHILE expression COLON sequence ;

forStatement : FOR expression IN expressionList COLON sequence ;

returnStatement : RETURN expression NEWLINE ;

printStatement : PRINT expression NEWLINE ;

assignStatement : ID ASSIGN expression NEWLINE ;

functionCallStatement : primitiveExpression LP expressionList RP NEWLINE ;

sequence : INDENT moreStatements DEDENT ;

moreStatements : statement+ ;

expression : additionExpression ( comparison )? ;

comparison : ( LT | GT | LE | GE | EQ ) additionExpression ;

additionExpression : multiplicationExpression (( ADD | MINUS ) multiplicationExpression )* ;

multiplicationExpression : elementExpression ((MUL | DIV ) elementExpression)* ;

elementExpression : primitiveExpression (LB expression RB)? ;

expressionList : (expression  (COMMA expression )*)? ;

primitiveExpression : (MINUS)? ( INTEGER 
                               | FLOAT 
                               | CHAR 
                               | STRING 
                               | ID (LP expressionList RP)? 
                               | LP expression RP 
                               | listExpression 
                               | LEN LP expression RP) ;

listExpression : LB expressionList RB ;