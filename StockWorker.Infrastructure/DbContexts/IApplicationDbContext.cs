using Microsoft.EntityFrameworkCore;
using StockWorker.Infrastructure.Entities;

namespace StockWorker.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<StockPrice> StockPrices { get; set; }
    }
}