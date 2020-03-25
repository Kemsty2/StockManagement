using StockManagement.Repository.Entities;
using StockManagement.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.Repository.Repository.Implementation
{
    public class StockPurchaseRepository : IStockPurchaseRepository
    {
        private StockManagementContext _stockManagementContext;

        public StockPurchaseRepository(StockManagementContext stockManagementContext)
        {
            _stockManagementContext = stockManagementContext;
        }
        public async Task<Order> AddOrder(Order orderdetails)
        {
            var companyId = _stockManagementContext.Company.Where(x => x.CompanyName == orderdetails.Company.CompanyName.ToString()).Select(s => s.CompanyId).FirstOrDefault();
            Order order = new Order()
            {
                CompanyId = companyId,
                Quantity = orderdetails.Quantity,
                OrderDate = DateTime.Now,
                UserName = orderdetails.UserName,
            };
            _stockManagementContext.Order.Add(order);
            _stockManagementContext.SaveChanges();
            return order;
        }
        public async Task<List<Order>> GetOrder(string userName)
        {
            return _stockManagementContext.Order.Where(x => x.UserName == userName).OrderByDescending(o=>o.OrderDate).ToList();
        }
    }
}
