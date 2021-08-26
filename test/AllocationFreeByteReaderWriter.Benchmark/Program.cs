BenchmarkDotNet.Running.BenchmarkSwitcher
    .FromAssembly(typeof(Benchmarks).Assembly)
    .Run(args.Any() ? args : new[] { "-f", "**" });