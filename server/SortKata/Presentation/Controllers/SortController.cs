using Microsoft.AspNetCore.Mvc;
using SortKata.Domain.Models;
using SortKata.Domain.Services;
using SortKata.Presentation.Assemblers;
using SortKata.Presentation.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SortKata.Presentation.Controllers {
    [ApiController]
    [Route("sort")]
    public class SortController<T> : ControllerBase where T : IComparable<T> {
        private static readonly string _ERROR_MSG = "An error occurred while processing the request.";
        
        private readonly ISortService<T> _sortService;
        private readonly SortAnalyticsAssembler _sortAnalyticsAssembler;

        public SortController(ISortService<T> sortService) {
            this._sortService = sortService;
            this._sortAnalyticsAssembler = new SortAnalyticsAssembler();
        }
        public SortController(
            ISortService<T> sortService,
            SortAnalyticsAssembler sortAnalyticsAssembler
        ) {
            this._sortService = sortService;
            this._sortAnalyticsAssembler = sortAnalyticsAssembler;
        }

        [HttpPost("analytics")]
        [SwaggerOperation(Summary = "Returns sort analytics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SortAnalyticsDto> GetSortAnalytics([FromBody] SortRequestDto<T> request) {
            if (request == null) { return BadRequest("Null request"); }
            if (!Enum.TryParse(request.SortType, true, out ESortType sortType)) {
                return BadRequest($"Invalid sort type: {request.SortType}");
            }
            try {
                SortAnalytics analytics = this._sortService.GetSortAnalytics(request.List, sortType);
                SortAnalyticsDto analyticsDto = this._sortAnalyticsAssembler.ToDto(analytics);
                return Ok(analyticsDto);
            } catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, _ERROR_MSG);
            }
        }
    }

    [ApiController]
    [Route("sort/numeric")]
    public class IntSortController : SortController<decimal> {
        public IntSortController(ISortService<decimal> sortService) : base(sortService) { }
    }
}
