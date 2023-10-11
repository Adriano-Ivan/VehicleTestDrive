using AutoMapper;
using CustomersAPI.Data;
using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using VehicleTestDrive.Events.Customer;

namespace CustomersAPI.Services
{
    public class CustomerService : ICustomer
    {
        private ApiDbContext dbContext;
        private IMessageProducer _messageProducer;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CustomerService(ApiDbContext dbContext, IMessageProducer messageProducer,
            IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            this.dbContext = dbContext;
            _messageProducer = messageProducer; 
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task AddCustomer(Customer customer)
        {
            var vehicle = await dbContext.Vehicles.FirstOrDefaultAsync(e => e.Id == customer.VehicleId);
            var vehicleObj = new Vehicle(customer.Vehicle);

            if(vehicle == null)
            {
                await dbContext.Vehicles.AddAsync(customer.Vehicle);
                await dbContext.SaveChangesAsync();
            }
            customer.Vehicle = null;
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            customer.Vehicle = vehicleObj;
            var customerCreated = _mapper.Map<CustomerCreated>(customer);
            await _publishEndpoint.Publish<CustomerCreated>(customerCreated);
            //_messageProducer.SendMessage<CustomerCreated>(customerCreated);
        }
    }
}
