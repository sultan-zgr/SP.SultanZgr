using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.API.RabbitMq.Producer
{
    public interface IRabbitMqProducer
    {
        public void SendMessage<T>(T message);
    }
}
