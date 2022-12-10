using AutoMapper;
using StockWorker.Infrastructure.Services;
using StockWorker.Infrastructure.UnitOfWorks;
using StockPriceEO = StockWorker.Infrastructure.Entities.StockPrice;

namespace StockWorker.Infrastructure.BusinessObjects
{
    public class StockPrice
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime Date {  get; set; }
        public double LastTradingPrice { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double ClosePrice { get; set; }
        public double YesterdayClosePrice { get; set; }
        public double Change { get; set; }
        public double Trade { get; set; }
        public double Value { get; set; }
        public double Volume { get; set; }
        public string TradeCode { get; set; }

        private IStockService _stockService;
        private IApplicationUnitOfWork _applicationUnitOfWork;
        private IMapper _mapper;

        public StockPrice(IStockService stockService,IApplicationUnitOfWork applicationUnitOfWork,IMapper mapper)
        {
            _stockService = stockService;
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        public async Task CreateStock(StockPrice stockPrice)
        {
            StockPriceEO StockPriceEO = new StockPriceEO();
            var shareCompany = _applicationUnitOfWork.Companies.Get(x=>x.TradeCode==stockPrice.TradeCode,"").First();

            _mapper.Map(stockPrice,StockPriceEO);
            StockPriceEO.Company = shareCompany;
            StockPriceEO.CompanyId = shareCompany.Id;
            //StockPriceEO.Date = stockPrice.Date;
            //StockPriceEO.LastTradingPrice = stockPrice.LastTradingPrice;
            //StockPriceEO.High = stockPrice.High;
            //StockPriceEO.Low = stockPrice.Low;
            //StockPriceEO.ClosePrice = stockPrice.ClosePrice;
            //StockPriceEO.YesterdayClosePrice = stockPrice.YesterdayClosePrice;
            //StockPriceEO.Change = stockPrice.Change;
            //StockPriceEO.Trade = stockPrice.Trade;
            //StockPriceEO.Value = stockPrice.Value;
            //StockPriceEO.Volume = stockPrice.Volume;
            await _stockService.CreateStock(StockPriceEO);
        }
    }
}
