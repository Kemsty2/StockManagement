using StockManagement.Repository.Entities;
using StockManagement.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace StockManagement.Repository.Repository.Interface
{
    public interface IStockRepository
    {
        Task<Company> AddStock(int stockQuantity);
        Task<List<StockDetails>> GetStockDetails(bool accessLevel);
        Task<IPagedList<StockDetails>> GetStockDetails(bool accessLevel, string sortOrder, string param, int pageNo);
    }
}
