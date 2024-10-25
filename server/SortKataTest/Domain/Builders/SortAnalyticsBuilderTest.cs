using SortKata.Domain.Builders;
using SortKata.Domain.Models;

namespace SortKata.Domain.Tests.Builders {
    public class SortAnalyticsBuilderTests {
        private SortAnalyticsBuilder _sortAnalyticBuilder;

        public SortAnalyticsBuilderTests() {
            this._sortAnalyticBuilder = new SortAnalyticsBuilder();
        }

        [Fact]
        public void New_ReturnBuilder() {
            SortAnalyticsBuilder result = this._sortAnalyticBuilder.New();

            Assert.NotNull(result);
            Assert.Equal(this._sortAnalyticBuilder, result);
        }

        private void AssertDefaultSortAnalytics(SortAnalytics sortAnalytics) {
            Assert.NotNull(sortAnalytics);
            Assert.Equal(ESortType.UNDEFINED, sortAnalytics.SortType);
            Assert.Equal(0, sortAnalytics.ItemCount);
            Assert.Equal(TimeSpan.Zero, sortAnalytics.ExecutionTime);
            Assert.Equal(string.Empty, sortAnalytics.BestTimeComplexity);
            Assert.Equal(string.Empty, sortAnalytics.WorstTimeComplexity);
            Assert.Equal(string.Empty, sortAnalytics.AverageTimeComplexity);
        }

        [Fact]
        public void Finalize_NewIsNotCalledBefore_ReturnDefaultSortAnalytics() {
            SortAnalytics result = this._sortAnalyticBuilder.Finalize();

            this.AssertDefaultSortAnalytics(result);
        }

        [Fact]
        public void Finalize_NewCalledBefore_ReturnsSortAnalytics() {
            SortAnalytics result = this._sortAnalyticBuilder.New().Finalize();

            this.AssertDefaultSortAnalytics(result);
        }

        [Fact]
        public void BuildSortType_ReturnsBuilder() {
            const ESortType SORT_TYPE = ESortType.QUICK_SORT;

            SortAnalyticsBuilder result = this._sortAnalyticBuilder.New().BuildSortType(SORT_TYPE);

            Assert.NotNull(result);
            Assert.Equal(this._sortAnalyticBuilder, result);
            Assert.Equal(SORT_TYPE, this._sortAnalyticBuilder.Finalize().SortType);
        }

        [Fact]
        public void BuildPerformanceMetadata_ReturnsBuilder() {
            const string BEST = "O(1)";
            const string WORST = "O(n^2)";
            const string AVERAGE = "O(n)";
            PerformanceMetadata performanceMetadata = new PerformanceMetadata(BEST, WORST, AVERAGE);

            SortAnalyticsBuilder result = this._sortAnalyticBuilder.New().BuildPerformanceMetadata(performanceMetadata);

            Assert.NotNull(result);
            Assert.Equal(this._sortAnalyticBuilder, result);
            SortAnalytics sortAnalytics = this._sortAnalyticBuilder.Finalize();
            Assert.Equal(BEST, sortAnalytics.BestTimeComplexity);
            Assert.Equal(WORST, sortAnalytics.WorstTimeComplexity);
            Assert.Equal(AVERAGE, sortAnalytics.AverageTimeComplexity);
        }

        [Fact]
        public void BuildExecution_ReturnsBuilder() {
            const int ITEM_COUNT = 10;
            TimeSpan EXECUTION_TIME = new TimeSpan(0, 10, 20);            
            
            SortAnalyticsBuilder result = this._sortAnalyticBuilder.New().BuildExecution(ITEM_COUNT, EXECUTION_TIME);

            Assert.NotNull(result);
            Assert.Equal(this._sortAnalyticBuilder, result);
            SortAnalytics sortAnalytics = this._sortAnalyticBuilder.Finalize();
            Assert.Equal(ITEM_COUNT, sortAnalytics.ItemCount);
            Assert.Equal(EXECUTION_TIME, sortAnalytics.ExecutionTime);
        }
    }
}
