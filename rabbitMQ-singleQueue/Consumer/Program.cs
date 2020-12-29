using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using RabbitMQ.Client.Events;
using static System.Console;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Waiting....");
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
          

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string messge = Encoding.UTF8.GetString(body);
                WriteLine(messge);
            };

            channel.BasicConsume("demo-queue", true, consumer);
            ReadLine();

        }
    }
}
