using System.Text;
using RabbitMQ.Client;
using System.Web.Http;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using TelegramWebhook.Models;

namespace TelegramWebhook.Controllers
{
	public class TelegramController : ApiController
    {
		/// <summary>
		/// Telegram highly recommends to use some secret in URL (f.e. current bot api token since only you and Telegram knows it)  https://core.telegram.org/bots/api#setwebhook
		/// </summary>
		[HttpPost]
		[Route("getUpdate")] 
		public void GetUpdate(Update update)
	    {
			// todo maybe would be better to catch global commands (such as /start and /help https://core.telegram.org/bots/#commands) here and immediately respond to such requests

			var connection = RabbitClient.Instance.Connection;
		    using (var publishChannel = connection.CreateModel()) // todo close channel every time or better to bring it to singleton?
		    {
			    publishChannel.ExchangeDeclare(exchange: "notifyMessages",
				    type: "direct");
			    var publishQueueName = publishChannel.QueueDeclare().QueueName;
			    publishChannel.QueueBind(queue: publishQueueName,
				    exchange: "notifyMessages",
				    routingKey: "telegramUpdate");
			    publishChannel.BasicPublish(exchange: "notifyMessages",
				    routingKey: "telegramUpdate",
				    body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(update))
			    );
			}
	    }
	}
}
