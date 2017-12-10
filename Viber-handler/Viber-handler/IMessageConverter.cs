using System;
using System.Collections.Generic;
using System.Text;

namespace Viber_handler
{
	public interface IMessageConverter
	{
		object Convert(Message message);
	}
}