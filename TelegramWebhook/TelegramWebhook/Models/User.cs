using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramWebhook.Models
{
	/// <summary>
	/// https://core.telegram.org/bots/api#user
	/// </summary>
	public class User
	{
		public int id { get; set; }
		public bool is_bot { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string username { get; set; }
		/// <summary>
		///  https://en.wikipedia.org/wiki/IETF_language_tag
		/// </summary>
		public string language_code { get; set; }
	}
}