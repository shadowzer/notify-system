using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramWebhook.Models
{
	/// <summary>
	/// https://core.telegram.org/bots/api#message
	/// </summary>
	public class Message
	{
		public int message_id { get; set; }
		public User from { get; set; }
		public int date { get; set; }
		public int edit_date { get; set; }
		public string text { get; set; }
	}
}