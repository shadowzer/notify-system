using System;
using System.Collections.Generic;
using System.Text;

namespace Viber_handler
{
    public class ViberConverter : IMessageConverter
    {
	    public object Convert(Message message)
	    {
		    return new ViberMessage // TODO:change sender name 
			{
			    auth_token = "46d7df8f5827d2bc-2029b8013c527800-5eccc42d2b8b0f5a", // todo get token from properties
				receiver = message.Id,
				text = message.Text,
				type = "text"
			};
	    }
    }
}
