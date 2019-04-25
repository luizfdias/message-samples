using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMqSample.MsgConsumer
{
    public class DirectMessageReceiver
    {
        private const string ExchangeName = "sample1.direct";
        private const string QueueName = "sameple1.queue";

        public IConnection Connection { get; }

        public DirectMessageReceiver(IConnection connection)
        {
            Connection = connection;
        }        

        public void Consume()
        {
            using (var channel = Connection.CreateModel())
            {
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(QueueName, true, false, false, null);
                channel.QueueBind(QueueName, ExchangeName, "", null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    Console.WriteLine("[Message received] " + Encoding.UTF8.GetString(ea.Body));
                };

                channel.BasicConsume(QueueName, true, consumer);
                
                Console.ReadLine();
            }
        }
    }
}
