using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using BankGateway.Models;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace BankGateway.Controllers
{
	public class RouteController : ApiController
	{
		[HttpPost]
		[Route("sendMessage")]
		public string SendMessage(ServiceMessage serviceMessage)
		{
			switch (serviceMessage.Service.ToLower())
			{
				case "telegram":
					using (var telegramChannel = RabbitClient.Instance.Connection.CreateModel())
					{
						telegramChannel.ExchangeDeclare(exchange: "notifyMessages",
							type: "direct");
						var telegramQueueName = telegramChannel.QueueDeclare().QueueName;
						telegramChannel.QueueBind(queue: telegramQueueName,
							exchange: "notifyMessages",
							routingKey: "telegramPending");

						telegramChannel.BasicPublish(exchange: "notifyMessages",
							routingKey: "telegramPending",
							body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(serviceMessage.Message))
						);
						return JsonConvert.SerializeObject("Message was queued for processing to Telegram.");
					}
				case "viber":
					using (var viberChannel = RabbitClient.Instance.Connection.CreateModel())
					{
						viberChannel.ExchangeDeclare(exchange: "notifyMessages",
							type: "direct");
						var viberQueueName = viberChannel.QueueDeclare().QueueName;
						viberChannel.QueueBind(queue: viberQueueName,
							exchange: "notifyMessages",
							routingKey: "viberPending");

						viberChannel.BasicPublish(exchange: "notifyMessages",
							routingKey: "viberPending",
							body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(serviceMessage.Message))
						);
						return JsonConvert.SerializeObject("Message was queued for processing to Viber.");
					}
				default:
					return JsonConvert.SerializeObject("Service \"" + serviceMessage.Service +
					                                   "\" is currently not supported. Supported services: Telegram, Viber.");
			}
		}
	}
}