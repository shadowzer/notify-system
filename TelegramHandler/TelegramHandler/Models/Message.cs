using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelegramHandler.Models
{
	public class Message
	{
		public string Id { get; set; }
		public string Text { get; set; }

		public override string ToString()
		{
			return "Id: " + Id + ", Text: " + Text;
		}
	}
}