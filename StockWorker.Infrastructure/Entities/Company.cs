namespace StockWorker.Infrastructure.Entities
{
    public class Company : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string TradeCode { get; set; }
        public List<StockPrice> StockPrices { get; set; }
    }
}
