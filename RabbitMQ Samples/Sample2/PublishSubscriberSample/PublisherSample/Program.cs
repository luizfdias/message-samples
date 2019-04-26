using Common;
using RabbitMQ.Client;
using System;
using System.Text;

namespace PublisherSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting publisher...");

            using (var connection = RabbitConnectionFactory.GetConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", ExchangeType.Fanout);

                    while (true)
                    {
                        var message = Console.ReadLine();

                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "logs",
                                             routingKey: "",
                                             basicProperties: null,
                                             body: body);
                    }
                }
            }            
        }
    }
}
