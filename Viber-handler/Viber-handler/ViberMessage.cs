using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Viber_handler
{
    public class ViberMessage
    {

        public string Id { get; set; }
        public string Text { get; set; }

        public ViberMessage(string mess)
        {
            Id = mess.Substring(mess.IndexOf(':') + 1, mess.IndexOf("Text", StringComparison.OrdinalIgnoreCase) - mess.IndexOf(':')-3);
            Text = mess.Substring(mess.IndexOf("text", StringComparison.OrdinalIgnoreCase)+5);
        }

        public override string ToString()
        {
            return "{\"auth_token\" :" + "\"46d1678012e7d48c-4a411369fcd8263d-122e41e707bdf322\" " + ",\"receiver\": \"" + Id + "\", \"text\": \"" + Text + "\",\"type\" : \"text\"}";// TODO:chenge sender name 
        }
    }
}