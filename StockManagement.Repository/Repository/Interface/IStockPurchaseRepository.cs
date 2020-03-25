using StockManagement.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Repository.Repository.Interface
{
    public interface IStockPurchaseRepository
    {
        Task<Order> AddOrder(Order orderDetails);
        Task<List<Order>> GetOrder(string userName);
    }
}
