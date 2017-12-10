using System;
using System.Web.Mvc;

namespace TelegramHandler.Models
{
	public interface IMessageConverter
	{
		object Convert(Message message);
	}
}