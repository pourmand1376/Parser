# Parser
Implementation of LL(0), LR(0), SLR(1), CLR(1) algorithms in C#.

This is done for educational purposes only.

# Topics Covered
- First and follow terminals are calculated, even when grammer is left recursive.
- Stack changes are shown in a table.
- For LR algorithms state diagrams are shown in a graph (Microsoft Library `MSAGL`)
- Syntax Parse tree is shown (Microsoft Library `MSAGL`)

# ToDo
- Add Support for LALR(1) algorithms.
- Add Support to input grammar with | symbol.

# How to build parse tree 
Use the input from `SampleGrammars` Folder which is available in Project source
Grammars are written in `BNF` format as show below.

JSON Grammar:

    <JSON> ::= <array>
    <JSON> ::= <object>

    <array> ::= "[" <arrayinside> "]" 

    <arrayinside> ::= <value>
    <arrayinside> ::= <value> "," <arrayinside>
    <arrayinside> ::= ""

    <object> ::= "{" <objectinside> "}"

    <objectinside> ::= <pair>
    <objectinside> ::= <pair> "," <objectinside>
    <objectinside> :: ""

    <pair> ::= <string> ":" <value>

    <value> ::= <string>
    <value> ::= <number>
    <value> ::= <object>
    <value> ::= <array>
    <value> ::= "True"
    <value> ::= "False"
    <value> ::= "null"

    <string> ::= "a"
    <string> ::= "b"
    <string> ::= "c"
    <string> ::= "d"

    <number> ::= "1"
    <number> ::= "2"
    <number> ::= "3"
    <number> ::= "4"
    
   JSON test:
   
        { a : 1 , b : [ 1 , 3 , a , { a : 2 , c : 4 , } , ] , c : False , b : True , }
