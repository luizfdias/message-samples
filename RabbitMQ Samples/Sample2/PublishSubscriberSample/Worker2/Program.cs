using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Worker2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Worker2...");

            using (var connection = RabbitConnectionFactory.GetConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, "logs", "");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] {0}", message);

                        channel.BasicAck(ea.DeliveryTag, false);
                    };

                    channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

                    Console.ReadLine();
                }
            }
        }
    }
}
