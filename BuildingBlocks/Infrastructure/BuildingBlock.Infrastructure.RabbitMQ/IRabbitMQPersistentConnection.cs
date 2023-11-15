using RabbitMQ.Client;

namespace BuildingBlock.Infrastructure.RabbitMQ;

public interface IRabbitMQPersistentConnection
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}