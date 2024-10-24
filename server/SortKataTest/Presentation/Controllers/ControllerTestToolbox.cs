using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SortKataTest.Presentation.Controllers {
    public abstract class ControllerTestToolbox {
        protected void AssertStatus200OK<T>(ActionResult<T> actionResult, T result) {
            ActionResult<T> okResult = Assert.IsType<ActionResult<T>>(actionResult);
            Assert.IsType<OkObjectResult>(okResult.Result);
            OkObjectResult? objectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(result, objectResult.Value);
        }

        protected void AssertStatus400BadRequest<T>(ActionResult<T> actionResult, string message) {
            ActionResult<T> objectResult = Assert.IsType<ActionResult<T>>(actionResult);
            Assert.IsType<BadRequestObjectResult>(objectResult.Result);
            BadRequestObjectResult? badRequestResult = objectResult.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(message, badRequestResult.Value);
        }

        protected void AssertStatus500InternalServerError<T>(ActionResult<T> actionResult) {
            ActionResult<T> objectResult = Assert.IsType<ActionResult<T>>(actionResult);
            Assert.IsType<ObjectResult>(objectResult.Result);
            ObjectResult? errorResult = objectResult.Result as ObjectResult;
            Assert.NotNull(errorResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", errorResult.Value);
        }
    }
}
