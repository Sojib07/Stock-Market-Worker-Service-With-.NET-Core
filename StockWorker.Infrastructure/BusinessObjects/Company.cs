using AutoMapper;
using StockWorker.Infrastructure.Services;
using CompanyEO = StockWorker.Infrastructure.Entities.Company;

namespace StockWorker.Infrastructure.BusinessObjects
{
    public class Company
    {
        public Guid Id { get; set; }
        public string TradeCode { get; set; }

        private IStockService _stockService;
        private IMapper _mapper;

        public Company(IStockService stockService,IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task CreateCompany(Company company)
        {
            var companyEO=_mapper.Map<CompanyEO>(company);
            await _stockService.CreateCompany(companyEO);
        }
    }
}
