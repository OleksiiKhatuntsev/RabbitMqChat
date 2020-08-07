using System.Text;
using ChatSender.Logging;
using Models;
using RabbitMQ.Client;

namespace ChatSender.Post
{
    public class PostPublisherRabbit : IPostPublisher
    {
        private readonly string _hostName;
        private readonly ILogger _logger;
        private readonly string _queueName;

        public PostPublisherRabbit(string hostName, ILogger logger, string queueName = "postPublish")
        {
            _hostName = hostName;
            _logger = logger;
            _queueName = queueName;
        }

        public void Publish(PostModel postModel)
        {
            
            var factory = new ConnectionFactory { HostName = _hostName };
            _logger.LogInfo($"ConnectionFactory on {_hostName} was created");

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            _logger.LogInfo($"Queue with name {_queueName} was declared");

            var message = postModel.ToString();
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body);

            //ToDo make it with ILogger 
            _logger.LogInfo($"{postModel.Title} was sent");
        }
    }
}
