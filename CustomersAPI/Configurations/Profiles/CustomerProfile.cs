using AutoMapper;
using CustomersAPI.DTOs.Customer;
using CustomersAPI.Models;
using VehicleTestDrive.Events.Customer;

namespace CustomersAPI.Configurations.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDTO, Customer>().ReverseMap();
            CreateMap<CustomerCreated, Customer>().ReverseMap();    
        }
    }
}
