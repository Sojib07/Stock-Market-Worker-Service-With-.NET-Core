using Autofac;
using StockWorker.Infrastructure.BusinessObjects;

namespace StockData.Worker
{
    public class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Company>().AsSelf();
            builder.RegisterType<StockPrice>().AsSelf();
            base.Load(builder);
        }
    }
}
