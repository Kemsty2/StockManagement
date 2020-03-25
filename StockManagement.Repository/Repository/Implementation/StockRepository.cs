using StockManagement.Repository.Entities;
using StockManagement.Repository.Model;
using StockManagement.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList.Mvc.Core;
using X.PagedList;

namespace StockManagement.Repository.Repository.Implementation
{
    public class StockRepository : IStockRepository
    {
        private StockManagementContext _stockManagementContext;

        public StockRepository(StockManagementContext stockManagementContext)
        {
            _stockManagementContext = stockManagementContext;
        }
        public async Task<Company> AddStock(int stockQuantity)
        {
            //To Generate Stock Price between 2-200 euros
            Random price = new Random();
            var stockPrice = price.Next(2, 200);
            if ((stockPrice * stockQuantity) >= 10000)
                return new Company();
            //To generate random company name
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < 5; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            //Adding Company object
            Company company = new Company()
            {
                CompanyName = str_build.ToString(),
                StockQuantity = stockQuantity,
                Price = stockPrice,
                Status = (stockPrice >= 100)
            };
            _stockManagementContext.Add(company);
            _stockManagementContext.SaveChanges();
            
            return company;
        }

        private async Task<List<StockDetails>> ListStockDetails (bool accessLevel)
        {
            List<Company> companies;
            if (accessLevel)
                companies = _stockManagementContext.Company.ToList();
            else
                companies = _stockManagementContext.Company.Where(x => x.Status == false).ToList();
            List<StockDetails> stockdetails = new List<StockDetails>();
            foreach (var item in companies)
            {
                Random rnd = new Random();
                double MIN_VALUE = -0.1;
                double MAX_VALUE = 0.1;

                double random = rnd.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE;
                decimal random_value = Math.Round(Convert.ToDecimal(random), 2);
                var currentStockPrice = item.Price + (item.Price * random_value) / 100;
                var deltaInPrice = currentStockPrice - item.Price;
                var deltaInPercentage = (currentStockPrice / item.Price) / 100;
                stockdetails.Add(new StockDetails()
                {
                    CompanyId = item.CompanyId,
                    CompanyName = item.CompanyName,
                    CurrentStockPrice = (decimal)currentStockPrice,
                    DeltaInPrice = (decimal)deltaInPrice,
                    DeltaInPercentage = (decimal)deltaInPercentage
                });
            }
            return stockdetails;
        }


        public async Task<List<StockDetails>> GetStockDetails(bool accessLevel)
        {
            return await ListStockDetails(accessLevel);
        }

        public async Task<IPagedList<StockDetails>> GetStockDetails(bool accessLevel,string sortOrder, string param, int pageNo)
        {
            const int _pageSize= 10;
            List<StockDetails> stockdetails = await ListStockDetails(accessLevel);

            if (sortOrder == "ASC")
                return await stockdetails.OrderBy(s => s.GetType().GetProperty(param).GetValue(s)).ToPagedListAsync(pageNo, _pageSize);
            else              
                return await stockdetails.OrderByDescending(s => s.GetType().GetProperty(param).GetValue(s)).ToPagedListAsync(pageNo, _pageSize);
        }
    }
}
