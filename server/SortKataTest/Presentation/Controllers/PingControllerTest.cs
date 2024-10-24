using Microsoft.AspNetCore.Mvc;
using SortKata.Presentation.Controllers;

namespace SortKataTest.Presentation.Controllers {
    public class PingControllerTest : ControllerTestToolbox {
        private PingController _pingController;

        public PingControllerTest() {
            this._pingController = new PingController();
        }

        [Fact]
        public void Ping_ReturnsOkPong() {
            ActionResult<string> result = this._pingController.Ping();

            AssertStatus200OK(result, "pong");
        }
    }
}
