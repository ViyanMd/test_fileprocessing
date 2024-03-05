namespace csharplab.Abstractions
{
    internal interface ITimerService
    {
        void Start();
        TimeSpan Stop();
    }
}