lexer grammar MPLexer;

tokens {INDENT, DEDENT}

@lexer::header{
    using UI.Imports.DenterHelper;
}

@lexer::members {
    private DenterHelper denter;
      
    public override IToken NextToken()
    {
        if (denter == null)
        {
            denter = DenterHelper.Builder()
                .Nl(NL)
                .Indent(MPParser.INDENT)
                .Dedent(MPParser.DEDENT)
                .PullToken(base.NextToken);
        }
    
        return denter.NextToken();
    }
}
DEF: 'def';
RETURN: 'return';
IF: 'if';
ELSE: 'else';
WHILE: 'while';
FOR: 'for';
IN: 'in';
PRINT: 'print';
LEN: 'len';

PLUS: '+';
MINUS: '-';
MUL: '*';
DIV: '/';
ASSIGN: '=';
LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '==';
LP: '(';
RP: ')';
LB: '[';
RB: ']';
COLON: ':';
COMMA: ',';

INTEGER: [0-9]+;
FLOAT: [0-9]+ '.' [0-9]+;
CHAR: '\'' . '\'';
STRING: '"' .*? '"';

ID: [a-zA-Z_] [a-zA-Z_0-9]*;

COMMENT: '#' ~[\r\n]* -> skip;
MULTILINE_COMMENT: '"""' .*? '"""' -> skip;

NL: ('\r'? '\n' [ \t]*) -> channel(HIDDEN);
WS: [ \t]+ -> skip;
