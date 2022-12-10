using StockWorker.Infrastructure.Repositories;

namespace StockWorker.Infrastructure.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        ICompanyRepository Companies { get; }
        IStockPriceRepository StockPrice { get; }
    }
}