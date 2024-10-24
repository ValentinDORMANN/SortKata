using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public abstract class MergeBaseSortStrategy<T> : ISortStrategy<T> where T : IComparable<T> {
        public PerformanceMetadata PerformanceMetadata => new PerformanceMetadata("\u03A9(n log n)", "O(n log n)", "\u0398(n^2)");

        public IEnumerable<T> Sort(IEnumerable<T> list) {
            var array = list.ToArray();
            this.MergeSort(array);
            return array;
        }
        protected abstract void MergeSort(T[] list);

        protected void Merge(T[] list, int leftIdx, int midIdx, int rightIdx) {
            int leftSize = midIdx - leftIdx + 1;
            int rightSize = rightIdx - midIdx;

            T[] leftArray = new T[leftSize];
            T[] rightArray = new T[rightSize];

            Array.Copy(list, leftIdx, leftArray, 0, leftSize);
            Array.Copy(list, midIdx + 1, rightArray, 0, rightSize);

            int i = 0;
            int j = 0;
            int k = leftIdx;
            while (i < leftSize && j < rightSize) {
                if (leftArray[i].CompareTo(rightArray[j]) <= 0) {
                    list[k] = leftArray[i];
                    i++;
                } else {
                    list[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < leftSize) {
                list[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < rightSize) {
                list[k] = rightArray[j];
                j++;
                k++;
            }
        }
    }
}
