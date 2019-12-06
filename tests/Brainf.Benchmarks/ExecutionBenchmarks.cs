using BenchmarkDotNet.Attributes;
using Brainf.Streams;

namespace Brainf.Benchmarks
{
    [MemoryDiagnoser]
    public class ExecutionBenchmarks
    {
        private const string SourceCode = @"
>++[<+++++++++++++>-]<[[>+>+<<-]>[<+>-]++++++++
[>++++++++<-]>.[-]<<>++++++++++[>++++++++++[>++
++++++++[>++++++++++[>++++++++++[>++++++++++[>+
+++++++++[-]<-]<-]<-]<-]<-]<-]<-]++++++++++.";

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
            var parser = BrainfParser.Default;
            var compiler = BrainfCompiler.Default;

            var program = parser.Parse(SourceCode);
            var func = compiler.Compile<BrainfMemory, EmptyBrainfStream>(program);

            func(_memory, KnownBrainfStreams.Empty);

            return program;
        }
    }
}
