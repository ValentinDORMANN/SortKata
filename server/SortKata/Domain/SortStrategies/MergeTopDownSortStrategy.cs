namespace SortKata.Domain.SortStrategies {
    public class MergeTopDownSortStrategy<T> : MergeBaseSortStrategy<T> where T : IComparable<T> {
        protected override void MergeSort(T[] array) {
            this.MergeSortRecursive(array, 0, array.Length - 1);
        }

        private void MergeSortRecursive(T[] array, int leftIdx, int rightIdx) {
            if (leftIdx < rightIdx) {
                int midIdx = (leftIdx + rightIdx) / 2;
                this.MergeSortRecursive(array, leftIdx, midIdx);
                this.MergeSortRecursive(array, midIdx + 1, rightIdx);
                this.Merge(array, leftIdx, midIdx, rightIdx);
            }
        }
    }
}
