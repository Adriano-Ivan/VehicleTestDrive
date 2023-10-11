using MassTransit;
using System.Text.Json;
using VehicleTestDrive.Events.Customer;

namespace ReservationsAPI.Consumers
{
    public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
    {
        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });

            Console.WriteLine($"NotificationCreated event consumed. Message: {serializedMessage}");
        }
    }
}
