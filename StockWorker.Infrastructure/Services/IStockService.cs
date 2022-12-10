using StockWorker.Infrastructure.Entities;

namespace StockWorker.Infrastructure.Services
{
    public interface IStockService
    {
        Task CreateCompany(Company company);
        Task CreateStock(StockPrice stockPriceEO);
    }
}
