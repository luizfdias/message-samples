using RabbitMqSample.Common;
using System;

namespace RabbitMqSample.MsgConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Escutando...");

            new DirectMessageReceiver(RabbitConnectionFactory.GetConnection()).Consume();
        }
    }
}
