using Microsoft.EntityFrameworkCore;
using StockWorker.Infrastructure.DbContexts;
using StockWorker.Infrastructure.Entities;

namespace StockWorker.Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company, Guid>, ICompanyRepository
    {
        public CompanyRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
