using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using TelegramHandler.Models;
using RabbitMQ.Client;

namespace TelegramHandler
{
	class Program
	{
		static void Main(string[] args)
		{
			IMessageConverter converter = new TelegramConvert();
			using (var connection = RabbitClient.Instance.Connection)
			using (var consumeChannel = connection.CreateModel())
			using (var publishChannel = connection.CreateModel())
			{
				consumeChannel.ExchangeDeclare(exchange: "notifyMessages",
					type: "direct");
				var consumeQueueName = consumeChannel.QueueDeclare().QueueName;
				consumeChannel.QueueBind(queue: consumeQueueName,
					exchange: "notifyMessages",
					routingKey: "telegramPending");

				publishChannel.ExchangeDeclare(exchange: "notifyMessages",
					type: "direct");
				var publishQueueName = publishChannel.QueueDeclare().QueueName;
				publishChannel.QueueBind(queue: publishQueueName,
					exchange: "notifyMessages",
					routingKey: "telegramReady");

				var consumer = new EventingBasicConsumer(consumeChannel);
				consumer.Received += (model, ea) =>
				{
					var message = Encoding.UTF8.GetString(ea.Body);
					var handledMessage = JsonConvert.SerializeObject(converter.Convert(new Message(message)));
					publishChannel.BasicPublish(exchange: "notifyMessages",
						routingKey: "telegramReady",
						basicProperties: null,
						body: Encoding.UTF8.GetBytes(handledMessage)
					);
				};
				consumeChannel.BasicConsume(queue: consumeQueueName,
					autoAck: true,
					consumer: consumer);

				Console.WriteLine("Press [enter] to exit.");
				Console.ReadLine();
			}
		}
	}
}
