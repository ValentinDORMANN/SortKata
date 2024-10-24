using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public class BubbleSortStrategy<T> : ISortStrategy<T> where T : IComparable<T> {
        public PerformanceMetadata PerformanceMetadata => new PerformanceMetadata("O(n)", "O(n^2)", "O(n^2)");

        public IEnumerable<T> Sort(IEnumerable<T> list) {
            var array = list.ToList();
            for (int i = 0; i < array.Count - 1; i++) {
                for (int j = 0; j < array.Count - i - 1; j++) {
                    if (array[j].CompareTo(array[j + 1]) > 0) {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            return array;
        }

    }
}
