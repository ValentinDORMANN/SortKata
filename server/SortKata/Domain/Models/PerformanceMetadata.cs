namespace SortKata.Domain.Models {
    public class PerformanceMetadata {
        public string BestTimeComplexity { get; set; }
        public string WorstTimeComplexity { get; set; }
        public string AverageTimeComplexity { get; set; }

        public PerformanceMetadata(string best, string worst, string average) {
            BestTimeComplexity = best;
            WorstTimeComplexity = worst;
            AverageTimeComplexity = average;
        }
    }
}
