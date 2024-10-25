using SortKata.Domain.Models;
using SortKata.Domain.SortStrategies;

namespace SortKata.Tests.SortStrategies {
    public class SortStrategyFactoryTests {
        private readonly SortStrategyFactory<int> _sortStategyfactory;

        public SortStrategyFactoryTests() {
            this._sortStategyfactory = new SortStrategyFactory<int>();
        }

        [Fact]
        public void GetSortStrategy_BuubleSort_ReturnsBubbleSortStrategy() {
            ISortStrategy<int> result = this._sortStategyfactory.GetSortStrategy(ESortType.BUBBLE_SORT);
            
            Assert.IsType<BubbleSortStrategy<int>>(result);
        }

        [Fact]
        public void GetSortStrategy_CoctailSort_ReturnsCocktailSortStrategy() {
            ISortStrategy<int> result = this._sortStategyfactory.GetSortStrategy(ESortType.COCKTAIL_SORT);
            
            Assert.IsType<CocktailSortStrategy<int>>(result);
        }

        [Fact]
        public void GetSortStrategy_InsertionSort_ReturnsInsertionSortStrategy() {
            ISortStrategy<int> result = this._sortStategyfactory.GetSortStrategy(ESortType.INSERTION_SORT);
            
            Assert.IsType<InsertionSortStrategy<int>>(result);
        }

        [Fact]
        public void GetSortStrategy_MergeBottomUpSort_ReturnsMergeBottomUpSortStrategy() {
            ISortStrategy<int> result = this._sortStategyfactory.GetSortStrategy(ESortType.MERGE_BOTTOM_UP_SORT);
            
            Assert.IsType<MergeBottomUpSortStrategy<int>>(result);
        }

        [Fact]
        public void GetSortStrategy_MergeTopDownSort_ReturnsMergeTopDownSortStrategy() {
            ISortStrategy<int> result = this._sortStategyfactory.GetSortStrategy(ESortType.MERGE_TOP_DOWN_SORT);
            
            Assert.IsType<MergeTopDownSortStrategy<int>>(result);
        }

        [Fact]
        public void GetSortStrategy_QuickSort_ReturnsQuickSortStrategy() {
            ISortStrategy<int> result = this._sortStategyfactory.GetSortStrategy(ESortType.QUICK_SORT);
            
            Assert.IsType<QuickSortStrategy<int>>(result);
        }

        [Fact]
        public void GetSortStrategy_InvalidSort_ThrowsArgumentException() {
            Exception result = Assert.Throws<ArgumentException>(() => this._sortStategyfactory.GetSortStrategy((ESortType) 999));
            
            Assert.Equal("Invalid sort type", result.Message);
        }
    }
}
