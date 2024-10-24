using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public class InsertionSortStrategy<T> : ISortStrategy<T> where T : IComparable<T> {
        public PerformanceMetadata PerformanceMetadata => new PerformanceMetadata("O(n)", "O(n^2)", "O(n^2)");

        public IEnumerable<T> Sort(IEnumerable<T> list) {
            var array = list.ToArray();
            for (int i = 1; i < array.Length; i++) {
                T key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j].CompareTo(key) > 0) {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
            return array;
        }
    }
}
