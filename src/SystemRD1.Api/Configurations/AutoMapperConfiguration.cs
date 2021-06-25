using AutoMapper;
using SystemRD1.Api.ViewModels;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Api.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
