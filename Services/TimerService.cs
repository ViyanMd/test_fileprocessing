using System.Diagnostics;
using csharplab.Abstractions;

namespace csharplab.Services
{
    internal class TimerService : ITimerService
    {
        private Stopwatch _stopwatch;

        public TimerService()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public TimeSpan Stop()
        {
            _stopwatch.Stop();
            return _stopwatch.Elapsed;
        }
    }
}