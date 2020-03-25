using StockManagement.Repository.Entities;
using StockManagement.Repository.Model;
using StockManagement.Repository.Repository.Interface;
using StockManagement.Service.Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace StockManagement.Service.Application.Services
{
    public class StockService :IStockService
    {
        private IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public Task<Company> AddStock(int stockQuantity)
        {
           return _stockRepository.AddStock(stockQuantity);
        }

        public Task<List<StockDetails>> GetStockDetails(bool accessLevel)
        {
            return _stockRepository.GetStockDetails(accessLevel);
        }

        public async Task<IPagedList<StockDetails>> GetStockDetails(bool accessLevel, string sortOrder, string param, int pageNo)
        {
            return await _stockRepository.GetStockDetails(accessLevel, sortOrder, param, pageNo);
        }

    }
}
