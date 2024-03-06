using csharplab.Abstractions;

namespace csharplab.Services
{
    internal class SortService : ISortService
    {
        public void QuickSort(List<int> arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = arr[right];
                int i = left - 1;

                for (int j = left; j < right; j++)
                {
                    if (arr[j] <= pivot)
                    {
                        i++;
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }

                int temp2 = arr[i + 1];
                arr[i + 1] = arr[right];
                arr[right] = temp2;

                int pivotIndex = i + 1;

                QuickSort(arr, left, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, right);
            }
        }
    }
}
