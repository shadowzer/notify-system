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
            return "{\"auth_token\" :" + "\"46d7df8f5827d2bc-2029b8013c527800-5eccc42d2b8b0f5a\" " + ",\"receiver\": \"" + Id + "\", \"text\": \"" + Text + "\",\"type\" : \"text\"}";// TODO:chenge sender name 
        }
    }
}