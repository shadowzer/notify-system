using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TelegramHandler.Models
{
	public class TelegramConvert : IMessageConverter
	{
		public object Convert(Message message) => new TelegramMessage
		{
			chat_id = message.Id,
			text = message.Text
		};
	}
}