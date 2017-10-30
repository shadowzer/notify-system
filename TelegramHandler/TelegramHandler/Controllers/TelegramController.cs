using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using TelegramHandler.Models;

namespace TelegramHandler.Controllers
{
	[RoutePrefix("telegram")]
	public class TelegramController : ApiController
	{
		[HttpPost]
		[Route("convertMessage")]
		public JsonResult<object> ConvertMessage(Message message)
		{
			return Json(new TelegramConvert().Convert(message));
		}
	}
}
