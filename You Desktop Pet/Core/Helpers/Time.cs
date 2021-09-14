using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Helpers
{
    static class Time
    {
        public static float ElapsedTime { get; private set; }
        public static float DeltaTime {  get; private set; }

        private static Stopwatch _watch;
        private static float _prevElapsed = 0;

        public static void Start()
        {
            _watch = new Stopwatch();
            _watch.Start();

            _prevElapsed = ElapsedTime;
            ElapsedTime = (float)_watch.Elapsed.TotalSeconds;
            DeltaTime = ElapsedTime - _prevElapsed;
        }

        public static void Update()
        {
            _prevElapsed = ElapsedTime;
            ElapsedTime = (float)_watch.Elapsed.TotalSeconds;
            DeltaTime = ElapsedTime - _prevElapsed;
        }
    }
}
