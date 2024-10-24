using SortKata.Domain.Models;
using SortKata.Presentation.Assemblers;
using SortKata.Presentation.Models;

namespace SortKataTest.Presentation.Assemblers {
    public class SortAnalyticsAssemblerTest {
        private static readonly ESortType _SORT_TYPE = ESortType.QUICK_SORT;
        private static readonly int _ITEM_COUNT = 10;
        private static readonly TimeSpan _EXECUTION_TIME = new TimeSpan(10, 20, 30);
        private static readonly string _BEST_TIME_COMPLEXITY = "O(1)";
        private static readonly string _WORST_TIME_COMPLEXITY = "O(n^2)";
        private static readonly string _AVERAGE_TIME_COMPLEXITY = "O(n)";

        private static readonly int _PRECISION = 8;

        private SortAnalyticsAssembler _sortAnalyticsAssembler;

        public SortAnalyticsAssemblerTest() {
            this._sortAnalyticsAssembler = new SortAnalyticsAssembler();
        }

        [Fact]
        public void ToDTO_ValidSortAnalytics_ReturnsValidSortAnalyticsDto() {
            SortAnalytics sortAnalytics = new SortAnalytics() {
                SortType = _SORT_TYPE,
                ItemCount = _ITEM_COUNT,
                ExecutionTime = _EXECUTION_TIME,
                BestTimeComplexity = _BEST_TIME_COMPLEXITY,
                WorstTimeComplexity = _WORST_TIME_COMPLEXITY,
                AverageTimeComplexity = _AVERAGE_TIME_COMPLEXITY
            };

            SortAnalyticsDto result = this._sortAnalyticsAssembler.ToDto(sortAnalytics);

            Assert.NotNull(result);
            Assert.Equal(_SORT_TYPE.ToString(), result.SortType);
            Assert.Equal(_ITEM_COUNT, result.ItemCount);
            Assert.Equal(_EXECUTION_TIME.TotalMilliseconds, result.ExecutionTime, _PRECISION);
            Assert.Equal(_BEST_TIME_COMPLEXITY, result.BestTimeComplexity);
            Assert.Equal(_WORST_TIME_COMPLEXITY, result.WorstTimeComplexity);
            Assert.Equal(_AVERAGE_TIME_COMPLEXITY, result.AverageTimeComplexity);
        }
    }
}
