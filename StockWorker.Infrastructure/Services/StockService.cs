using StockWorker.Infrastructure.UnitOfWorks;
using CompanyEO = StockWorker.Infrastructure.Entities.Company;
using StockPriceEO = StockWorker.Infrastructure.Entities.StockPrice;

namespace StockWorker.Infrastructure.Services
{
    public class StockService:IStockService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly ITimeService _timeService;
        public StockService(IApplicationUnitOfWork applicationUnitOfWork, ITimeService timeService)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _timeService = timeService;
        }

        public async Task CreateCompany(CompanyEO company)
        {
            var count = _applicationUnitOfWork.Companies.GetCount(x => x.TradeCode == company.TradeCode);
            if (count == 0)
            {
                _applicationUnitOfWork.Companies.Add(company);
                _applicationUnitOfWork.Save();
            }
        }

        public async Task CreateStock(StockPriceEO stockPriceEO)
        {
            _applicationUnitOfWork.StockPrice.Add(stockPriceEO);
            _applicationUnitOfWork.Save();
        }

    }
}
