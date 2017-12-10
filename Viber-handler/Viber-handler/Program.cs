using System;
using System.Net.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace Viber_handler
{
    class Worker
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; //todo get hostname from properties
            using (var connection = factory.CreateConnection())
			using (var consumeChannel = connection.CreateModel())
			using (var publishChannel = connection.CreateModel())
			{
				consumeChannel.ExchangeDeclare(exchange: "notifyMessages",
					type: "direct");
				var consumeQueueName = consumeChannel.QueueDeclare().QueueName;
				consumeChannel.QueueBind(queue: consumeQueueName,
					exchange: "notifyMessages",
					routingKey: "viberPending");

				publishChannel.ExchangeDeclare(exchange: "notifyMessages",
					type: "direct");
				publishChannel.QueueBind(queue: consumeQueueName,
					exchange: "notifyMessages",
					routingKey: "viberReady");

				var consumer = new EventingBasicConsumer(consumeChannel);
				var converter = new ViberConverter();
				consumer.Received += (model, ea) =>
				{
					var message = Encoding.UTF8.GetString(ea.Body);
					var handledMessage = JsonConvert.SerializeObject(converter.Convert(new Message(message)));
					publishChannel.BasicPublish(exchange: "notifyMessages",
						routingKey: "viberReady",
						body: Encoding.UTF8.GetBytes(handledMessage)
					);
				};
				consumeChannel.BasicConsume(queue: consumeQueueName,
					noAck: true,
					consumer: consumer);

				Console.WriteLine("Press [enter] to exit.");
				Console.ReadLine();
			}
        }
    }
}
