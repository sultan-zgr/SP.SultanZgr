using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace SP.API.RabbitMq.Consumer
{
    public class CunsomeService : ICunsomerService
    {
        private readonly IConfiguration _configuration;

        public CunsomeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConsumeMessage<T>(T message)
        {
            var connectionHost = _configuration.GetSection("RabbitMqConfiguration:RabbitMqConnection").Value;
            var factory = new ConnectionFactory
            {
                HostName = connectionHost,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("Message", exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<T>(json);

                // Mesajı SQL veritabanına kaydedecek işlemleri burada gerçekleştirin.
                //SaveMessageToDatabaseAsync(message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "Message", autoAck: false, consumer: consumer);
        }


    }
}

