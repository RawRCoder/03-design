using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace PerfLogging
{
	class Program
	{
		static void Main(string[] args)
		{
		    var maxiter = 500000;
            var ml = new List<string>();
		    using (PerfLogger.Measure(t => Console.WriteLine($"ML: {t.TotalMilliseconds}")))
		    {
		        for (var i = 0; i < maxiter; ++i) ml.Add((i+1).ToString());
            }
		    var il = ImmutableList<string>.Empty;
            using (PerfLogger.Measure(t => Console.WriteLine($"IL: {t.TotalMilliseconds}")))
                for (var i = 0; i < maxiter; ++i) il.Add((i+3).ToString());
		    Console.ReadKey(true);
		}
	}

    internal class PerfLogger : IDisposable
    {
        public Stopwatch Watch { get; }

        public PerfLogger(Action<TimeSpan> finalAction)
        {
            FinalAction = finalAction;
            Watch = new Stopwatch();
            Watch.Start();
        }

        public Action<TimeSpan> FinalAction { get; }
        public void Dispose()
        {
            Watch.Stop();
            FinalAction(Watch.Elapsed);
        }

        public static IDisposable Measure(Action<TimeSpan> f)
        {
            return new PerfLogger(f);
        }
    }
}
