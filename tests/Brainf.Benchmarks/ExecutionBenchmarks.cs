using BenchmarkDotNet.Attributes;
using Brainf.Compiler;
using Brainf.Memory;
using Brainf.Parser;
using Brainf.Program;
using Brainf.Streams;

namespace Brainf.Benchmarks;

[MemoryDiagnoser]
public class ExecutionBenchmarks
{
    private const string SourceCode = @"
>++[<+++++++++++++>-]<[[>+>+<<-]>[<+>-]++++++++
[>++++++++<-]>.[-]<<>++++++++++[>++++++++++[>++
++++++++[>++++++++++[>++++++++++[>++++++++++[>+
+++++++++[-]<-]<-]<-]<-]<-]<-]<-]++++++++++.";

    private readonly BrainfParser _parser = new();
    private readonly BrainfCompiler _compiler = new();
    private BrainfMemory _memory;

    [IterationSetup]
    public void IterationSetup()
    {
        _memory = new BrainfMemory(64);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        _memory = null;
    }

    [Benchmark]
    public IBrainfProgram Brainf()
    {
        var program = _parser.Parse(SourceCode);
        var func = _compiler.Compile<BrainfMemory, EmptyBrainfStream>(program);

        func(_memory, BrainfStreams.Empty);

        return program;
    }
}