using BenchmarkDotNet.Running;

namespace Brainf.Benchmarks;

class Program
{
    static void Main()
    {
        BenchmarkRunner.Run<ExecutionBenchmarks>();
    }
}