using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using TelegramHandler.Models;
using RabbitMQ.Client;

namespace TelegramHandler
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			IMessageConverter converter = new TelegramConvert();
			var connection = RabbitClient.Instance.Connection;
			var consumerChannel = connection.CreateModel();
			var publisherChannel = connection.CreateModel();
			//{
			consumerChannel.QueueDeclare(queue: "TelegramPending",
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);
			publisherChannel.QueueDeclare(queue: "TelegramReady",
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			var consumer = new EventingBasicConsumer(consumerChannel);
			consumer.Received += (model, ea) =>
			{
				var message = Encoding.UTF8.GetString(ea.Body);
				//Console.WriteLine(" [x] Received {0}", message);
				var handledMessage = JsonConvert.SerializeObject(converter.Convert(new Message(message)));
				publisherChannel.BasicPublish(exchange: "",
					routingKey: "TelegramReady",
					basicProperties: null,
					body: Encoding.UTF8.GetBytes(handledMessage)
				);

				//var resp = client.PostAsync("sendMessage", new StringContent(message, Encoding.UTF8, "application/json"));
				//new RouteController().Answer(resp.Result.Content.ReadAsStringAsync().Result);
			};
			consumerChannel.BasicConsume(queue: "TelegramPending",
				autoAck: true,
				consumer: consumer);
			//}
		}
	}
}
