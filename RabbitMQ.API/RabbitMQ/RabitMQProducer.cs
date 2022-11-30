using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.API.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        
            var connection = factory.CreateConnection();
           
            using
            var channel = connection.CreateModel();
            
            channel.QueueDeclare("product", exclusive: false);
         
            string json = JsonConvert.SerializeObject(message);
            byte[] body = Encoding.UTF8.GetBytes(json);
          
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
