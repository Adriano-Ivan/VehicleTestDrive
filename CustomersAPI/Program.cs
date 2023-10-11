using CustomersAPI.Data;
using CustomersAPI.Interfaces;
using CustomersAPI.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ServiceBus configurations
var serviceBusHostName = builder.Configuration.GetValue<string>("ServiceBus:HostName");
var serviceBusVirtualHost = builder.Configuration.GetValue<string>("ServiceBus:VirtualHost");
var serviceBusUsername = builder.Configuration.GetValue<string>("ServiceBus:UserName");
var serviceBusPassword = builder.Configuration.GetValue<string>("ServiceBus:Password");
var serviceBusPort = builder.Configuration.GetValue<ushort>("ServiceBus:Port");

// Database
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host(serviceBusHostName, serviceBusPort, serviceBusVirtualHost, hostConfigurator =>
        {
            hostConfigurator.Password(serviceBusPassword);
            hostConfigurator.Username(serviceBusUsername);
        });

    });
});

builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
