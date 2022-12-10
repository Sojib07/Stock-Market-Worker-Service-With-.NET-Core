using AutoMapper;
using StockWorker.Infrastructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyEO = StockWorker.Infrastructure.Entities.Company;
using StockPriceEO = StockWorker.Infrastructure.Entities.StockPrice;

namespace StockWorker.Infrastructure.Profiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<CompanyEO, Company>()
                .ReverseMap();

            CreateMap<StockPriceEO, StockPrice>()
                .ForMember(dest => dest.TradeCode, src => src.Ignore())
                .ReverseMap();
        }
    }
}
