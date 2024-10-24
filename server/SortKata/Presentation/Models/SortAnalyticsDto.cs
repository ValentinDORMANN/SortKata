using SortKata.Domain.Models;

namespace SortKata.Presentation.Models {
    public class SortAnalyticsDto {
        public string SortType { get; set; }
        public int ItemCount { get; set; }
        public double ExecutionTime { get; set; }
        public string BestTimeComplexity { get; set; }
        public string WorstTimeComplexity { get; set; }
        public string AverageTimeComplexity { get; set; }

        public SortAnalyticsDto() {
            this.SortType = string.Empty;
            this.ItemCount = 0;
            this.ExecutionTime = 0;
            this.BestTimeComplexity = string.Empty;
            this.WorstTimeComplexity = string.Empty;
            this.AverageTimeComplexity = string.Empty;
        }
    }
}
