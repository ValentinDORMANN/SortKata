using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public class CocktailSortStrategy<T> : ISortStrategy<T> where T : IComparable<T> {
        public PerformanceMetadata PerformanceMetadata => new PerformanceMetadata("O(n)", "O(n^2)", "O(n^2)");

        public IEnumerable<T> Sort(IEnumerable<T> list) {
            var array = list.ToArray();
            this.CocktailSortAlgorithm(array);
            return array;
        }

        private void CocktailSortAlgorithm(T[] array) {
            bool swapped = true;
            int start = 0;
            int end = array.Length - 1;

            while (swapped) {
                swapped = false;

                // left to right
                for (int i = start; i < end; i++) {
                    if (array[i].CompareTo(array[i + 1]) > 0) {
                        this.Swap(array, i, i + 1);
                        swapped = true;
                    }
                }

                if (!swapped) { break; }

                swapped = false;
                end--;

                // right to left
                for (int i = end - 1; i >= start; i--) {
                    if (array[i].CompareTo(array[i + 1]) > 0) {
                        this.Swap(array, i, i + 1);
                        swapped = true;
                    }
                }

                // Réduction de la plage de tri à gauche
                start++;
            }
        }

        private void Swap(T[] array, int i, int j) {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
