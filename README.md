![Actions Status](https://github.com/aikidos/Brainf/workflows/build/badge.svg)

Brainf
===

Library for executing code written in the "Brainfuck" programming language.

Example
---

On platforms supporting netstandard 2.1+

```csharp
const string sourceCode = @"
>++[<+++++++++++++>-]<[[>+>+<<-]>[<+>-]++++++++
[>++++++++<-]>.[-]<<>++++++++++[>++++++++++[>++
++++++++[>++++++++++[>++++++++++[>++++++++++[>+
+++++++++[-]<-]<-]<-]<-]<-]<-]<-]";

var parser = BrainfParser.Default;
var compiler = BrainfCompiler.Default;

var program = parser.Parse(sourceCode);
var func = compiler.Compile(program);

func(KnownBrainfStreams.Console);
```

*Output:*
> ZYXWVUTSRQPONMLKJIHGFEDCBA
