using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Gateway
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var telegramHttpClient = new HttpClient())
			using (var viberHttpClient = new HttpClient())
			using (var rabbitConnection = RabbitClient.Instance.Connection)
			using (var telegramChannel = rabbitConnection.CreateModel())
			using (var viberChannel = rabbitConnection.CreateModel())
			{
				telegramHttpClient.BaseAddress = new Uri("https://api.telegram.org/bot<token>/"); // todo get base url from properties

				telegramChannel.ExchangeDeclare(exchange: "notifyMessages",
					type: "direct");
				var telegramQueueName = telegramChannel.QueueDeclare().QueueName;
				telegramChannel.QueueBind(queue: telegramQueueName,
					exchange: "notifyMessages",
					routingKey: "telegramReady");

				var telegramConsumer = new EventingBasicConsumer(telegramChannel);
				telegramConsumer.Received += (model, ea) =>
				{
					var message = Encoding.UTF8.GetString(ea.Body);
					var response = PostAsync(client: telegramHttpClient, method: "sendMessage", message: message);
				};
				telegramChannel.BasicConsume(queue: telegramQueueName, 
					autoAck: true,
					consumer: telegramConsumer);


				viberHttpClient.BaseAddress = new Uri("https://chatapi.viber.com/pa/"); // todo get base url from properties

				viberChannel.ExchangeDeclare(exchange: "notifyMessages",
					type: "direct");
				var viberQueueName = viberChannel.QueueDeclare().QueueName;
				viberChannel.QueueBind(queue: viberQueueName,
					exchange: "notifyMessages",
					routingKey: "viberReady");

				var viberConsumer= new EventingBasicConsumer(viberChannel);
				viberConsumer.Received += (model, ea) =>
				{
					var message = Encoding.UTF8.GetString(ea.Body);
					var response = PostAsync(client: viberHttpClient, method: "send_message", message: message);
				};
				viberChannel.BasicConsume(queue: viberQueueName,
					autoAck: true,
					consumer: viberConsumer);

				Console.WriteLine("Press [enter] to exit.");
				Console.ReadLine();
			}
		}

		public static async Task<HttpResponseMessage> PostAsync(HttpClient client, string method, string message)
		{
			return await client.PostAsync(method, new StringContent(message, Encoding.UTF8, "application/json"));
		}
	}
}
