using Microsoft.EntityFrameworkCore;
using StockWorker.Infrastructure.DbContexts;
using StockWorker.Infrastructure.Repositories;

namespace StockWorker.Infrastructure.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICompanyRepository Companies { get; private set; }
        public IStockPriceRepository StockPrice { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            ICompanyRepository companyRepository, IStockPriceRepository stockPriceRepository) : base((DbContext)dbContext)
        {
            Companies = companyRepository;
            StockPrice = stockPriceRepository;
        }
    }
}
