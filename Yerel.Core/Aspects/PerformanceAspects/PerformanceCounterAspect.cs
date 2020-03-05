﻿using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;

namespace Yerel.Core.Aspects.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect : OnMethodBoundaryAspect
    {
        [NonSerialized] private Stopwatch _stopwatch;

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>();

            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopwatch.Start();

            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _stopwatch.Stop();

            Debug.WriteLine("Performans : {0}.{1} ->> {2}", args.Method.DeclaringType.FullName, args.Method.Name, _stopwatch.Elapsed.TotalSeconds);
            
            _stopwatch.Reset();
            
            base.OnExit(args);
        }
    } 
}
