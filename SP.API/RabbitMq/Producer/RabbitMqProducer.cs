using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace SP.API.RabbitMq.Producer
{
    public class RabbitMqProducer : IRabbitMqProducer
    {
        private readonly IConfiguration _configuration;

        public RabbitMqProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage<T>(T message)
        {
            var connectionHost = _configuration.GetSection("RabbitMqConfiguration:RabbitMqConnection").Value;
            var factory = new ConnectionFactory
            {
                HostName = connectionHost,
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("Message", exclusive: false, autoDelete: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "Message", body: body);


        }
    }
}
