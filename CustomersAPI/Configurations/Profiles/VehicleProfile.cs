using AutoMapper;
using CustomersAPI.Models;
using VehicleTestDrive.Events.Models;

namespace CustomersAPI.Configurations.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<CustomerVehicle, Vehicle>().ReverseMap();
        }
    }
}
