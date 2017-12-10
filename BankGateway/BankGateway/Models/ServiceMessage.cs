using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankGateway.Models
{
	public class ServiceMessage
	{
		public string Service { get; set; }
		public Message Message { get; set; }
	}
}