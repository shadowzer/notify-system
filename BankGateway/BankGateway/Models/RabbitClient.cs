﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;

namespace BankGateway.Models
{
	public class RabbitClient
	{
		private static volatile RabbitClient instance;
		private static object SyncRoot = new Object();
		public IConnection Connection { get; private set; }

		private RabbitClient()
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			this.Connection = factory.CreateConnection();
		}

		public static RabbitClient Instance
		{
			get
			{
				if (instance != null)
					return instance;

				lock (SyncRoot)
				{
					if (instance == null)
						instance = new RabbitClient();
				}
				return instance;
			}
		}
	}
}