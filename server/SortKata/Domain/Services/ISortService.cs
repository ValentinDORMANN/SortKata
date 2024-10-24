using SortKata.Domain.Models;

namespace SortKata.Domain.Services {
    public interface ISortService<T> where T : IComparable<T> {
        SortAnalytics GetSortAnalytics(IEnumerable<T> list, ESortType sortType);
    }
}
