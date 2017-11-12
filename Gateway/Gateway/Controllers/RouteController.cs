using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Http;
using Gateway.Models;
using RabbitMQ.Client;

namespace Gateway.Controllers
{
    public class RabbitClient
    {
        private static volatile RabbitClient instance;
        private static object SyncRoot = new Object();
        public IConnection Connection { get; private set; }
        public IModel channel { get; private set; }
        private RabbitClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            this.Connection = factory.CreateConnection();
            this.channel = this.Connection.CreateModel();
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
    

    public class RouteController : ApiController
	{
        [HttpPost]
		[Route("send")]
		public void ParseQuery(Job job)
		{
		    
            RabbitClient.Instance.channel.BasicPublish(exchange: "messages",
                routingKey: job.Service,
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(job.Message.ToString()));
            
        }

    }
}
