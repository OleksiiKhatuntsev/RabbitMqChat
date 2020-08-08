using System;
using System.Collections.Generic;
using System.Text;
using ChatPublisher.PostBuilder;
using ChatSender.Logging;
using Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatPublisher.PostReceiver
{
    public class PostReceiverRabbit : IPostReceiver
    {
        private readonly string _hostName;
        private readonly ILogger _logger;
        private readonly string _queueName;
        private readonly IPostBuilder _postBuilder;

        public PostModel Post { get; private set; }

        public PostReceiverRabbit(string hostName, ILogger logger, IPostBuilder postBuilder, string queueName = "postPublish")
        {
            _hostName = hostName;
            _logger = logger;
            _queueName = queueName;
            _postBuilder = postBuilder;
        }

        public void Receive()
        {
            var factory = new ConnectionFactory() {HostName = _hostName};
            _logger.LogInfo($"ConnectionFactory on {_hostName} was created");

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                _logger.LogInfo($"Queue with name {_queueName} was declared");

                var consumer = new EventingBasicConsumer(channel);

                var message = "";
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                    if (!string.IsNullOrEmpty(message))
                    {
                        Post = _postBuilder.BuildModel(message);
                        OnMessageReceived();
                        _logger.LogInfo($"Message with title : {Post.Title} received");
                    }
                };

                channel.BasicConsume(queue: _queueName,
                    autoAck: true,
                    consumer: consumer);
            }
        }

        public delegate void MessageReceivedEventHandler(object source, EventArgs args);

        public event EventHandler<MessageEventArgs> MessageReceived;

        protected virtual void OnMessageReceived()
        {
            MessageReceived?.Invoke(this, new MessageEventArgs {Post = Post});
        }
    }
}
