using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockManagement.API.Controllers;
using StockManagement.Repository.Model;
using StockManagement.Service.Application.Interface;

namespace StockManagement.Test
{
    [TestClass]
    public class StockManagementTest
    {
        private readonly Mock<IStockService> mockStockservice;
        private readonly Mock<HttpContext> mockHttpContext;
        public StockManagementTest()
        {
            mockStockservice = new Mock<IStockService>();
            mockHttpContext = new Mock<HttpContext>();
        }
        [TestMethod]
        public async void StockManagementControllerTest()
        {
            mockHttpContext.Setup(c => c.Request).Returns(new Mock<HttpRequest>().Object);
            mockHttpContext.Setup(c => c.Request.Query).Returns(new Mock<IQueryCollection>().Object);
            mockStockservice.Setup(x => x.GetStockDetails(true));
            var controllerContext = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };
            StockManagementController testControllerObject = new StockManagementController(mockStockservice.Object)
            {
                ControllerContext = controllerContext
            };
            IActionResult result = await testControllerObject.GetStockDetails();
            var jsonResult = result as JsonResult;
            var response = (StockDetails)jsonResult.Value;
            Assert.AreEqual(response!=null, true);
        }
    }
}
