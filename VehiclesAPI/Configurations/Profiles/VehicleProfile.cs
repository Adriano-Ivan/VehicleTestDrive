using AutoMapper;
using VehiclesAPI.DTOs.Vehicle;
using VehiclesAPI.Models;

namespace VehiclesAPI.Configurations.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<CreateVehicleDto, Vehicle>().ReverseMap();
            CreateMap<UpdateVehicleDto, Vehicle>().ReverseMap();
        }
    }
}
