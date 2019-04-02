using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NullableGuidVsConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }

    [ClrJob, MonoJob, CoreJob] // 可以針對不同的 CLR 進行測試
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        public TestRunner()
        {
        }

        [Benchmark]
        public void Convert() => _test.Convert();

        [Benchmark]
        public void Value() => _test.Value();
    }

    public class TestClass
    {
        private readonly Guid? _target = Guid.Empty;

        public void Convert()
        {
            var result = (Guid)_target;
        }

        public void Value()
        {
            var result = _target.Value;
        }
    }
}
