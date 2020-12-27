using System;
using System.Collections;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:sefat@localhost:5672")
            };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            dynamic message = new { Name = "Producer", Message = "Work Done !" };
            dynamic body = Encoding.UTF32.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "demo-queue", true, null, body);
        }
    }
}
