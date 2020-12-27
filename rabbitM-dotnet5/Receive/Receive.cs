using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using static System.Console;

class Receive
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        // var factory = new ConnectionFactory() { HostName = "localhost", Port = 5505, UserName = "rabbit", Password = "12345" };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    WriteLine($"[x] Receive {message}");
                };

                channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

                WriteLine("Press [enter] to exit");
                ReadLine();
            }
        }
    }

}
