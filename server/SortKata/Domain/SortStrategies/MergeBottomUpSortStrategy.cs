namespace SortKata.Domain.SortStrategies {
    public class MergeBottomUpSortStrategy<T> : MergeBaseSortStrategy<T> where T : IComparable<T> {
        protected override void MergeSort(T[] array) {
            int n = array.Length;
            for (int size = 1; size < n; size *= 2) {
                for (int leftStart = 0; leftStart < n - size; leftStart += 2 * size) {
                    int mid = leftStart + size - 1;
                    int rightEnd = Math.Min(leftStart + 2 * size - 1, n - 1);
                    this.Merge(array, leftStart, mid, rightEnd);
                }
            }
        }
    }
}
