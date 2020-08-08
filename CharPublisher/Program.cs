using System;
using System.IO;
using System.Text;
using System.Threading;
using ChatPublisher.PostBuilder;
using ChatPublisher.PostPublisher;
using ChatPublisher.PostReceiver;
using ChatSender.Logging;
using Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Services.Logging;

namespace ChatPublisher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //IPublisher publisher = new Publisher();
            //publisher.Publish();

            var streamWriter = File.AppendText("log.txt");
            IPostBuilder builder = new PostBuilder.PostBuilder();
            ILogger logger = new FileLogger(streamWriter);
            IPostReceiver receiver = new PostReceiverRabbit("localhost", logger, builder);
            IPostPublisher publisher = new ConsolePostPublisher(logger);
            receiver.MessageReceived += publisher.Publish;

            while (true)
            {
                receiver.Receive();
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

        }

       
    }

    public interface IPublisher
    {
        void Publish();
    }

    public class Publisher : IPublisher
    {
        public void Publish()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "postPublish",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "postPublish",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
}
}
