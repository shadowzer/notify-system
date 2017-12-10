using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace TelegramHandler.Models
{
	public class Message
	{
		public string Id { get; set; }
		public string Text { get; set; }

		public Message() { }

		public Message(string json)
		{
			var jObject = JObject.Parse(json);
			Id = (string)jObject["Id"];
			Text = (string)jObject["Text"];
		}

		public override string ToString()
		{
			return "Id: " + Id + ", Text: " + Text;
		}
	}
}