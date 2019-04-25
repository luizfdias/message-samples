using RabbitMqSample.Common;
using System;

namespace RabbitMqSample.MsgSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new DirectMessageSender(RabbitConnectionFactory.GetConnection());

            while (true)
            {
                var message = Console.ReadLine();

                sender.Send(message);
            }
        }
    }
}
