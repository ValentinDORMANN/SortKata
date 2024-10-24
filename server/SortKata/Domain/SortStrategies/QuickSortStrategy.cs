using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public class QuickSortStrategy<T> : ISortStrategy<T> where T : IComparable<T> {
        public PerformanceMetadata PerformanceMetadata => new PerformanceMetadata("O(n log n)", "O(n^2)", "O(n log n)");
        
        public IEnumerable<T> Sort(IEnumerable<T> list) {
            var array = list.ToList();
            this.QuickSort(array, 0,  array.Count - 1);
            return array;
        }

        private void QuickSort(IList<T> list, int lowIdx, int highIdx) {
            if (lowIdx < highIdx) {
                int partitionIdx = this.Partition(list, lowIdx, highIdx);
                this.QuickSort(list, lowIdx, partitionIdx - 1);
                this.QuickSort(list, partitionIdx + 1, highIdx);
            }
        }

        private int Partition(IList<T> list, int lowIdx, int highIdx) {
            T pivot = list[highIdx];
            int i = lowIdx - 1;

            for (int j = lowIdx; j < highIdx; j++) {
                if (list[j].CompareTo(pivot) < 0) {
                    i++;
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            T temp1 = list[i + 1];
            list[i + 1] = list[highIdx];
            list[highIdx] = temp1;

            return i + 1;
        }
    }
}
