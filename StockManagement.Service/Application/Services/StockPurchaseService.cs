using StockManagement.Repository.Entities;
using StockManagement.Repository.Repository.Interface;
using StockManagement.Service.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Service.Application.Services
{
    public class StockPurchaseService : IStockPurchaseService
    {
        private IStockPurchaseRepository _stockPurchaseRepository;

        public StockPurchaseService(IStockPurchaseRepository stockPurchaseRepository)
        {
            _stockPurchaseRepository = stockPurchaseRepository;
        }
        public Task<Order> AddOrder(Order orderDetails)
        {
            return _stockPurchaseRepository.AddOrder(orderDetails);
        }

        public Task<List<Order>> GetOrder(string userName)
        {
            return _stockPurchaseRepository.GetOrder(userName);
        }
    }
}
