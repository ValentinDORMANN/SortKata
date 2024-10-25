using Moq;
using SortKata.Domain.Builders;
using SortKata.Domain.Models;
using SortKata.Domain.Services;
using SortKata.Domain.SortStrategies;


namespace SortKata.Tests.Services {
    public class SortServiceTests {
        private Mock<SortStrategyFactory<int>> _strategyFactoryMock;
        private Mock<ISortStrategy<int>> _sortStrategyMock;
        private Mock<SortAnalyticsBuilder> _sortAnalyticsBuilderMock;
        private SortService<int> _sortService;

        public SortServiceTests() {
            this._strategyFactoryMock = new Mock<SortStrategyFactory<int>>(MockBehavior.Strict);
            this._sortStrategyMock = new Mock<ISortStrategy<int>>(MockBehavior.Strict);
            this._sortAnalyticsBuilderMock = new Mock<SortAnalyticsBuilder>(MockBehavior.Strict);
            this._sortService = new SortService<int>(
                this._strategyFactoryMock.Object, this._sortStrategyMock.Object,
                this._sortAnalyticsBuilderMock.Object
            );
        }

        private void SetupAnalyticBuilder(SortAnalytics sortAnalytics) {
            this._sortAnalyticsBuilderMock.Setup(m => m.New()).Returns(this._sortAnalyticsBuilderMock.Object);
            this._sortAnalyticsBuilderMock.Setup(m => m.BuildSortType(It.IsAny<ESortType>())).Returns(this._sortAnalyticsBuilderMock.Object);
            this._sortAnalyticsBuilderMock.Setup(m => m.BuildPerformanceMetadata(It.IsAny<PerformanceMetadata>())).Returns(this._sortAnalyticsBuilderMock.Object);
            this._sortAnalyticsBuilderMock.Setup(m => m.BuildExecution(It.IsAny<int>(), It.IsAny<TimeSpan>())).Returns(this._sortAnalyticsBuilderMock.Object);
            this._sortAnalyticsBuilderMock.Setup(m => m.Finalize()).Returns(sortAnalytics);
        }

        private void VerifyAnalyticBuilder(ESortType sortType, PerformanceMetadata performanceMetadata, int itemCount) {
            this._sortAnalyticsBuilderMock.Verify(m => m.New(), Times.Once);
            this._sortAnalyticsBuilderMock.Verify(m => m.BuildSortType(sortType), Times.Once);
            this._sortAnalyticsBuilderMock.Verify(m => m.BuildPerformanceMetadata(performanceMetadata), Times.Once);
            this._sortAnalyticsBuilderMock.Verify(m => m.BuildExecution(itemCount, It.IsAny<TimeSpan>()), Times.Once);
            this._sortAnalyticsBuilderMock.Verify(m => m.Finalize(), Times.Once);
        }

        public void GetSortAnalytics_HappyPath_ReturnsSortAnalytic() {
            this._strategyFactoryMock.Setup(m => m.GetSortStrategy(It.IsAny<ESortType>())).Returns(this._sortStrategyMock.Object);
            PerformanceMetadata performanceMetadata = new PerformanceMetadata("", "", "");
            this._sortStrategyMock.SetupGet(m => m.PerformanceMetadata).Returns(performanceMetadata);
            SortAnalytics sortAnalytics = new SortAnalytics();
            this.SetupAnalyticBuilder(sortAnalytics);

            IEnumerable<int> list = new List<int>() { 10 };
            ESortType sortType = ESortType.INSERTION_SORT;

            SortAnalytics result = this._sortService.GetSortAnalytics(list, sortType);

            Assert.Equal(sortAnalytics, result);
            this._strategyFactoryMock.Verify(m => m.GetSortStrategy(sortType), Times.Once);
            this._sortStrategyMock.Verify(m => m.PerformanceMetadata, Times.Once);
            this.VerifyAnalyticBuilder(sortType, performanceMetadata, list.Count());
        }

    }
}
