namespace StockWorker.Infrastructure.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Date { get => DateTime.Now; }
    }
}
