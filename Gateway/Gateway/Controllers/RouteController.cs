using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Gateway.Models;
using Gateway.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Gateway.Controllers
{
	public class RouteController : ApiController
	{
		// default gateway
		[HttpPost]
		[Route("send")]
		public void SendMessage(Job job)
		{
			var connection = RabbitClient.Instance.Connection;
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "TelegramPending",
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				channel.BasicPublish(exchange: "",
					routingKey: "TelegramPending",
					basicProperties: null,
					body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(job.Message))
				);
			}
		}

		// bank gateway
		public void SendAnswerToBank(string answer)
		{
			// todo send answer to bank gateway
		}
	}
}
