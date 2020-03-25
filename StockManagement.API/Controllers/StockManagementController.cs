using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.API.Filters;
using StockManagement.Repository.Entities;
using StockManagement.Repository.Model;
using StockManagement.Service.Application.Interface;

namespace StockManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingAction))]
    [ServiceFilter(typeof(LoggingException))]
    public class StockManagementController : ControllerBase
    {
        private IStockService _stockService { get; set; }
        public StockManagementController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("{stockQuantity}")]
        public async Task<IActionResult> AddStock(int stockQuantity)
        {
            Task<Company> company= _stockService.AddStock(stockQuantity);
            if(company.Result.CompanyName !=null)
                return new JsonResult(company);
            return BadRequest();
        }

       

        [HttpGet]
        [Authorize]
        
        public async Task<JsonResult> GetStockDetails()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "ACCESS_LEVEL" && c.Value=="SUPERUSER"))
            {
                return new JsonResult(await _stockService.GetStockDetails(true));
            }
            else
            {
                return new JsonResult(await _stockService.GetStockDetails(false));
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<JsonResult> GetStockDetails(string sortOrder, string param,int pageNo=1)
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "ACCESS_LEVEL" && c.Value == "SUPERUSER"))
            {
                return new JsonResult(await _stockService.GetStockDetails(true,sortOrder, param,pageNo));
            }
            else
            {
                return new JsonResult(await _stockService.GetStockDetails(false));
            }

        }


    }
}