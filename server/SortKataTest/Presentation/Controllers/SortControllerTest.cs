using Microsoft.AspNetCore.Mvc;
using Moq;
using SortKata.Domain.Models;
using SortKata.Domain.Services;
using SortKata.Presentation.Assemblers;
using SortKata.Presentation.Controllers;
using SortKata.Presentation.Models;

namespace SortKataTest.Presentation.Controllers {
    public class SortControllerTest : ControllerTestToolbox {
        private static readonly string _ERROR_MSG  = "Custom error";

        private Mock<ISortService<double>> _sortServiceMock;
        private Mock<SortAnalyticsAssembler> _sortAnalyticsAssemblerMock;
        private SortController<double> _sortController;

        public SortControllerTest() {
            this._sortServiceMock = new Mock<ISortService<double>>(MockBehavior.Strict);
            this._sortAnalyticsAssemblerMock = new Mock<SortAnalyticsAssembler>(MockBehavior.Strict);
            this._sortController = new SortController<double>(
                this._sortServiceMock.Object, this._sortAnalyticsAssemblerMock.Object
            );
        }

        #region GetSortAnalytics
        [Fact]
        public void GetSortAnalytics_NoBody_ReturnsBadRequest() {
            ActionResult<SortAnalyticsDto> result = this._sortController.GetSortAnalytics(null as SortRequestDto<double>);

            this.AssertStatus400BadRequest(result, "Null request");
        }

        [Theory]
        [InlineData("")]
        [InlineData("bad_sortType")]
        public void GetSortAnalytics_RequestWithUnparsableSortType_ReturnsBadRequest(string sortType) {
            SortRequestDto<double> request = new SortRequestDto<double>() { SortType = sortType };

            ActionResult<SortAnalyticsDto> result = this._sortController.GetSortAnalytics(request);

            this.AssertStatus400BadRequest(result, $"Invalid sort type: {sortType}");
        }

        [Fact]
        public void GetSortAnalytics_SortServiceFails_ReturnsInternalServerError() {
            ESortType sortType = ESortType.INSERTION_SORT;
            IEnumerable<double> list = Enumerable.Empty<double>();
            SortRequestDto<double> request = new SortRequestDto<double>() {
                List = list,
                SortType = sortType.ToString() 
            };
            this._sortServiceMock.Setup(m => m.GetSortAnalytics(It.IsAny<IEnumerable<double>>(), It.IsAny<ESortType>()))
                .Throws(new Exception(_ERROR_MSG));

            ActionResult<SortAnalyticsDto> result = this._sortController.GetSortAnalytics(request);

            this.AssertStatus500InternalServerError(result);
            this._sortServiceMock.Verify(m => m.GetSortAnalytics(request.List, sortType), Times.Once());
        }

        [Fact]
        public void GetSortAnalytics_HappyPath_ReturnsOk() {
            ESortType sortType = ESortType.INSERTION_SORT;
            IEnumerable<double> list = Enumerable.Empty<double>();
            SortRequestDto<double> request = new SortRequestDto<double>() {
                List = list,
                SortType = sortType.ToString()
            };
            SortAnalytics sortAnalytics = new SortAnalytics();
            SortAnalyticsDto sortAnalyticsDto = new SortAnalyticsDto();
            this._sortServiceMock.Setup(m => m.GetSortAnalytics(It.IsAny<IEnumerable<double>>(), It.IsAny<ESortType>()))
                .Returns(sortAnalytics);
            this._sortAnalyticsAssemblerMock.Setup(m => m.ToDto(It.IsAny<SortAnalytics>())).Returns(sortAnalyticsDto);

            ActionResult<SortAnalyticsDto> result = this._sortController.GetSortAnalytics(request);

            this.AssertStatus200OK(result, sortAnalyticsDto);
            this._sortServiceMock.Verify(m => m.GetSortAnalytics(request.List, sortType), Times.Once());
            this._sortAnalyticsAssemblerMock.Verify(m => m.ToDto(sortAnalytics), Times.Once());
        }
        #endregion GetSortAnalytics
    }
}
