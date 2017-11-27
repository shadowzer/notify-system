using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gateway.Models
{
	public class Job
	{
		public string Service { get; set; } // fixme: use enum instead of hardcoded strings
		public Message Message { get; set; }

		public override string ToString()
		{
			return "Service: " + Service + ", " + Message.ToString();
		}
	}
}