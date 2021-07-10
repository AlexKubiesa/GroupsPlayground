grammar PermutationGrammar;

/*
 * Parser Rules
 */

permutation
    : cycle (WS* cycle)* EOF
    ;

cycle
    : LPAREN (number (numberSeparator number)*)? RPAREN
    ;

number : NUMBER ;

numberSeparator
    : COMMA
    | COMMA? WS+
    ;

/*
 * Lexer Rules
 */

LPAREN : '(' ;

RPAREN : ')' ;

NUMBER : [1-9] [0-9]* ;

COMMA : ',' ;

WS : ' ' ;
