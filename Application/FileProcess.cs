using csharplab.Abstractions;

namespace csharplab.Application
{
    internal class FileProcess
    {
        private string _filePath = string.Empty;

        private ITimerService _timer;
        private ISortService _sortService;

        private List<int> _nums;
        private List<int> _longestAscending;
        private List<int> _longestDescending;
        private int _max;
        private int _min;
        private int _arythMean;
        private int _median;
        private int _lineCount;
        private TimeSpan _elapsed;


        private FileProcess(string filePath, ITimerService timer, ISortService sortService)
        {
            _filePath = filePath;

            _timer = timer;
            _sortService = sortService;

            _nums = new List<int>();
            _longestAscending = new List<int>();
            _longestDescending = new List<int>();
            _max = 0;
            _min = 0;
            _arythMean = 0;
            _median = 0;
            _lineCount = 0;
        }

        public static FileProcess Create(string filePath, ITimerService timer, ISortService sortService)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path is empty");
            } else if(!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exist");
            }

            if(timer == null)
            {
                throw new ArgumentException("Timer is null");
            }

            if(sortService == null)
            {
                throw new ArgumentException("SortService is null");
            }

            return new FileProcess(filePath, timer, sortService);
        }

        public void Process()
        {
            Console.WriteLine("Processing file: " + _filePath);

            _timer.Start();
            using (StreamReader sr = new StreamReader(_filePath))
            {
                int current;
                string? currentLine;

                while ((currentLine = sr.ReadLine()) != null)
                {
                    current = int.Parse(currentLine);

                    _nums.Add(current);
                    
                    SetMax(current);
                    SetMin(current);

                    _arythMean += current;

                    _lineCount++;
                }
            }

            CalculateArythMean();

            FindSequences();

            _sortService.QuickSort(_nums, 0, _nums.Count - 1);
            CalculateMedian();

            _elapsed = _timer.Stop();
        }

        private void SetMax(int num)
        {
            if(num > _max)
                _max = num;
        }

        private void SetMin(int num)
        {
            if(num < _min)
                _min = num;
        }

        private void CalculateArythMean()
        {
            _arythMean /= _lineCount;
        }

        private void CalculateMedian()
        {
            if (_nums.Count % 2 == 0)
                _median = (_nums[_nums.Count / 2] + _nums[_nums.Count / 2 - 1]) / 2;
            else
                _median = _nums[_nums.Count / 2];
        }

        private void FindSequences()
        {
            List<int> currentSequenceAsc = new List<int> { _nums[0] };
            List<int> currentSequenceDesc = new List<int> { _nums[0] };

            for (int i = 1; i < _nums.Count; i++)
            {
                if (_nums[i] > _nums[i - 1])
                {
                    currentSequenceAsc.Add(_nums[i]);
                }
                else
                {
                    if (currentSequenceAsc.Count > _longestAscending.Count)
                        _longestAscending = new List<int>(currentSequenceAsc);

                    currentSequenceAsc.Clear();
                    currentSequenceAsc.Add(_nums[i]);
                }

                if (_nums[i] < _nums[i - 1])
                {
                    currentSequenceDesc.Add(_nums[i]);
                }
                else
                {
                    if (currentSequenceDesc.Count > _longestDescending.Count)
                        _longestDescending = new List<int>(currentSequenceDesc);

                    currentSequenceDesc.Clear();
                    currentSequenceDesc.Add(_nums[i]);
                }
            }

            if (currentSequenceAsc.Count > _longestAscending.Count)
                _longestAscending = new List<int>(currentSequenceAsc);

            if (currentSequenceDesc.Count > _longestDescending.Count)
                _longestDescending = new List<int>(currentSequenceDesc);
        }

        public void ShowResults()
        {
            Console.WriteLine("Processed " + _lineCount + " lines");
            Console.WriteLine("\nMax Value: " + _max + "\nMin Value: " + _min);
            Console.WriteLine("\nArythmetic Mean: " + _arythMean);
            Console.WriteLine("\nMedian: " + _median);
            Console.WriteLine($"\nLongest Ascending Sequence ({_longestAscending.Count}): ");
            Console.WriteLine($"\nLongest Descending Sequence ({_longestDescending.Count}): ");
            Console.WriteLine("\nElapsed time: " + _elapsed);
        }
    }
}
