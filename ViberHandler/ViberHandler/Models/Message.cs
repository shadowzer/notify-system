using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViberHandler.Models
{
	public class Message
	{
		public string Id { get; set; }
		public string Text { get; set; }

		public override string ToString()
		{
			return "receiver: " + Id + ", text: " + Text + "type : text" + "sender:{ name : Johny}";// TODO:chenge sender name 
		}
	}
}