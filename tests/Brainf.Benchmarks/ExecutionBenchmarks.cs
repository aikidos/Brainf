using BenchmarkDotNet.Attributes;

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

        private readonly IBrainfParser _parser = new BrainfParser();
        private readonly IBrainfCompiler _compiler = new BrainfCompiler();
        private readonly IBrainfStream _emptyStream = new EmptyStream();

        [Benchmark]
        public IBrainfProgram Brainf()
        {
            var program = _parser.Parse(SourceCode);
            var func = _compiler.Compile<BrainfMemory>(program);
            
            func(_emptyStream);

            return program;
        }
    }
}
