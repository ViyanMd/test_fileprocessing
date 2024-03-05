using csharplab.Application;
using csharplab.Services;

namespace csharplab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileProcess fileProcess = FileProcess.Create(new TimerService(), new SortService());

            fileProcess.Process();
            fileProcess.ShowResults();
            
        }
    }
}