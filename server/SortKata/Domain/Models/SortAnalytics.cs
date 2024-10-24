namespace SortKata.Domain.Models {
    public class SortAnalytics {
        public ESortType SortType { get; set; }
        public int ItemCount { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public string BestTimeComplexity { get; set; }
        public string WorstTimeComplexity { get; set; }
        public string AverageTimeComplexity { get; set; }

        public SortAnalytics() {
            this.SortType = ESortType.UNDEFINED;
            this.ItemCount = 0;
            this.ExecutionTime = TimeSpan.Zero;
            this.BestTimeComplexity = string.Empty;
            this.WorstTimeComplexity = string.Empty;
            this.AverageTimeComplexity = string.Empty;
        }
    }
}
