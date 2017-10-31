using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViberHandler.Models
{
	public class ViberMessage
	{
		public string chat_id { get; set; }
		public string text { get; set; }

		public override string ToString()
		{
			return "Id: " + chat_id + ", Text: " + text;
		}
	}
}