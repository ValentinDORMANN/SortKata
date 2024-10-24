using SortKata.Domain.Models;

namespace SortKata.Presentation.Models {
    public class SortRequestDto<T> where T : IComparable<T> {
        public IEnumerable<T> List { get; set; }
        public string SortType { get; set; }
    }
}
