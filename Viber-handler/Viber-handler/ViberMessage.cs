using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Viber_handler
{
    public class ViberMessage
    {
		public string auth_token { get; set; }
        public string receiver { get; set; }
        public string text { get; set; }
		public string type { get; set; }
    }
}