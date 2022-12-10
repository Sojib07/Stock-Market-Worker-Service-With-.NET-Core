using Autofac;
using HtmlAgilityPack;
using StockWorker.Infrastructure.BusinessObjects;
using StockWorker.Infrastructure.Services;
using System.Text.RegularExpressions;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILifetimeScope _scope;
        private readonly ITimeService _timeService;

        public Worker(ILogger<Worker> logger, ILifetimeScope scope, ITimeService timeService)
        {
            _logger = logger;
            _scope = scope;
            _timeService = timeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";
                HtmlWeb web = new HtmlWeb();
                var htmlDoc = web.Load(html);
                var marketStatus = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='HeaderTop']//span//span").InnerText.ToString();
                if (marketStatus.ToLower() != "closed")
                {
                    var nodes = htmlDoc.DocumentNode.SelectNodes("//*[@class='table table-bordered background-white shares-table fixedHeader']//tr").ToList();
                    nodes.Remove(nodes[0]);
                    var data = new List<string>();
                    foreach (var node in nodes)
                    {
                        data.Clear();
                        await InsertCompany(node, data);
                        await InsertStockPrice(data);

                    }
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
        public async Task InsertCompany(HtmlNode node, List<string> data)
        {

            foreach (var inNode in node.ChildNodes)
            {
                var innerText = inNode.InnerText;
                if (string.IsNullOrWhiteSpace(innerText))
                {
                    continue;
                }
                var pattern = @"[^0-9a-zA-Z ,.!$&()-=@{}[\]]+";
                var replaceValue = string.Empty;
                innerText = Regex.Replace(innerText, pattern, replaceValue);
                data.Add(innerText);
            }
            data.Remove(data[0]);
            var company = _scope.Resolve<Company>();
            company.TradeCode = data[0];
            await company.CreateCompany(company);
        }
        public async Task InsertStockPrice(List<string> data)
        {
            var numericData = new List<double>();
            for (int i = 1; i < data.Count; i++)
            {
                double testValue;
                if (double.TryParse(data[i], out testValue) == true)
                {
                    numericData.Add(double.Parse(data[i]));
                }
                else
                {
                    numericData.Add(testValue);
                }
            }

            var stockPrice = _scope.Resolve<StockPrice>();
            stockPrice.TradeCode = data[0];
            stockPrice.Date = _timeService.Date;
            stockPrice.LastTradingPrice = numericData[0];
            stockPrice.High = numericData[1];
            stockPrice.Low = numericData[2];
            stockPrice.ClosePrice = numericData[3];
            stockPrice.YesterdayClosePrice = numericData[4];
            stockPrice.Change = numericData[5];
            stockPrice.Trade = numericData[6];
            stockPrice.Value = numericData[7];
            stockPrice.Volume = numericData[8];

            await stockPrice.CreateStock(stockPrice);
        }
    }
}