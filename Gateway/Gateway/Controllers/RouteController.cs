using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Gateway.Models;

namespace Gateway.Controllers
{
	public class RouteController : ApiController
	{
		private Dictionary<string, string> ServiceToIP;
		private Dictionary<string, string> ServiceToUrl;

		public RouteController()
		{
			ServiceToUrl = new Dictionary<string, string>();
			ServiceToUrl.Add("Telegram", "https://api.telegram.org/bot<token>/sendMessage"); // todo move secret out of project files
			ServiceToUrl.Add("Viber", null);

			ServiceToIP = new Dictionary<string, string>();
			ServiceToIP.Add("Telegram", "http://localhost:4470/telegram/convertMessage"); // todo change from static routing
			ServiceToIP.Add("Viber", null);
		}

		[HttpPost]
		[Route("send")]
		public string ParseQuery(Job job)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(ServiceToIP[job.Service]);
				var resp = client.PostAsJsonAsync("", job.Message);
				using (var client2 = new HttpClient())
				{
					client2.BaseAddress = new Uri(ServiceToUrl[job.Service]);
					var query = resp.Result.Content.ReadAsStringAsync().Result;
					resp = client2.PostAsync("", new StringContent(query, Encoding.UTF8, "application/json"));
					return resp.Result.Content.ReadAsStringAsync().Result;
				}
			}
		}
	}
}
