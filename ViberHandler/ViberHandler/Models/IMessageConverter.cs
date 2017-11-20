using System;
using System.Web.Mvc;

namespace ViberHandler.Models
{
	public interface IMessageConverter
	{
		object Convert(Message message);
	}
}