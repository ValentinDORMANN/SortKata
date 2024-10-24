using SortKata.Domain.Models;
using SortKata.Presentation.Models;

namespace SortKata.Presentation.Assemblers {
    public class SortAnalyticsAssembler : IAssembler<SortAnalytics, SortAnalyticsDto> {
        public virtual SortAnalyticsDto ToDto(SortAnalytics sortAnalytics) {
            return new SortAnalyticsDto() {
                SortType = sortAnalytics.SortType.ToString(),
                ItemCount = sortAnalytics.ItemCount,
                ExecutionTime = sortAnalytics.ExecutionTime.TotalMilliseconds,
                BestTimeComplexity = sortAnalytics.BestTimeComplexity,
                WorstTimeComplexity = sortAnalytics.WorstTimeComplexity,
                AverageTimeComplexity = sortAnalytics.AverageTimeComplexity
            };
        }
    }
}
