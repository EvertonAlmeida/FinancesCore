using AutoMapper;
using FinancesCore.App.ViewModels;
using FinancesCore.Business.Models;

namespace FinancesCore.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}
