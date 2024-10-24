using SortKata.Domain.Models;

namespace SortKata.Domain.Builders {
    public class SortAnalyticsBuilder : BuilderBase<SortAnalytics> {
        public SortAnalyticsBuilder() : base() { }

        public new SortAnalyticsBuilder New() {
            base.New();
            return this;
        }

        public SortAnalyticsBuilder BuildSortType(ESortType sortType) {
            this.CheckInstanciate();
            this._t!.SortType = sortType;
            return this;
        }

        public SortAnalyticsBuilder BuildPerformanceMetadata(PerformanceMetadata performanceMetadata) {
            this.CheckInstanciate();
            this._t!.BestTimeComplexity = performanceMetadata.BestTimeComplexity;
            this._t.WorstTimeComplexity = performanceMetadata.WorstTimeComplexity;
            this._t.AverageTimeComplexity = performanceMetadata.AverageTimeComplexity;
            return this;
        }

        public SortAnalyticsBuilder BuildExecution(int itemCount, TimeSpan executionTime) {
            this.CheckInstanciate();
            this._t!.ItemCount = itemCount;
            this._t.ExecutionTime = executionTime;
            return this;
        }
    }
}
