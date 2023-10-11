using AutoMapper;
using ReservationsAPI.Models;
using VehicleTestDrive.Events.Models;

namespace ReservationsAPI.Configurations.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<CustomerVehicle, Vehicle>().ReverseMap();
        }
    }
}
