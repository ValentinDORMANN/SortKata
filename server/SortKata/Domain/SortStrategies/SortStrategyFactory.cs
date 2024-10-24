using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public class SortStrategyFactory<T> where T : IComparable<T> {
        public ISortStrategy<T> GetSortStrategy(ESortType sortType) {
            return sortType switch { 
                ESortType.BUBBLE_SORT => new BubbleSortStrategy<T>(),
                ESortType.COCKTAIL_SORT => new CocktailSortStrategy<T>(),
                ESortType.INSERTION_SORT => new InsertionSortStrategy<T>(),
                ESortType.MERGE_BOTTOM_UP_SORT => new MergeBottomUpSortStrategy<T>(),
                ESortType.MERGE_TOP_DOWN_SORT => new MergeTopDownSortStrategy<T>(),
                ESortType.QUICK_SORT => new QuickSortStrategy<T>(),
                _ => throw new ArgumentException("Invalid sort type")
            };
        }
    }
}
