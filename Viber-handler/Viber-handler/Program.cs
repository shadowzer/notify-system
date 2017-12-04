using System;
using System.Net.Http;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace Viber_handler
{
    class Worker
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "messages",
                    type: "direct");
                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queue: queueName,
                    exchange: "messages",
                    routingKey: "viber");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    sendMessageAsync(message);
                };
                channel.BasicConsume(queue: queueName,
                    noAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
        static async void sendMessageAsync(string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://chatapi.viber.com/pa/send_message");//TODO cut url to config
                await client.PostAsync("", new StringContent(new ViberMessage(message).ToString(), Encoding.UTF8, "application/json"));
            }
            Console.WriteLine(new ViberMessage(message).ToString());
        }
    }
}
