using SortKata.Domain.SortStrategies;

namespace SortKataTest.Domain.Tests.SortStrategies {
    public class BubbleSortStrategyTest {
        private BubbleSortStrategy<int> _bubbleSortStrategy;

        public BubbleSortStrategyTest() {
            this._bubbleSortStrategy = new BubbleSortStrategy<int>();
        }

        #region Sort
        [Fact]
        public void Sort_InputIsEmpty_ReturnsEmptyList() {
            IEnumerable<int> input = Enumerable.Empty<int>();

            IEnumerable<int> output = this._bubbleSortStrategy.Sort(input);

            Assert.Empty(output);
        }

        [Fact]
        public void Sort_OneElement_ReturnSingleElementList() {
            IEnumerable<int> input = new List<int> { 5 };

            IEnumerable<int> output = this._bubbleSortStrategy.Sort(input);

            Assert.Single(output);
            Assert.Equal(5, output.First());
        }

        [Fact]
        public void Sort_ShouldSortArray_WhenInputIsUnsorted() {
            IEnumerable<int> input = new List<int> { 1, 3, 4, 5, 2 };

            IEnumerable<int> output = this._bubbleSortStrategy.Sort(input);

            IEnumerable<int> expected = new List<int> { 1, 2, 3, 4, 5 };
            Assert.Equal(expected, output);
        }

        [Fact]
        public void Sort_ShouldReturnAlreadySortedArray_WhenInputIsSorted() {
            IEnumerable<int> input = new List<int> { 1, 2, 3, 4, 5 };

            IEnumerable<int> output = this._bubbleSortStrategy.Sort(input);

            Assert.Equal(input, output);
        }

        [Fact]
        public void Sort_ShouldSortArray_WhenInputIsInDescendingOrder() {
            IEnumerable<int> input = new List<int> { 5, 4, 3, 2, 1 };

            IEnumerable<int> output = this._bubbleSortStrategy.Sort(input);

            IEnumerable<int> expected = new List<int> { 1, 2, 3, 4, 5 };
            Assert.Equal(expected, output);
        }
        #endregion Sort
    }
}
