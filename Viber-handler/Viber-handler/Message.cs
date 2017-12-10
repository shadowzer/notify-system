using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Viber_handler
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
	}
}