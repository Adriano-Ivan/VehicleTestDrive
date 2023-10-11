using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReservationsAPI.Data;
using ReservationsAPI.Interfaces;
using ReservationsAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container..
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Configuration.AddJsonFile("emailConfig.json");
// ServiceBus configurations
var serviceBusHostName = builder.Configuration.GetValue<string>("ServiceBus:HostName");
var serviceBusVirtualHost = builder.Configuration.GetValue<string>("ServiceBus:VirtualHost");
var serviceBusUsername = builder.Configuration.GetValue<string>("ServiceBus:UserName");
var serviceBusPassword = builder.Configuration.GetValue<string>("ServiceBus:Password");
var serviceBusPort = builder.Configuration.GetValue<ushort>("ServiceBus:Port");

var emailUsername = builder.Configuration.GetValue<string>("usernameEmailService");
var emailPassword = builder.Configuration.GetValue<string>("passwordEmailService");
var emailHost = builder.Configuration.GetValue<string>("emailHost");

// Database
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(busConfigurator =>
{
    var entryAssembly = Assembly.GetExecutingAssembly();
    busConfigurator.AddConsumers(entryAssembly);
    busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host(serviceBusHostName, serviceBusPort, serviceBusVirtualHost, h => {
            h.Password(serviceBusPassword);
            h.Username(serviceBusUsername);
            
        });
       
        busFactoryConfigurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<IReservation, ReservationService>();

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
