![Actions Status](https://github.com/aikidos/Brainf/workflows/build/badge.svg)

Brainf
===

Library for executing code written in the [**Brainf\*ck**](https://en.wikipedia.org/wiki/Brainfuck) programming language.

Examples
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
var func = compiler.Compile(program);

func(memory, BrainfIO.Console);
```

*Output:*
> ZYXWVUTSRQPONMLKJIHGFEDCBA

### [Brainf\*ck interpreter 👀](http://www.hevanet.com/cristofd/brainfuck/dbfi.b)


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
