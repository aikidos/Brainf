![Actions Status](https://github.com/aikidos/Brainf/workflows/build/badge.svg)

Brainf
===

Library for executing code written in the [**Brainfuck**](https://en.wikipedia.org/wiki/Brainfuck) programming language.

Example
---

On platforms supporting netstandard 2.1+

```csharp
const string sourceCode = @"
>++[<+++++++++++++>-]<[[>+>+<<-]>[<+>-]++++++++
[>++++++++<-]>.[-]<<>++++++++++[>++++++++++[>++
++++++++[>++++++++++[>++++++++++[>++++++++++[>+
+++++++++[-]<-]<-]<-]<-]<-]<-]<-]";

var parser = new BrainfParser();
var compiler = new BrainfCompiler();
var memory = new BrainfMemory();

var program = parser.Parse(sourceCode);
var func = compiler.Compile<BrainfMemory, ConsoleBrainfStream>(program);

func(memory, BrainfStreams.Console);
```

*Output:*
> ZYXWVUTSRQPONMLKJIHGFEDCBA

Performance
---

Code execution the [example](#example) above:

| Method |     Mean |   Error |  StdDev | Allocated |
|------- |---------:|--------:|--------:|----------:|
| Brainf | 591.2 ms | 1.26 ms | 1.18 ms |  20.88 KB |

What is madness?
---

[A brainfuck interpreter](http://www.hevanet.com/cristofd/brainfuck/dbfi.b) is madness:

```csharp
const string sourceCode = @"
>>>+[[-]>>[-]++>+>+++++++[<++++>>++<-]++>>+>+>+++++[>++>++++++<<-]+>>>,<++[[>[
->>]<[>>]<<-]<[<]<+>>[>]>[<+>-[[<+>-]>]<[[[-]<]++<-[<+++++++++>[<->-]>>]>>]]<<
]<]<[[<]>[[>]>>[>>]+[<<]<[<]<+>>-]>[>]+[->>]<<<<[[<<]<[<]+<<[+>+<<-[>-->+<<-[>
+<[>>+<<-]]]>[<+>-]<]++>>-->[>]>>[>>]]<<[>>+<[[<]<]>[[<<]<[<]+[-<+>>-[<<+>++>-
[<->[<<+>>-]]]<[>+<-]>]>[>]>]>[>>]>>]<<[>>+>>+>>]<<[->>>>>>>>]<<[>.>>>>>>>]<<[
>->>>>>]<<[>,>>>]<<[>+>]<<[+<<]<]";

...

Console.WriteLine(@"Input a `Brainfuck` program and its input, separated by an exclamation point:");

func(memory, stream);
```

*Console:*
```
- Input a `Brainfuck` program and its input, separated by an exclamation point:
- >++[<+++++++++++++>-]<[[>+>+<<-]>[<+>-]++++++++
  [>++++++++<-]>.[-]<<>++++++++++[>++++++++++[>++
  ++++++++[>++++++++++[>++++++++++[>++++++++++[>+
  +++++++++[-]<-]<-]<-]<-]<-]<-]<-]++++++++++.
- !
- ZYXWVUTSRQPONMLKJIHGFEDCBA
```

LICENSE
===
[MIT](LICENSE)
