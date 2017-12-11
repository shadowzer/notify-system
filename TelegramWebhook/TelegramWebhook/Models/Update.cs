using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramWebhook.Models
{
	/// <summary>
	/// https://core.telegram.org/bots/api#update
	/// </summary>
	public class Update
	{
		public int update_id { get; set; }
		public Message message { get; set; }
		public Message edited_message { get; set; }
	}
}