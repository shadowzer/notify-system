using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Gateway.Clients
{
	public class HttpClientSingleton
	{
		private static volatile HttpClientSingleton instance;
		private static object SyncRoot = new Object();
		public HttpClient Client { get; private set; }

		private HttpClientSingleton()
		{
			this.Client = new HttpClient();
		}

		public static HttpClientSingleton Instance
		{
			get
			{
				if (instance != null)
					return instance;

				lock (SyncRoot)
				{
					if (instance == null)
						instance = new HttpClientSingleton();
				}
				return instance;
			}
		}
	}
}