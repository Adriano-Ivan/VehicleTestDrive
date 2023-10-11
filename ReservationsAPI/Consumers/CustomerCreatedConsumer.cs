using AutoMapper;
using MassTransit;
using ReservationsAPI.Interfaces;
using ReservationsAPI.Models;
using System.Text.Json;
using VehicleTestDrive.Events.Customer;

namespace ReservationsAPI.Consumers
{
    public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
    {
        private readonly IReservation _reservationService;
        private readonly IMapper _mapper;

        public CustomerCreatedConsumer(IReservation reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;   
        }

        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });
            CustomerCreated customerCreated = context.Message;
            Reservation reservation = _mapper.Map<Reservation>(customerCreated);

            await _reservationService.InsertReservation(reservation);

            Console.WriteLine($"NotificationCreated event consumed. Message: {serializedMessage}");
        }
    }
}
