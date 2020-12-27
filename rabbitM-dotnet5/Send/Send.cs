using System.Text;
using RabbitMQ.Client;
using static System.Console;

class Send
{
    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost"};
        // var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "rabbit", Password = "12345" };
        // This is how we create Server
        // The connection abstracts the socket connection, and takes care of protocol version negotiation and 
        // authentication and so on for us. Here we connect to a RabbitMQ node on the local machine - hence
        // the localhost. If we wanted to connect to a node on a different machine we'd simply specify its hostname or IP address here.
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                // Next we create a channel, which is where most of the API for getting things done resides.

                channel.QueueDeclare(queue: "Hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                string message = "Hello World";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                WriteLine($"[x] Sent {message}");
            }

            WriteLine("Press [enter] to exit");
            ReadLine();
        }
    }

}
