using StockManagement.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Service.Application.Interface
{
    public interface IStockPurchaseService
    {
        Task<Order> AddOrder(Order orderDetails);
        Task<List<Order>> GetOrder(string userName);
    }
}
