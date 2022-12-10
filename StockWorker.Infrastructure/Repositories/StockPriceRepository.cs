using Microsoft.EntityFrameworkCore;
using StockWorker.Infrastructure.DbContexts;
using StockWorker.Infrastructure.Entities;

namespace StockWorker.Infrastructure.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, Guid>, IStockPriceRepository
    {
        public StockPriceRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
