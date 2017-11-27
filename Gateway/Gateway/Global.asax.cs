using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Gateway.Controllers;
using Gateway.Models;
using Gateway.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Gateway
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

			var controller = new RouteController();
			const string telegramApiBaseUrl = "https://api.telegram.org/bot<token>/"; // todo get token
			var client = new HttpClient {BaseAddress = new Uri(telegramApiBaseUrl)};
			// RabbitMQ
			var connection = RabbitClient.Instance.Connection;
			var channel = connection.CreateModel();
			channel.QueueDeclare(queue: "TelegramReady",
				durable: false,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			var consumer = new EventingBasicConsumer(channel);
			var routeController = new RouteController();
			consumer.Received += (model, ea) =>
			{
				var message = Encoding.UTF8.GetString(ea.Body);
				controller.SendAnswerToBank(
					controller.PostAsync(client, "sendMessage", message).Result.Content.ReadAsStringAsync().Result
					);
			};
			channel.BasicConsume(queue: "TelegramReady",
				autoAck: true,
				consumer: consumer);
		}
	}
}
