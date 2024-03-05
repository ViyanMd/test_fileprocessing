using csharplab.Application;
using csharplab.Services;

namespace csharplab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "10m.txt";

            FileProcess fileProcess = FileProcess.Create(path, new TimerService(), new SortService());

            fileProcess.Process();
            fileProcess.ShowResults();
            
        }
    }
}