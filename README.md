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
var func = compiler.Compile<BrainfMemory, ConsoleBrainfStream>(program);

var memory = new BrainfMemory();
var stream = KnownBrainfStreams.Console;

func(memory, stream);
```

*Output:*
> ZYXWVUTSRQPONMLKJIHGFEDCBA

Performance
---

Code execution the [example](#example) above:

| Method |     Mean |   Error |  StdDev | Allocated |
|------- |---------:|--------:|--------:|----------:|
| Brainf | 591.2 ms | 1.26 ms | 1.18 ms |  20.88 KB |
