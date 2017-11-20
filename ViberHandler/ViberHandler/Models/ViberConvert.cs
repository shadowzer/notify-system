using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViberHandler.Models
{
	public class ViberConvert : IMessageConverter
	{
		public object Convert(Message message)
		{
			return new ViberMessage
			{
				chat_id = message.Id,
				text = message.Text
			}; ;
		}
	}
}