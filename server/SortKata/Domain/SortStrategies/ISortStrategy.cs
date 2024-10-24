using SortKata.Domain.Models;

namespace SortKata.Domain.SortStrategies {
    public interface ISortStrategy<T> where T : IComparable<T> {
        public IEnumerable<T> Sort(IEnumerable<T> list);
        PerformanceMetadata PerformanceMetadata { get; }
    }
}
