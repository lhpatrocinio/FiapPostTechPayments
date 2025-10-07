using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Payments.Application.Producer
{
    public class PaymentCompletedProducer : IPaymentCompletedProducer
    {
        public void PublishUserActiveEvent(PaymentCompleted payment)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" }; // ou nome do container no docker-compose
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: $"{payment.GetType().Name}-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = JsonSerializer.Serialize(new
            {
                GameId = payment.GameId,
                UserId = payment.UserId,
                CreatedAt = DateTime.UtcNow
            });

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: $"{payment.GetType().Name}-queue",
                basicProperties: null,
                body: body);
        }

        public class PaymentCompleted
        {
            public Guid GameId { get; set; }
            public Guid UserId { get; set; }
        }
    }
}
