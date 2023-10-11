using AutoMapper;
using ReservationsAPI.Models;
using VehicleTestDrive.Events.Customer;

namespace ReservationsAPI.Configurations.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CustomerCreated, Reservation>().ReverseMap();
        }
    }
}
