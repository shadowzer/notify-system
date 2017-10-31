using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using ViberHandler.Models;

namespace ViberHandler.Controllers
{
	[RoutePrefix("viber")]
	public class ViberController : ApiController
	{
		[HttpPost]
		[Route("convertMessage")]
		public JsonResult<object> ConvertMessage(Message message)
		{
			return Json(new ViberConvert().Convert(message));
		}
	}
}
