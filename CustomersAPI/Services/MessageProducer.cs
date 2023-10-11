using AutoMapper;
using CustomersAPI.Interfaces;
using MassTransit;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CustomersAPI.Services
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _virtualHost;
        private readonly int _port;

        public MessageProducer(IConfiguration configuration)
        {
            _hostName = configuration.GetValue<string>("ServiceBus:HostName");
            _userName = configuration.GetValue<string>("ServiceBus:UserName");
            _virtualHost = configuration.GetValue<string>("ServiceBus:VirtualHost");
            _password = configuration.GetValue<string>("ServiceBus:Password");
            _port = configuration.GetValue<int>("ServiceBus:Port");
         
        }

        public void SendMessage<T>(T message)
        {
            try
            {

                var factory = new ConnectionFactory()
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password,
                    VirtualHost = _virtualHost,
                    Port = _port
                };

                var conn = factory.CreateConnection();

                using var channel = conn.CreateModel();
                

                channel.QueueDeclare("reservations", durable: true, exclusive: false);

                var jsonString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonString);
                channel.BasicPublish("", "reservations", body: body);

               
            } catch(Exception e)
            {

            }           
        }
    }
}
