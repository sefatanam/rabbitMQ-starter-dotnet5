using System;
using System.Collections;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQ.Client;
using static System.Console;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {

            string? message;
            do
            {
                Write("Enter Your Message : ");
                message = ReadLine();

                SendMessage(message);
            }
            while (!string.IsNullOrWhiteSpace(message));

            
        }

        static void SendMessage(dynamic obj)
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            dynamic message = obj;
            dynamic body = Encoding.UTF32.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "demo-queue", true, null, body);
        }
    }
}
