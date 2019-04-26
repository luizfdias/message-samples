using RabbitMQ.Client;
using System;

namespace Common
{
    public static class RabbitConnectionFactory
    {
        public static IConnection GetConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672/")
            };

            return connectionFactory.CreateConnection();
        }
    }
}
