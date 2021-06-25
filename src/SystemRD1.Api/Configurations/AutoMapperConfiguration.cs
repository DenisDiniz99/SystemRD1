using AutoMapper;
using SystemRD1.Api.ViewModels;
using SystemRD1.Domain.Entities;
using SystemRD1.Domain.ValueObjects;

namespace SystemRD1.Api.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
            CreateMap<Address, AddressViewModel>().ReverseMap();
        }
    }
}
