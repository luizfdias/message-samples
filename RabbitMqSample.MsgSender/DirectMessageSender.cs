using RabbitMQ.Client;
using System.Text;

namespace RabbitMqSample.MsgSender
{
    public class DirectMessageSender
    {
        private const string ExchangeName = "sample1.direct";
        private const string QueueName = "sameple1.queue";

        public IConnection Connection { get; }

        public DirectMessageSender(IConnection connection)
        {
            Connection = connection;
        }        

        public void Send(string message)
        {
            using (var channel = Connection.CreateModel())
            {
                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true, false, null);
                channel.QueueDeclare(QueueName, true, false, false, null);
                channel.QueueBind(QueueName, ExchangeName, "", null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = false;

                var sentMessage = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(ExchangeName, "", properties, sentMessage);
            }
        }
    }
}
