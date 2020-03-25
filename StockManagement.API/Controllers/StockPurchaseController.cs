using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagement.API.Filters;
using StockManagement.Repository.Entities;
using StockManagement.Service.Application.Interface;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingAction))]
    [ServiceFilter(typeof(LoggingException))]
    public class StockPurchaseController : ControllerBase
    {
        private IStockPurchaseService _stockPurchaseService;

        public StockPurchaseController(IStockPurchaseService stockPurchaseService)
        {
            _stockPurchaseService = stockPurchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            if (order.Quantity > 0)
                return new JsonResult(await _stockPurchaseService.AddOrder(order));
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<JsonResult> GetOrder(string userName)
        {
            return new JsonResult(await _stockPurchaseService.GetOrder(userName));
        }
    }
}