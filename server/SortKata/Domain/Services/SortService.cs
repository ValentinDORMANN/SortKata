using SortKata.Domain.Builders;
using SortKata.Domain.Models;
using SortKata.Domain.SortStrategies;
using System.Diagnostics;

namespace SortKata.Domain.Services {
    public class SortService<T> : ISortService<T> where T : IComparable<T> {
        private readonly SortStrategyFactory<T> _sortStrategyFactory;
        private ISortStrategy<T> _sortStrategy;
        private SortAnalyticsBuilder _sortAnalyticsBuilder;

        public SortService() {
            this._sortStrategyFactory = new SortStrategyFactory<T>();
            this._sortStrategy = new QuickSortStrategy<T>();
            this._sortAnalyticsBuilder = new SortAnalyticsBuilder();
        }

        private void SetSortStrategy(ESortType sortType) {
            this._sortStrategy = this._sortStrategyFactory.GetSortStrategy(sortType);
        }

        public SortAnalytics GetSortAnalytics(IEnumerable<T> list, ESortType sortType) {
            this.SetSortStrategy(sortType);
            Stopwatch stopwatch = Stopwatch.StartNew();
            IEnumerable<T> sortedList = this._sortStrategy.Sort(list);
            stopwatch.Stop();
            return this._sortAnalyticsBuilder.New()
                .BuildSortType(sortType)
                .BuildPerformanceMetadata(this._sortStrategy.PerformanceMetadata)
                .BuildExecution(sortedList.Count(), stopwatch.Elapsed)
                .Finalize();
        }
    }
}
