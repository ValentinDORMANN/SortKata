namespace SortKata.Presentation.Assemblers {
    public interface IAssembler<TBusiness, TDto> {
        TDto ToDto(TBusiness business);
    }
}
